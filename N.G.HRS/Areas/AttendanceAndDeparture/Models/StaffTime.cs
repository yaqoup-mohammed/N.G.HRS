using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class StaffTime//دوام الموظفين
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "  تاريخ بدأ الدوام")]

        public DateOnly WorksFullTimeFromDate { get; set; }
        //====================================================
        [Display(Name = "  الموظف")]

        [ForeignKey("EmployeeId")]
        public int ?EmployeeId { get; set; }
        [Display(Name = "  الموظف")]

        public Employee? Employee { get; set; }
        //=
        [Display(Name = "  الدوام")]

        [ForeignKey("PermanenceModelsId")]
        public int? PermanenceModelsId { get; set; }
        [Display(Name = "  الدوام")]

        public PermanenceModels? PermanenceModels { get; set; }
        //=
        [Display(Name = "  القسم")]

        [ForeignKey("SectionsId")]
        public int? SectionsId { get; set; }
        [Display(Name = "  القسم")]

        public Sections? Sections { get; set; }
        [Display(Name = "  الفترة")]

        [ForeignKey("PeriodId")]
        public int? PeriodId { get; set; }
        [Display(Name = "  الفترة")]

        public Periods? Periods { get; set; }



    }
}
