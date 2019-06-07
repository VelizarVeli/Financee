using Financee.Common.ViewModels.GoogleSheets;

namespace Financee.Services.Contracts
{
    public interface IGoogleSheetsService
    {
        GoogleSheetsViewModel MonthlyReportFromGoogleSheets();
    }
}