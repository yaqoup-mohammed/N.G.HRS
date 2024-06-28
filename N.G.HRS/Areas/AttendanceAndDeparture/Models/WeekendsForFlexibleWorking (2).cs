using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class WeekendsForFlexibleWorking: Weekends
    {

        [Required]
        [Range(0, 50)]
        public int NumbersOfHours { get; set;}
        //===============================================


    }
}
