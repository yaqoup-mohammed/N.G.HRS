using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.MaintenanceControl.Models
{
    public class AttendanceRecord//حافظة الدوام
    {
        public int Id { get; set; }
        [Display(Name = "  القسم")]
        public int? SectionId { get; set; }
        [Display(Name = "  القسم")]
        public virtual Sections? Sections { get; set; }
        [Display(Name = "الموظف")]
        public int? EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public virtual Employee? Employee { get; set; }
        [Display(Name = "الفترة")]
        public int? PeriodsId { get; set; }
        [Display(Name = "الفترة")]
        public virtual Periods? Period { get; set; }
        [Display(Name = "الوقت")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss tt}")]
        public DateTime TimeOnlyRecord { get; set; }
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "yyyy-MM-dd")]
        public DateTime Date { get; set; }
        [Display(Name = "الملاحظة")]
        public string? Note { get; set; }
    }
    
}
