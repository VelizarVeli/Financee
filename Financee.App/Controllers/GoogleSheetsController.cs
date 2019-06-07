using Financee.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Financee.App.Controllers
{
    public class GoogleSheetsController : Controller
    {
        private readonly IGoogleSheetsService _googleSheetsService;

        public GoogleSheetsController(IGoogleSheetsService googleSheetsService)
        {
            _googleSheetsService = googleSheetsService;
        }

        public IActionResult GoogleSheets()
        {
            var monthlyReportFromGoogleSheets = _googleSheetsService.MonthlyReportFromGoogleSheets();
            return View("GoogleSheets", monthlyReportFromGoogleSheets);
        }
    }
}