using System;

namespace Financee.Models
{
    public abstract class MoneyFlow
    {
        public long Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public decimal Money { get; set; }

        public string OwnerId { get; set; }
        public FinanceeUser Owner { get; set; }
    }
}