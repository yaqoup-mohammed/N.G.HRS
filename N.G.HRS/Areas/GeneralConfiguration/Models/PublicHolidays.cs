using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.MaintenanceControl.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class PublicHolidays//الاجازات العامة
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "اسم الاجازة")]
        public string HolidayName { get; set; }
        [Display(Name = "رصيد")]
        public bool Balance { get; set; }
        [Display(Name = " مدفوع الاجر")]
        public bool Paid { get; set;}
        [Range(0,31)]
        [Display(Name = " عدد الايام الافتراضية")]
        public int? DayCount { get;set; }
        [Range(0, 100)]
        [Display(Name = "مدة التدوير")]
        public int? RotationDuration { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set;}
        //=========================================
        public List<OpeningBalancesForVacations>? OpeningBalancesForVacationsList { get; set; }
        public List<StaffVacations>? StaffVacationsList { get; set; }


    }
}
