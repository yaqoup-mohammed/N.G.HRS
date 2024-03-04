using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class AdditionalAccountInformation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, 10)]
        public decimal NormalCoefficient { get; set; }//المعامل الطبيعي
        [Required]
        [Range(1, 10)]
        public decimal WeekendLaboratories { get; set; }//المعامل الاسبوعي
        [Required]
        [Range(1, 10)]
        public decimal OfficialHolidaysLab { get; set; }//معامل العطلات الرسمية
        [Required]
        [Range(1, 10)]
        public decimal NightPeriodParameter { get; set; }//معامل الفترة الليلية
        [Required]
        [Range(1, 10)]
        public decimal LaboratoriesPerDay { get; set; }//المعامل اليومي
        [DataType(DataType.Time)]
        [Display(Name = "From Time")]
        public DateTime FromTime { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "To Time")]
        public DateTime ToTime { get; set; }
        //=================
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateOnly FromDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateOnly ToDate { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }

    }
}
