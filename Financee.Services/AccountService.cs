using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Financee.Common.ViewModels;
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

        public async Task<MoneyFlowViewModel> MonthlyMoneyFlow(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            var monthlyFlow = new MoneyFlowViewModel();
            var currentMonth = DateTime.Now.Month;

            if (user != null)
            {
                monthlyFlow.Expenditures = DbContext.Expenditures
                    .Where(u => u.SpenderId == user.Id && u.Date.Month == currentMonth)
                    .Select(a => new ExpenditureViewModel
                    {
                        Id = a.Id,
                        Date = a.Date,
                        Expenditure = a.Money,
                        WeekDay = WeekDayTranslateBg(a.Date),
                        WhatIsmadeFor = a.ForWhat
                    })
                    .OrderBy(m => m.Date);
                monthlyFlow.Incomes = DbContext.Incomes
                    .Where(u => u.EarnerId == user.Id && u.Date.Month == currentMonth)
                    .Select(a => new IncomeViewModel
                    {
                        Id = a.Id,
                        Date = a.Date,
                        Income = a.Money,
                        WeekDay = WeekDayTranslateBg(a.Date),
                        WhereFrom = a.FromWhere
                    })
                    .OrderBy(m => m.Date);
            }

            monthlyFlow.AvailableMoney = Math.Truncate(DbContext.Incomes
                                                           .Where(u => u.EarnerId == id && u.Date.Month <= currentMonth)
                                                           .Sum(i => i.Money) - DbContext.Expenditures
                                                           .Where(u => u.SpenderId == id && u.Date.Month <= currentMonth)
                                                           .Sum(e => e.Money)); return monthlyFlow;
        }

        public ExpenditureModalBindingModel GetCategoryNames()
        {
            var allCategories = DbContext.BudgetCategories;
            var viewModel = new ExpenditureModalBindingModel();
            foreach (var category in allCategories)
            {
                viewModel.CurrentCategories.Add(category.Name);
            }

            return viewModel;
        }

        public async Task AddExpenditure(ExpenditureModalBindingModel viewModel, string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var budgetCategory = DbContext.BudgetCategories.FirstOrDefault(n => n.Name == viewModel.Category);

            if (budgetCategory != null)
            {
                var expenditure = new Expenditure
                {
                    BudgetCategoryId = budgetCategory.Id,

                    Date = viewModel.Date,
                    ForWhat = viewModel.ForWhat,
                    Money = viewModel.Expenditure,
                    SpenderId = user.Id
                };

                DbContext.Expenditures.Add(expenditure);
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task AddIncome(IncomeModalBindingModel viewModel, string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);

            var income = new Income()
            {
                Date = viewModel.Date,
                FromWhere = viewModel.FromWhere,
                Money = viewModel.Income,
                EarnerId = user.Id
            };

            DbContext.Incomes.Add(income);
            await DbContext.SaveChangesAsync();
        }

        public async Task<MoneyFlowViewModel> ViewByMonth(int id, string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);

            var monthlyFlow = new MoneyFlowViewModel();

            if (user != null)
            {
                monthlyFlow.Expenditures = DbContext.Expenditures
                    .Where(u => u.SpenderId == user.Id && u.Date.Month == id)
                    .Select(a => new ExpenditureViewModel
                    {
                        Id = a.Id,
                        Date = a.Date,
                        Expenditure = a.Money,
                        WeekDay = WeekDayTranslateBg(a.Date),
                        WhatIsmadeFor = a.ForWhat
                    })
                    .OrderBy(m => m.Date);
                monthlyFlow.Incomes = DbContext.Incomes
                    .Where(u => u.EarnerId == user.Id && u.Date.Month == id)
                    .Select(a => new IncomeViewModel
                    {
                        Id = a.Id,
                        Date = a.Date,
                        Income = a.Money,
                        WeekDay = WeekDayTranslateBg(a.Date),
                        WhereFrom = a.FromWhere
                    })
                    .OrderBy(m => m.Date);
            }
            monthlyFlow.AvailableMoney = Math.Truncate(DbContext.Incomes
                                                           .Where(u => u.EarnerId == userId && u.Date.Month <= id)
                                                           .Sum(i => i.Money) - DbContext.Expenditures
                                                           .Where(u => u.SpenderId == userId && u.Date.Month <= id)
                                                           .Sum(e => e.Money));
            return monthlyFlow;
        }

        public async Task DeleteExpenditure(long id)
        {
            var expenditure = DbContext.Expenditures.FirstOrDefault(e => e.Id == id);
            if (expenditure != null)
            {
                DbContext.Expenditures.Remove(expenditure);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteIncome(long id)
        {
            var income = DbContext.Incomes.FirstOrDefault(e => e.Id == id);
            if (income != null)
            {
                DbContext.Incomes.Remove(income);
                await DbContext.SaveChangesAsync();
            }
        }

        private string WeekDayTranslateBg(DateTime date)
        {
            CultureInfo bulgarian = new CultureInfo("bg-BG");
            var weekDayInBulgarian = bulgarian.DateTimeFormat.GetDayName(date.DayOfWeek);
            return weekDayInBulgarian;
        }
    }
}