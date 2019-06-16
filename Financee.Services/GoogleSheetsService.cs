using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Financee.Common.ViewModels;
using Financee.Common.ViewModels.GoogleSheets;
using Financee.Services.Contracts;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Financee.Services
{
    public class GoogleSheetsService : IGoogleSheetsService
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "2019Smetki";
        private string sheet = "Януари";
        static readonly string SpreadsheetId = "1-DD-rMm-iaA0GcWKkdutlf4U9QFXQAKEKSBKhgfYGqs";
        static SheetsService service;

        public GoogleSheetsViewModel MonthlyReportFromGoogleSheets()
        {
            Init();
            var readSheets = ReadSheet();

            return readSheets;
        }
        private void Init()
        {
            GoogleCredential credential;
            using (var stream = new FileStream("app_client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }
            // Creating Google Sheets API service...
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private GoogleSheetsViewModel ReadSheet()
        {
            sheet = DateTime.Now.ToString("MMMM", new CultureInfo("bg-BG")).ToUpper();
            // Specifying Column Range for reading...
            var range = $"{sheet}!B:G";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(SpreadsheetId, range);
            // Ecexuting Read Operation...
            var response = request.Execute();
            // Getting all records from Column B to G...
            IList<IList<object>> values = response.Values;
            var allInfo = new GoogleSheetsViewModel();
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    if (row.Count == 3 || row.Count == 6 || row.Count == 4)
                    {
                        if (row[1].ToString() == "ОБЩО:")
                        {
                            break;
                        }

                        var expenditureDayInt = row[0].ToString();

                        bool successExpenditure = int.TryParse(expenditureDayInt, out int numberExpenditure);
                        if (successExpenditure)
                        {
                            var getString = row[1].ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            var decimalParse = getString[0];
                            var decima = decimal.Parse(decimalParse);
                            var date = new DateTime(2019, DateTime.Now.Month, numberExpenditure);

                            allInfo.GoogleSheetExpenditures.Add(new GoogleSheetExpenditureViewModel
                            {
                                Weekday = WeekDayTranslateBg(date),
                                Date = date,
                                Expenditure = decima,
                                ForWhat = row[2].ToString()
                            });
                        }

                        if (row.Count == 6)
                        {
                            var incomeDayInt = row[3].ToString();
                            bool successIncome = int.TryParse(incomeDayInt, out int numberIncome);
                            if (successIncome)
                            {
                                string[] getString = row[4].ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                                string decimalParse = getString[0];
                                bool successDecimalIncome = decimal.TryParse(decimalParse, out decimal decimalParseString);
                                if (successDecimalIncome)
                                {
                                    var date = new DateTime(2019, DateTime.Now.Month, numberIncome);
                                    allInfo.GoogleSheetIncomes.Add(new GoogleSheetIncomeViewModel
                                    {
                                        Weekday = WeekDayTranslateBg(date),
                                        Date = date,
                                        Income = decimalParseString,
                                        FromWhere = row[5].ToString()
                                    });
                                }
                            }
                        }
                    }
                }
            }

            allInfo.Month = sheet;
            allInfo.AvailableMoney = GetAvailableMoney();
            return allInfo;
        }

        public GoogleSheetsViewModel ViewByMonth(int id)
        {
            sheet = new DateTime().AddMonths(id - 1).ToString("MMMM", new CultureInfo("bg-BG")).ToUpper();

            // Specifying Column Range for reading...
            var range = $"{sheet}!B:G";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(SpreadsheetId, range);
            // Ecexuting Read Operation...
            var response = request.Execute();
            // Getting all records from Column B to G...
            IList<IList<object>> values = response.Values;
            var allInfo = new GoogleSheetsViewModel();
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    if (row.Count == 3 || row.Count == 6 || row.Count == 4)
                    {
                        if (row[1].ToString() == "ОБЩО:")
                        {
                            break;
                        }

                        var expenditureDayInt = row[0].ToString();

                        bool successExpenditure = int.TryParse(expenditureDayInt, out int numberExpenditure);
                        if (successExpenditure)
                        {
                            var getString = row[1].ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            var decimalParse = getString[0];
                            var decima = decimal.Parse(decimalParse);

                            allInfo.GoogleSheetExpenditures.Add(new GoogleSheetExpenditureViewModel
                            {
                                Weekday = WeekDayTranslateBg(new DateTime(2019, id, numberExpenditure)),
                                Date = new DateTime(2019, id, numberExpenditure),
                                Expenditure = decima,
                                ForWhat = row[2].ToString()
                            });
                        }

                        if (row.Count == 6)
                        {
                            var incomeDayInt = row[3].ToString();
                            bool successIncome = int.TryParse(incomeDayInt, out int numberIncome);
                            if (successIncome)
                            {
                                string[] getString = row[4].ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                                string decimalParse = getString[0];
                                bool successDecimalIncome = decimal.TryParse(decimalParse, out decimal decimalParseString);
                                if (successDecimalIncome)
                                {
                                    allInfo.GoogleSheetIncomes.Add(new GoogleSheetIncomeViewModel
                                    {
                                        Weekday = WeekDayTranslateBg(new DateTime(2019, id, numberIncome)),
                                        Date = new DateTime(2019, id, numberIncome),
                                        Income = decimalParseString,
                                        FromWhere = row[5].ToString()
                                    });
                                }
                            }
                        }
                    }
                }
            }

            allInfo.Month = sheet;
            allInfo.AvailableMoney = GetAvailableMoney();
            return allInfo;
        }

        private decimal GetAvailableMoney()
        {
            var range = $"{sheet}!H2";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;

            string[] decimalValue = values[0][0].ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);


            bool successDecimalAvailableMoney = decimal.TryParse(decimalValue[0], out decimal decimalParseString);
            if (successDecimalAvailableMoney)
            {
                return decimalParseString;
            }
            return 0;
        }

        private string WeekDayTranslateBg(DateTime date)
        {
            CultureInfo bulgarian = new CultureInfo("bg-BG");
            var weekDayInBulgarian = bulgarian.DateTimeFormat.GetDayName(date.DayOfWeek);
            return weekDayInBulgarian;
        }

        public void AddExpenditureInGoogleSheets(ExpenditureModalBindingModel model)
        {
            sheet = DateTime.Now.ToString("MMMM", new CultureInfo("bg-BG")).ToUpper();
            // Specifying Column Range for reading...
            var range = $"{sheet}!A:C";
            var valueRange = new ValueRange();
            // Data for another Student...
            var oblist = new List<object> {model.Date.Day.ToString(), model.Expenditure.ToString(CultureInfo.InvariantCulture), model.ForWhat };
            valueRange.Values = new List<IList<object>> { oblist };
            // Append the above record...
            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
        }

        //static void UpdateCell()
        //{
        //    // Setting Cell Name...
        //    var range = $"{sheet}!C5";
        //    var valueRange = new ValueRange();
        //    // Setting Cell Value...
        //    var oblist = new List<object>() { "32" };
        //    valueRange.Values = new List<IList<object>> { oblist };
        //    // Performing Update Operation...
        //    var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreadsheetId, range);
        //    updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        //    var appendReponse = updateRequest.Execute();
        //}
    }
}