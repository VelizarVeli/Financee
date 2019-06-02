using System.Threading.Tasks;
using Financee.Common.ViewModels;

namespace Financee.Services.Contracts
{
    public interface IAccountService
    {
        Task<MoneyFlowViewModel> MonthlyMoneyFlow(string id);
        ExpenditureModalBindingModel GetCategoryNames();
        Task AddExpenditure(ExpenditureModalBindingModel viewModel, string userId);
        Task<MoneyFlowViewModel> ViewByMonth(int id, string userId);
        Task AddIncome(IncomeModalBindingModel viewModel, string userId);
    }
}