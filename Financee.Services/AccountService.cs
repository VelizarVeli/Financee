using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Financee.Models.ViewModels;
using Financee.Services.Contracts;

namespace Financee.Services
{
    class AccountService : IAccountService
    {
        public Task<IEnumerable<AccountViewModel>> AllExpenditures(string id)
        {

        }
    }
}
