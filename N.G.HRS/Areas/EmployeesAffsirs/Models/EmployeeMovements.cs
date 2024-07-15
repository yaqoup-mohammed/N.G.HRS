using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Controllers;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.EmployeesAffsirs.Models
{
    public class EmployeeMovements  
    {

        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        public DateTime  Date { get; set; }
        [Required (ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "تاريخ النقل")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDown { get; set; }
        [Display(Name = "ملاحظات")]
        public string? Note { get; set; }


        //==============================================
        [Display(Name = "الموظف")]
        public int? EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public Employee? Employee { get; set; }
        //==============================================
        [Display(Name = "الوظيفة")]
        public int? jopdescriptionId { get; set; }

        public JobDescription? jopdescription { get; set; }


        [Display(Name = "الوظيفة الحالية")]
        public string? CurrentJop { get; set; }
        [Display(Name = "الوظيفة السابقة")]
        public string? LastJop { get; set; }
    }
}
