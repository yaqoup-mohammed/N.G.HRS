using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class BasicDataForWagesAndSalaries
    {
        private int _numberOfMonthsDays;

        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, 31)]
        public int NumberOfMonthsDays
        {
            get { return _numberOfMonthsDays; }
            set
            {

                _numberOfMonthsDays = CalculateDaysInMonth();
            }
        }
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

        public int CalculateDaysInMonth()
        {

           int month = DateTime.Now.Month;
            // Assuming year is fixed or known, you can use DateTime.DaysInMonth
            int year = DateTime.Now.Year; // Replace with the actual year
            return DateTime.DaysInMonth(year, month);
        }
    }
}
