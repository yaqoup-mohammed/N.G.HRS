using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.EmployeesAffsirs.Models
{
    public class EmploymentStatusManagement
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("EmployeeId")]
        [Display(Name = "الموظف")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public Employee? Employee { get; set; }
        [Display(Name = "حالة الموظف")]
        public string? EmployeeStatus { get; set; }
        [Display(Name = "تاريخ الحالة")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StatusDate { get; set; }
        [Display(Name = "ملاحظات")]
        public string? Note { get; set; }


    }
}
