using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class Months
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly Month { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly EndDate { get; set;}


    }
}
