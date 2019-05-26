namespace Financee.Models
{
    public class Income : MoneyFlow
    {
        public string FromWhere { get; set; }

        public string EarnerId { get; set; }
        public virtual FinanceeUser Earner { get; set; }
    }
}