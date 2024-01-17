using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class Weekends
    {
        [Key]
        public int Id { get; set; }
        public bool SaturDay { get; set; }
        public bool SunDay { get; set; }
        public bool MonDay { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get;set; }
        public bool Thursday { get; set;}
        public bool Friday { get; set;}
        //===========================
        [ForeignKey("PermanenceModelsId")]
        public int PermanenceModelsId { get; set; }
        public PermanenceModels PermanenceModels { get; set; }
        //=
        [ForeignKey("PeriodsId")]
        public int PeriodsId { get; set; }
        public Periods Periods { get; set; }
    }
}
