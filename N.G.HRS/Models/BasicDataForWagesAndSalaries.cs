using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace N.G.HRS.Models
{
    public class BasicDataForWagesAndSalaries
    {
        public int Id { get; set; }
        [Required]
        [Range(0, 31)]
        public int NumberOfMonthsDays { get; set; }
        [Required]
        [Range(0, 9999.99)]
        public Decimal AbsencePerHour { get; set;}
        [Required]
        [Range(0, 9999.99)]
        public Decimal DelayPerHour { get; set;}
        [Required]
        [Range(0, 9999.99)]
        public Decimal OneFingerPrintPerHourDelay { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateOnly FromDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateOnly ToDate { get; set; }
        [Required]
        [StringLength(150)]
        public string TypeOfWage { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }


    }
}
