using Microsoft.Graph;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.MaintenanceControl.Models
{
    public class EmployeePermissions
    {
        public int Id { get; set; }
        //=============================================
        [Display(Name = "الموظف")]
        public int EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public Employee? Employee { get; set; }
        [Display(Name = "المشرف المباشر")]
        public int? SupervisorId { get; set; }
        [Display(Name = "المشرف المباشر")]
        public Employee? Supervisor { get; set; }
        [Display(Name = "الفترة")]
        public int PeriodId { get; set; }
        [Display(Name = "الفترة")]
        public Periods? Period { get; set; }
        [Display(Name = "الأذونات")]
        public int? PermissionId { get; set; }
        [Display(Name = "الأذونات")]
        public Permissions? Permission { get; set; }
        //=============================================
        [Display(Name = "التاريخ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "من تاريخ")]
        public DateTime FromDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "إلى تاريخ")]
        public DateTime? ToDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        [Display(Name = "من الوقت")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime FromTime { get; set; }
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        [Display(Name = "إلى الوقت")]
        public DateTime ToTime { get; set; }
        [Display(Name = "المدة")]
        public double? Duration { get; set; }
        [Display(Name = "الساعات")]
        public double? Hours { get; set; }
        [Display(Name = "الدقائق")]
        public int? Minutes { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "السبب")]
        public string? Reason { get; set; }
        [Display(Name = "بين تاريخين")]

        public bool BetweenToDate { get; set; }
        [Display(Name = "الملاحظات")]
        public string? Note { get; set; }
        public bool IsProccessed { get; set; }




    }
}