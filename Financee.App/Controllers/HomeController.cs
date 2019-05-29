using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Financee.App.Models;
using Financee.Models;
using Financee.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Financee.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<FinanceeUser> _user;
        private readonly IAccountService _accountService;

        public HomeController(UserManager<FinanceeUser> user, IAccountService accountService)
        {
            _user = user;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var monthlyReport = await _accountService.MonthlyExpenditures(_user.GetUserId(User));
            return View("Index", monthlyReport);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
