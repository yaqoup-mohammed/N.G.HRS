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
        [Display(Name = "المعامل الطبيعي")]
        public decimal NormalCoefficient { get; set; }//المعامل الطبيعي
        [Required]
        [Range(1, 10)]
         [Display(Name = "المعامل الاسبوعي")]
        public decimal WeekendLaboratories { get; set; }//المعامل الاسبوعي
        [Required]
        [Range(1, 10)]
        [Display(Name = "معامل العطلات الرسمية")]
        public decimal OfficialHolidaysLab { get; set; }//معامل العطلات الرسمية

        [Required]
        [Range(1, 10)]
        [Display(Name = "معامل الفترة الليلية")]
        public decimal NightPeriodParameter { get; set; }//معامل الفترة الليلية
        [Required]
        [Range(1, 10)]
        [Display(Name = "المعامل في يوم")]
        public decimal LaboratoriesPerDay { get; set; }//المعامل اليومي
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "من وقت")]
        public DateTime FromTime { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "الى وقت")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ToTime { get; set; }
        [Display(Name = " اليوم")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public int Date { get; set; }=DateTime.Now.Day;

        //=================
        [DataType(DataType.Date)]
        [Display(Name = "من تاريخ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateOnly FromDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "الى تاريخ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly ToDate { get; set; }
        [StringLength(255)]
        [Display(Name ="الملاحظات")]
        public string? Notes { get; set; }

    }
}
