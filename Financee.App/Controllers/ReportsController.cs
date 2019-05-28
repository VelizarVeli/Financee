using System.Threading.Tasks;
using Financee.Data;
using Financee.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Financee.App.Controllers
{
    public class ReportsController : Controller
    {
        private readonly UserManager<IdentityUser> _user;
        private readonly IAccountService _accountService;
        public ReportsController(FinanceeDbContext dbContext, UserManager<IdentityUser> user, IAccountService accountService)
        {
            _user = user;
            _accountService = accountService;
        }

        [Authorize]
        public async Task<IActionResult> MonthlyReport()
        {
            var monthlyReport = await _accountService.MonthlyExpenditures(_user.GetUserId(User));
            return View("_MonthlyReport", monthlyReport);
        }
    }
}