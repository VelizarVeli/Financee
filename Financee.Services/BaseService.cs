using AutoMapper;
using Financee.Data;
using Financee.Models;
using Microsoft.AspNetCore.Identity;

namespace Financee.Services
{
    public class BaseService
    {
        protected BaseService(FinanceeDbContext dbContext,
            UserManager<FinanceeUser> userManager)
        {
            this.DbContext = dbContext;
            this.UserManager = userManager;
        }

        protected FinanceeDbContext DbContext { get; private set; }

        protected UserManager<FinanceeUser> UserManager { get; private set; }

        protected IMapper Mapper { get; private set; }

        //protected async Task<IdentityUser> GetUserByIdAsync(string id)
        //{
        //    var user = await this.UserManager.FindByIdAsync(id);

        //    CoreValidator.ThrowIfNull(user, new InvalidUserException());

        //    return user;
        //}
    }
}
