using System.Threading.Tasks;
using Financee.Common.ViewModels;
using Financee.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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

        public IActionResult Monthly(int id)
        {
            var viewByMonth = _googleSheetsService.ViewByMonth(id);
            return View("ShowReportByMonth", viewByMonth);
        }

        [Authorize]
        public IActionResult AddExpenditureInGoogleSheets()
        {
            //var viewModel = _googleSheetsService.GetCategoryNames();
            return View("AddExpenditureInGoogleSheets"/*, viewModel*/);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddExpenditureInGoogleSheetsPost(ExpenditureModalBindingModel model)
        {
            _googleSheetsService.AddExpenditureInGoogleSheets(model);
            return RedirectToAction("GoogleSheets");
        }
    }
}