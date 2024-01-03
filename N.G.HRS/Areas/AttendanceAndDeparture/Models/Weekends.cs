using System.ComponentModel.DataAnnotations;

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
        public List<PermanenceModels> PermanenceModelsList {  get; set; }
        public List<Periods> PeriodsList { get; set; }
    }
}
