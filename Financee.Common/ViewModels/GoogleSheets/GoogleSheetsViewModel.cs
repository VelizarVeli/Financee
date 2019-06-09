using System.Collections.Generic;

namespace Financee.Common.ViewModels.GoogleSheets
{
   public class GoogleSheetsViewModel
    {
        public GoogleSheetsViewModel()
        {
            GoogleSheetExpenditures = new HashSet<GoogleSheetExpenditureViewModel>();
            GoogleSheetIncomes = new HashSet<GoogleSheetIncomeViewModel>();
        }

        public ICollection<GoogleSheetExpenditureViewModel> GoogleSheetExpenditures { get; set; }
        public ICollection<GoogleSheetIncomeViewModel> GoogleSheetIncomes { get; set; }
    }
}