using System;
using System.ComponentModel.DataAnnotations;

namespace Financee.Common.ViewModels
{
    public class IncomeModalBindingModel
    {
        [Display(Name = "Дата")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd-MMМ-yyyy")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Сума")]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335", ErrorMessage = "Сумата не трябва да е отрицателно число")]
        public decimal Income { get; set; }

        [Required]
        [Display(Name = "Откъде")]
        public string FromWhere { get; set; }
    }
}