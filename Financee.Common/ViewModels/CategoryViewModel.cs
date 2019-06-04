using System.ComponentModel.DataAnnotations;

namespace Financee.Common.ViewModels
{
   public class CategoryViewModel
    {
        [Display(Name = "Добави Бюджетна категория")]
        public string Category { get; set; }
    }
}