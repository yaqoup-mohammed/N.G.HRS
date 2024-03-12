using N.G.HRS.Areas.AttendanceAndDeparture.Models;

namespace N.G.HRS.Areas.AttendanceAndDeparture.ViewModels
{
    public class PermanenceModelsAndPeriodsVM
    {
        public PermanenceModels permanenceModels { get; set; }
        public Periods? periods { get; set; }
        public List<PermanenceModels> permanenceModelsList { get; set; } 
        public List<Periods> periodsList { get; set; } 

    }
}
