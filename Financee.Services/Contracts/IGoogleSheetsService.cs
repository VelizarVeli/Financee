using Financee.Common.ViewModels;
using Financee.Common.ViewModels.GoogleSheets;

namespace Financee.Services.Contracts
{
    public interface IGoogleSheetsService
    {
        GoogleSheetsViewModel MonthlyReportFromGoogleSheets();
        GoogleSheetsViewModel ViewByMonth(int id);
        void AddExpenditureInGoogleSheets(ExpenditureModalBindingModel model);
    }
}