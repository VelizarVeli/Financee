using Financee.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Financee.Data
{
    public class FinanceeDbContext : IdentityDbContext<FinanceeUser>
    {
        public FinanceeDbContext(DbContextOptions<FinanceeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }
    }
}
