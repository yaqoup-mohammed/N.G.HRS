using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Models
{
    public class AdditionalAccountInformation
    {
        public int ID { get; set; }
        [Required]
        [Range(0, 100)]
        public int NormalParameter { get; set; }
        [Required]
        [Range(0, 100)]
        public int WeekendParameter { get; set; }
        [Required]
        [Range(0, 100)]

        public int OfficialHolidaysParameter { get; set; }
        [Required]
        [Range(0, 100)]
        public int NightPeriodParameter { get; set; }
        [Required]
        [Range(0, 100)]
        public int InDayParameter { get; set; }
        [Required]
        [Display(Name = "From Time")]
        public TimeOnly FromTime { get; set;}
        [Display(Name = "To Time")]
        public TimeOnly ToTime { get; set;}
        [DataType(DataType.Date)]
        [Display(Name = "Day")]
        public DateOnly Day { get; set; }
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
