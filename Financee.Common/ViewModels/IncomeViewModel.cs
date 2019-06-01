using System;

namespace Financee.Common.ViewModels
{
    public class IncomeViewModel
    {
        public long Id { get; set; }
        public string WeekDay { get; set; }
        public DateTime Date { get; set; }
        public decimal Income { get; set; }
        public string WhereFrom { get; set; }
    }
}