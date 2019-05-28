using System.Threading.Tasks;
using Financee.Common.ViewModels;

namespace Financee.Services.Contracts
{
    public interface IAccountService
    {
        Task<MoneyFlowViewModel> MonthlyExpenditures(string id);
    }
}