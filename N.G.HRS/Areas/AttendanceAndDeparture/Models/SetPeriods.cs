using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class SetPeriods: AdjustingTime
    {
        [Key]
        public int Id { get; set; }

    }
}
