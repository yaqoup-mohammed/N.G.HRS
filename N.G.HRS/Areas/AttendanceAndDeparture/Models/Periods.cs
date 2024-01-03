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
        public string PeriodsName { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime FromTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime ToTime { get; set; }

        public bool Saturday { get; set;}
        public bool SunDay { get; set;}
        public bool Monday { get; set;}
        public bool Tuesday { get; set;}
        public bool Wednesday { get;set;}
        public bool Thursday { get; set;}
        public bool Friday { get; set;}
        [Required]
        [Range(0,50)]
        public int Hours { get; set; }
        //============================================
        [ForeignKey("LinkingEmployeesToShiftPeriodsId")]
        public int LinkingEmployeesToShiftPeriodsId { get; set; }
        public LinkingEmployeesToShiftPeriods linkingEmployeesToShiftPeriods { get; set; }
        //=
        [ForeignKey("WeekendsId")]
        public int WeekendsId { get; set; }
        public Weekends weekends { get; set; }



    }
}
