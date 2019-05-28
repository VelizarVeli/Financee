using System.Collections.Generic;

namespace Financee.Common.ViewModels
{
    public class MoneyFlowViewModel
    {
        public IEnumerable<ExpenditureViewModel> Expenditures { get; set; }
        public IEnumerable<IncomeViewModel> Incomes { get; set; }
    }
}
