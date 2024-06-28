using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class PermissionToAttendAndLeave
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string PermanencyStatus { get; set; }
        [Required]
        [Range(0, 50)]
        public int Duration { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }


    }
}
