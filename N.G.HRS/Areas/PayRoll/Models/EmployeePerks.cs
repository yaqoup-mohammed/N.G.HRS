using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EmployeePerks//اكراميات الموظفين
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "هذا الحقل مطلوبي")]
        [Display(Name = "التاريخ")]
        public DateTime Date { get; set; }

        public int? EmployeeId { get; set; }
        //===================================================
        [Display(Name = "الموظف")]

        public Employee? Employee { get; set; }
        //===================================================
        [Required(ErrorMessage = "هذا الحقل مطلوبي")]
        [Display(Name = "الوصف")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوبي")]
        [Display(Name = "الكمية")]
        public double? Amount { get; set; }
        [Range(1, 100)]

        //[Required(ErrorMessage = "هذا الحقل مطلوبي")]
        [Display(Name = "النسبة")]

        public int? Percentage { get; set; }

        [Display(Name = "ملاحضات")]
        public string? Notes { get; set; }

    }
}
