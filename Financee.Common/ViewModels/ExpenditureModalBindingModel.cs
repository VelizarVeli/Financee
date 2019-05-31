using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Financee.Common.ViewModels
{
    public class ExpenditureModalBindingModel
    {
        [Display(Name = "Дата")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd-MMМ-yyyy")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Сума")]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335", ErrorMessage = "Сумата не трябва да е отрицателно число")]
        public decimal Expenditure { get; set; }

        [Required]
        [Display(Name = "За какво е направен")]
        public string ForWhat { get; set; }

        [Required]
        [Display(Name = "Перо от бюджета")]
        public string Category { get; set; }
        public ICollection<string> CurrentCategories { get; set; } = new HashSet<string>();
    }
}