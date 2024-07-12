using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EmployeePerks//اكراميات الموظفين
    {
        //private double _installmentAmount;

        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Required (ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "تاريخ الاكراميات")]
        public DateTime Date { get; set; }
        [Required (ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الموظف")]
        public int EmployeeId { get; set; }
        [Display(Name = " الموظف")]
        public Employee? Employee { get; set; }
        [Display(Name = "الوصف")]
        public string? Description { get; set; }
        [Required (ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "المبلغ")]
        public double Amount { get; set; }
        [Required (ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "نسبة ")]
        
        public int Percentage { get; set; }
        [Display(Name = "الملاحضات ")]
        public string? Notes { get; set; }

    }
}
