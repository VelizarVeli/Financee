using System;

namespace Financee.Common.ViewModels
{
    public class ExpenditureViewModel
    {
        public long Id { get; set; }
        public string WeekDay { get; set; }
        public DateTime Date { get; set; }
        public decimal Expenditure { get; set; }
        public string WhatIsmadeFor { get; set; }
    }
}