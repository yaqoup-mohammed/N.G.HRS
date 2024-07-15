using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.MaintenanceControl.Models
{
    public class StaffVacations//اجازات الموظفين
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage="هذا الحقل مطلوب!!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="تاريخ الطلب")]
        public DateTime Date { get; set; }
        [Display(Name = " نوع الأجازة")]
        public int? VacationId { get; set; }
        [Display(Name = " نوع الأجازة")]
        public virtual PublicHolidays? Vacation { get; set; }
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
        [Display(Name = "متصلة")]
        public bool IsConnected { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "من تاريخ")]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "الى تاريخ")]
        public DateTime? ToDate { get; set; }
        [Display(Name = "باليوم")]
        public int PerDay { get; set; }
        [Display(Name = "بالساعة")]
        public int PerHour { get; set; }
        [Display(Name = "بالدقيقة")]
        public int PerMinute { get; set; } 

        [Display(Name = "تم قبول الطلب")]
        public bool Accepted { get; set; }
        [Display(Name = "الموظف البديل")]
        public int? SubstituteStaffMemberId { get; set; }
        [Display(Name = "الموظف البديل")]
        public virtual Employee? SubstituteStaffMember { get; set; }
        [Display(Name = "جهة منح الاجازة")]
        public string? DonorSide { get; set; }
        [Display(Name = "سبب الاجازة")]
        public string? Reason { get; set; }
        [Display(Name = "الملاحظات")]
        public bool IsProcssessed { get; set; }
        [Display(Name = "الملاحظات")]
        public string? Note { get; set; }




    }
}
