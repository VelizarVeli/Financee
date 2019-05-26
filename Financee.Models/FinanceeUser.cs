using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Financee.Models
{
    public class FinanceeUser : IdentityUser
    {
        public FinanceeUser()
        {
            Expenditures = new List<Expenditure>();
            Incomes = new List<Income>();
        }

        public ICollection<Expenditure> Expenditures { get; set; }
        public ICollection<Income> Incomes { get; set; }
    }
}