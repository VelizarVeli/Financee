using System;

namespace Financee.Common.ViewModels
{
   public class MonthlyBudgetViewModel
    {
        public Guid Id { get; set; }
        public string Item { get; set; }
        public decimal SetGoal { get; set; }
        public decimal CurrentSum { get; set; }
    }
}