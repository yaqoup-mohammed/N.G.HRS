using System.Runtime.ConstrainedExecution;

namespace N.G.HRS.Models
{
    public class BasicDataForWagesAndSalaries
    {
        public int Id { get; set; }
        public Decimal NumberOfMonthsDays { get; set; }
        public Decimal AbsencePerHour { get; set;}
        public Decimal DelayPerHour { get; set;}
        public Decimal OneFingerPrintPerHourDelay { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string TypeOfWage { get; set; }
        public string Notes { get; set; }


    }
}
