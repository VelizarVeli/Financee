using System.Collections.Generic;

namespace Financee.Common.ViewModels.GoogleSheets
{
   public class GoogleSheetsViewModel
    {
        public GoogleSheetsViewModel()
        {
            GoogleSheetExpenditures = new HashSet<GoogleSheetExpenditureViewModel>();
            GoogleSheetIncomess = new HashSet<GoogleSheetIncomeViewModel>();
        }

        public ICollection<GoogleSheetExpenditureViewModel> GoogleSheetExpenditures { get; set; }
        public ICollection<GoogleSheetIncomeViewModel> GoogleSheetIncomess { get; set; }
    }
}