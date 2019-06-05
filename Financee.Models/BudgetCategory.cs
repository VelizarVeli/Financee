using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Financee.Models
{
   public class BudgetCategory
    {
        public BudgetCategory()
        {
            Expenditures = new List<Expenditure>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal SetGoal { get; set; }

        public ICollection<Expenditure> Expenditures { get; set; }
    }
}