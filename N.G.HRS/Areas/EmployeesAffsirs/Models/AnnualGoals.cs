using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.EmployeesAffsirs.Models
{
    public class AnnualGoals
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "الرجاء تحديد التاريخ")]
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        //=======================================================
        [Required (ErrorMessage = "الرجاء تحديد الموظف")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        //========================================================
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }
        [Display(Name = "الهدف")]
        public string? Goals { get; set; }
    }
}
