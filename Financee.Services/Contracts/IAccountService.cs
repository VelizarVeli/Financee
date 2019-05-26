using System.Collections.Generic;
using System.Threading.Tasks;
using Financee.Models.ViewModels;

namespace Financee.Services.Contracts
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountViewModel>> MonthlyExpenditures(string id);
    }
}