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

        public int? DepartmentsId { get; set; }
        [Display(Name = "الادارة")]
        public Departments? Departments { get; set; }
        //=
        [ForeignKey("SectionsId")]
        public int? SectionsId { get; set; }
        [Display(Name = "القسم")]
        public Sections? Sections { get; set; }
        //=
        [ForeignKey("EmployeeId")]
        public int? EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public Employee? Employee { get; set; }
        //=
        [ForeignKey("PermanenceModelsId")]
        public int? PermanenceModelsId { get; set; }
        [Display(Name = "الدوام")]
        public PermanenceModels? PermanenceModels { get; set; }
        //=
        [ForeignKey("PeriodsId")]
        public int? PeriodsId { get; set; }
        [Display(Name = "الفترة")]
        public Periods? Periods { get; set; }
        //=
    }
}
