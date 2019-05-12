using Financee.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Financee.Data
{
    public class FinanceeDbContext : IdentityDbContext<FinanceeUser>
    {

    }
}