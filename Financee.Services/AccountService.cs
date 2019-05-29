﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Financee.Common.ViewModels;
using Financee.Data;
using Financee.Models;
using Financee.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            if (user != null)
            {
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
            }
            return monthlyFlow;
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

            var expenditure = new Expenditure
            {
                BudgetCategoryId = budgetCategory.Id,

                Date = viewModel.Date,
                ForWhat = viewModel.ForWhat,
                Money = viewModel.Expenditure,
                SpenderId = user.Id
            };

            DbContext.Expenditures.Add(expenditure);
            await DbContext.SaveChangesAsync();
        }

        private string WeekDayTranslateBg(DateTime date)
        {
            CultureInfo bulgarian = new CultureInfo("bg-BG");
            var weekDayInBulgarian = bulgarian.DateTimeFormat.GetDayName(date.DayOfWeek);
            return weekDayInBulgarian;
        }
    }
}