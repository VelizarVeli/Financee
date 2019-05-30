using System.Threading.Tasks;
using Financee.Common.ViewModels;
using Financee.Data;
using Financee.Models;
using Financee.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Financee.App.Controllers
{
    public class ReportsController : Controller
    {
        private readonly UserManager<FinanceeUser> _user;
        private readonly IAccountService _accountService;

        public ReportsController(FinanceeDbContext dbContext, UserManager<FinanceeUser> user, IAccountService accountService)
        {
            _user = user;
            _accountService = accountService;
        }

        [Authorize]
        public IActionResult AddExpenditure()
        {
            var viewModel = _accountService.GetCategoryNames();
            return View("AddExpenditure", viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddExpenditurePost(ExpenditureModalBindingModel model)
        {
            await _accountService.AddExpenditure(model, _user.GetUserId(User));
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Monthly(int id)
        {
            var viewByMonth = await _accountService.ViewByMonth(id, _user.GetUserId(User));
            return View("ShowReportByMonth", viewByMonth);
        }
    }
}