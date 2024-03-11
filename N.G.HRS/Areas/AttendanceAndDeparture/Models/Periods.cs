using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class Periods
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = " اسم الفترة")]
        public string PeriodsName { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "من الوقت")]
        public DateTime FromTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "الى الوقت")]
        public DateTime ToTime { get; set; }
        [Display(Name = "السبت")]
        public bool Saturday { get; set;}
        [Display(Name = "الاحد")]
        public bool SunDay { get; set;}
        [Display(Name = "الاثنين")]
        public bool Monday { get; set;}
        [Display(Name = "الثلاثاء")]
        public bool Tuesday { get; set;}
        [Display(Name = "الاربعاء")]
        public bool Wednesday { get;set;}
        [Display(Name = "الخميس")]
        public bool Thursday { get; set;}
        [Display(Name = "الجمعة")]
        public bool Friday { get; set;}
        [Range(0,24)]
        [Display(Name = "الساعات")]
        public int? Hours { get; set; }
        [Display(Name = "الدقائق")]
        public int? Muinutes { get; set; }
        //============================================
        public int? PermanenceModelsId { get; set; }
        [Display(Name = "نوع الدوام")]
        public PermanenceModels? PermanenceModels { get; set; } 
        //===============================================================
        public List<LinkingEmployeesToShiftPeriods>? LinkingEmployeesToShiftPeriodsList { get; set; }

        public List<Weekends>? WeekendsList { get; set; }
        public List<AdjustingTime>? AdjustingTimeList { get; set; }
        public List<Periods>? PeriodsList { get; set; }


        //=




    }
}
