using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class AutomaticallyApprovedAdd_on
    {
     


        public int Id { get; set; }
        [Required (ErrorMessage = "حقل  القسم مطلوب")]
        [Display(Name = "القسم")]
        public int? SectionsId { get; set; }
        [Display(Name = "القسم")]
        public Sections? Sections { get; set; }
        [Required(ErrorMessage = "حقل  الموظف مطلوب")]
        [Display(Name = "الموظف")]

        public int? EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public Employee? Employee { get; set; }

        [Required(ErrorMessage = "حقل  التاريخ مطلوب")]
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "حقل  هذا مطلوب")]
        [Display(Name = "من التاريخ")]
        public DateOnly FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "حقل  هذا مطلوب")]
        [Display(Name = "إلى التاريخ")]
        public DateOnly ToDate { get; set; }
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "حقل  هذا مطلوب")]
        [Display(Name = "من الوقت")]
        public TimeOnly FromTime { get; set; }
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "حقل  هذا مطلوب")]
        [Display(Name = "إلى الوقت")]
        public TimeOnly ToTime { get; set; }
        [Required]
        [Display ( Name =" الساعات")]
      
        
        public double? Hours { get; set; }
        [ Required ]

        [Display(Name = " الدقائق ")]

        public int? Minutes { get; set; }







    }
}
