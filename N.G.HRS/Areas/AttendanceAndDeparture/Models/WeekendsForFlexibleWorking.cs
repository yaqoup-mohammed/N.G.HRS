using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class WeekendsForFlexibleWorking: Weekends
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, 50)]
        public int NumbersOfHours { get; set;}
    }
}
