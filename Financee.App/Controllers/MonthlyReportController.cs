using System.Linq;
using System.Threading.Tasks;
using Financee.Data;
using Financee.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace Financee.App.Controllers
{
    public class MonthlyReportController : Controller
    {
        private readonly FinanceeDbContext dbContext;
        private readonly IdentityUser user;
        private readonly IAccountService accountService;
        public MonthlyReportController(FinanceeDbContext dbContext, IdentityUser user, IAccountService accountService)
        {
            this.dbContext = dbContext;
            this.user = user;
            this.accountService = accountService;
        }

        [Authorize]
        public async Task<IActionResult> MonthlyReport()
        {
            var all =  await this.accountService.AllExpenditures(this.user.Id);

            return RedirectToAction("Index", all);
        }
    }
}