using Microsoft.AspNetCore.Identity;

namespace Financee.Models
{
    public class FinanceeUser : IdentityUser
    {
        public string Nickname { get; set; }
    }
}