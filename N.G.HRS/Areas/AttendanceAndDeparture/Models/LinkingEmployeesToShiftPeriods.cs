using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class LinkingEmployeesToShiftPeriods
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "من تاريخ")]
        public DateOnly DateOfStartWork { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "الى تاريخ")]
        public DateOnly DateOfEndWork { get; set;}
        //===================================================
        //=
        [ForeignKey("DepartmentsId")]
        [Display(Name = "الادارة")]
        public int? DepartmentsId { get; set; }
        public Departments? Departments { get; set; }
        //=
        [ForeignKey("SectionsId")]
        [Display(Name = "القسم")]
        public int? SectionsId { get; set; }
        public Sections? Sections { get; set; }
        //=
        [ForeignKey("EmployeeId")]
        [Display(Name = "الموظف")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        //=
        [ForeignKey("PermanenceModelsId")]
        [Display(Name = "الدوام")]
        public int? PermanenceModelsId { get; set; }
        public PermanenceModels? PermanenceModels { get; set; }
        //=
        [ForeignKey("PeriodsId")]
        [Display(Name = "الفترة")]
        public int? PeriodsId { get; set; }
        public Periods? Periods { get; set; }
        //=
    }
}
