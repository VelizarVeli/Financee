using System;

namespace Financee.Models
{
    public abstract class MoneyFlow
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Money { get; set; }

        public string OwnerId { get; set; }
        public Identit
        //public IDentityUser Owner { get; set; }
    }
}
