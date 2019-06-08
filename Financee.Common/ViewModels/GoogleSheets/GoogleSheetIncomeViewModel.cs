using System;

namespace Financee.Common.ViewModels.GoogleSheets
{
    public class GoogleSheetIncomeViewModel
    {
        public DateTime Date { get; set; }
        public decimal Income { get; set; }
        public string FromWhere{ get; set; }
    }
}