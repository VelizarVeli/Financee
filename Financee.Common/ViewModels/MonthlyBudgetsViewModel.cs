using System.Collections.Generic;

namespace Financee.Common.ViewModels
{
   public class MonthlyBudgetsViewModel
    {
        public MonthlyBudgetsViewModel()
        {
            CurrentMonthlyBudgets = new List<MonthlyBudgetViewModel>();
        }

        public ICollection<MonthlyBudgetViewModel> CurrentMonthlyBudgets { get; set; }
    }
}
