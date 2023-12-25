using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class AdjustingTime//ضبط الدوام
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime FromTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime ToTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly FromDate {  get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly ToDate { get; set;}
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
    }
}
