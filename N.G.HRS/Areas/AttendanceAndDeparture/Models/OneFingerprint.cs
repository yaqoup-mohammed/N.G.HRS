using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class OneFingerprint
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        public bool OneDayFingerprint { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly FromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly ToDate { get; set;}
        [StringLength(255)]
        public string Notes { get; set; }
        //==========================================
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
