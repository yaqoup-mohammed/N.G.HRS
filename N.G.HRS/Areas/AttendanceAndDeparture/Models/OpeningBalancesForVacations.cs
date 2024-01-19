using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class OpeningBalancesForVacations
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Balance Year")]
        public DateOnly BalanceYear { get; set; }
        public int Balance { get; set; }
        public DateOnly Date {  get; set; }
        public string? Notes { get; set; }
        //==========================================
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        //=
        [ForeignKey("PublicHolidaysId")]
        public int PublicHolidaysId { get; set; }
        public PublicHolidays PublicHolidays { get; set; }

    }
}
