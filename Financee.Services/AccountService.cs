using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Financee.Common.ViewModels;
using Financee.Common.ViewModels.Enums;
using Financee.Data;
using Financee.Models;
using Financee.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Financee.Services
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(FinanceeDbContext dbContext, UserManager<FinanceeUser> user)
        : base(dbContext, user)
        {
        }

        public async Task<MoneyFlowViewModel> MonthlyExpenditures(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            var monthlyFlow = new MoneyFlowViewModel();

            monthlyFlow.Expenditures = DbContext.Expenditures
                .Where(u => u.SpenderId == user.Id)
                .Select(a => new ExpenditureViewModel
                {
                    Id = a.Id,
                    Date = a.Date,
                    Expenditure = a.Money,
                    WeekDay = WeekDayTranslateBg(a.Date),
                    WhatIsmadeFor = a.ForWhat
                });
            monthlyFlow.Incomes = DbContext.Incomes.Where(u => u.EarnerId == user.Id)
                .Select(a => new IncomeViewModel
                {

                });
            return monthlyFlow;
        }

        private string WeekDayTranslateBg(DateTime date)
        {
            CultureInfo bulgarian = new CultureInfo("bg-BG");
            var weekDayInBulgarian = bulgarian.DateTimeFormat.GetDayName(date.DayOfWeek);
            return weekDayInBulgarian;
        }
    }
}