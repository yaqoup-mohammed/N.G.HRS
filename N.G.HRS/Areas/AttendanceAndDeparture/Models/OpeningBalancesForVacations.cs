using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class OpeningBalancesForVacations
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "سنة الرصيد")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-YYYY}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "يجب تحديد سنة الرصيد")]
        public DateOnly BalanceYear { get; set; } 
        [Display(Name = "الرصيد")]
        [Range(0, 500, ErrorMessage = "يجب ان يكون الرصيد من 0 الى 500")]
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
        public Employee? Employee { get; set; }
        //=
        [ForeignKey("PublicHolidaysId")]
        [Display(Name = "الاجازة")]
        public int? PublicHolidaysId { get; set; }
        public PublicHolidays? PublicHolidays { get; set; }

    }
}
