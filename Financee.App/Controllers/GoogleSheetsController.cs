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

        //[Authorize]
        //public IActionResult AddExpenditureInGoogleSheets()
        //{
        //    var viewModel = _googleSheetsService.GetCategoryNames();
        //    return View("AddExpenditureInGoogleSheets", viewModel);
        //}

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> AddExpenditureInGoogleSheetsPost(ExpenditureModalBindingModel model)
        //{
        //    await _googleSheetsService.AddExpenditureInGoogleSheets(model, _user.GetUserId(User));
        //    return RedirectToAction("Index", "Home");
        //}
    }
}