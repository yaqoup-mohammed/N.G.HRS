using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class LinkingEmployeesToShiftPeriods
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfStartWork { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfEndWork { get; set;}
    }
}
