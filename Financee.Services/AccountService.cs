using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Financee.Data;
using Financee.Models.ViewModels;
using Financee.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Financee.Services
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(FinanceeDbContext dbContext, IMapper mapper, UserManager<IdentityUser> user)
        : base(dbContext, mapper, user)
        {
        }

        public async Task<IEnumerable<AccountViewModel>> MonthlyExpenditures(string id)
        {
            var user = await this.UserManager.FindByIdAsync(id);

            var monthlyExpenditure = this.Mapper.Map<IEnumerable<AccountViewModel>>(
                this.DbContext.Expenditures.Where(e => e.SpenderId == user.Id
                                                       && e.Date.Year == DateTime.Now.Year
                                                       && e.Date.Month == DateTime.Now.Month)
            );
            return monthlyExpenditure;
        }
    }
}
