using System.Threading.Tasks;
using Financee.Common.ViewModels;
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

        public ReportsController(UserManager<FinanceeUser> user, IAccountService accountService)
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

        [Authorize]
        public IActionResult AddIncome()
        {
            var viewModel = new IncomeModalBindingModel();
            return View("AddIncome", viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddIncomePost(IncomeModalBindingModel model)
        {
            await _accountService.AddIncome(model, _user.GetUserId(User));
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult AddCategory()
        {
            return View("AddCategory");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCategoryPost(CategoryViewModel model)
        {
            await _accountService.AddCategory(model);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Monthly(int id)
        {
            var viewByMonth = await _accountService.ViewByMonth(id, _user.GetUserId(User));
            return View("ShowReportByMonth", viewByMonth);
        }

        public async Task<IActionResult> DeleteExpenditure(long id)
        {
            await _accountService.DeleteExpenditure(id);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteIncome(long id)
        {
            await _accountService.DeleteIncome(id);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult MonthlyBudget()
        {
            var viewModel = _accountService.GetMonthlyBudget(_user.GetUserId(User));
            return View("MonthlyBudget", viewModel);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeGoal(MonthlyBudgetsViewModel model)
        {
            await _accountService.ChangeGoal(model);
            return RedirectToAction("MonthlyBudget");
        }
    }
}