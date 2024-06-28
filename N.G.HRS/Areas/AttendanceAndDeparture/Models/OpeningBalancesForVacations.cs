using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.MaintenanceControl.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class OpeningBalancesForVacations
    {
        public int Id { get; set; }

        [Display(Name = "سنة الرصيد")]
        [Required(ErrorMessage = "يجب تحديد سنة الرصيد")]
        public int BalanceYear { get; set; }
        [Display(Name = "الرصيد")]
        [Range(1, 100, ErrorMessage = "يجب ان يكون الرصيد من 1 الى 100")]
        [Required(ErrorMessage = "يجب تحديد الرصيد")]
        public int Balance { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "التاريخ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "يجب تحديد التاريخ ")]
        public DateOnly Date {  get; set; }
        [Display(Name = "الملاحظات")]
        [StringLength(255, ErrorMessage = "يجب ان لا يزيد عن 255 حرف")]
        public string? Notes { get; set; }

        //==========================================
        [ForeignKey("EmployeeId")]
        [Display(Name = "الموظف")]
        public int? EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public Employee? Employee { get; set; }
        //=
        [ForeignKey("PublicHolidaysId")]
        [Display(Name = "الاجازة")]
        public int? PublicHolidaysId { get; set; }
        [Display(Name = "الاجازة")]
        public PublicHolidays? PublicHolidays { get; set; }

    }
}
