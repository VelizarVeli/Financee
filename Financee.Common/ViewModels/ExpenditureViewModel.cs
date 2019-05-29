using System;
using System.Collections.Generic;

namespace Financee.Common.ViewModels
{
    public class ExpenditureViewModel
    {
        public long Id { get; set; }
        public string WeekDay { get; set; }
        public DateTime Date { get; set; }
        public decimal Expenditure { get; set; }
        public string WhatIsmadeFor { get; set; }

        public string Category { get; set; }
        public ICollection<string> CurrentCategories { get; set; } = new HashSet<string>();
    }
}