using System;

namespace Financee.Common.ViewModels.GoogleSheets
{
    public class GoogleSheetExpenditureViewModel
    {
        public DateTime Date { get; set; }
        public decimal Expenditure { get; set; }
        public string ForWhat { get; set; }
    }
}