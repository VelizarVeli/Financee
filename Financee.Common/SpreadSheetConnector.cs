//using System;
//using System.Collections.Generic;
//using System.IO;
//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Services;
//using Google.Apis.Sheets.v4;
//using Newtonsoft.Json;

//namespace Financee.Common
//{
//    public class SpreadSheetConnector
//    {

//        private string[] _scopes = { SheetsService.Scope.Spreadsheets };
//        private string _applicationName = "My Application Name from Google API Project ";
//        private string _spreadsheetId = "xdMsqBc3wblahblahblahblahkeygoeshere";
//        private SheetsService _sheetsService;

//        private void ConnectToGoogle()
//        {
//            GoogleCredential credential;

//            // Put your credentials json file in the root of the solution and make sure copy to output dir property is set to always copy 
//            using (var stream = new FileStream(Path.Combine(HttpRuntime.BinDirectory, "credentials.json"),
//                FileMode.Open, FileAccess.Read))
//            {
//                credential = GoogleCredential.FromStream(stream).CreateScoped(_scopes);
//            }

//            // Create Google Sheets API service.
//            _sheetsService = new SheetsService(new BaseClientService.Initializer()
//            {
//                HttpClientInitializer = credential,
//                ApplicationName = _applicationName
//            });
//        }

//        // Pass in your data as a list of a list (2-D lists are equivalent to the 2-D spreadsheet structure)
//        public string UpdateData(List<IList<object>> data)
//        {
//            String range = "My Tab Name!A1:Y";
//            string valueInputOption = "USER_ENTERED";

//            // The new values to apply to the spreadsheet.
//            List<Data.ValueRange> updateData = new List<Data.ValueRange>();
//            var dataValueRange = new Data.ValueRange();
//            dataValueRange.Range = range;
//            dataValueRange.Values = data;
//            updateData.Add(dataValueRange);

//            Data.BatchUpdateValuesRequest requestBody = new Data.BatchUpdateValuesRequest();
//            requestBody.ValueInputOption = valueInputOption;
//            requestBody.Data = updateData;

//            var request = _sheetsService.Spreadsheets.Values.BatchUpdate(requestBody, _spreadsheetId);

//            Data.BatchUpdateValuesResponse response = request.Execute();
//            // Data.BatchUpdateValuesResponse response = await request.ExecuteAsync(); // For async 

//            return JsonConvert.SerializeObject(response);
//        }
//    }
//}