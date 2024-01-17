using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class BasicDataForWagesAndSalaries
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, 31)]
        public int NumberOfMonthsDays { get; set; }
        [Required]
        [Range(0, 9999.99)]
        public decimal AbsencePerHour { get; set; }
        [Required]
        [Range(0, 9999.99)]
        public decimal DelayPerHour { get; set; }
        [Required]
        [Range(0, 9999.99)]
        public decimal OneFingerPrintPerHourDelay { get; set; }
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
