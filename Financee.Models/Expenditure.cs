namespace Financee.Models
{
    public class Expenditure : MoneyFlow
    {
        public string ForWhat { get; set; }

        public string SpenderId { get; set; }
        public virtual  FinanceeUser Spender { get; set; }
    }
}