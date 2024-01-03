using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class OpeningBalancesForVacations
    {
        public int Id { get; set; }
        public DateOnly BalanceYear { get; set; }
        public int Balance { get; set; }
        public DateOnly Date {  get; set; }
        public string? Notes { get; set; }
        //==========================================
        public List<Employee> EmployeeList { get; set; }
        public List<PublicHolidays> publicHolidaysList { get; set; }

    }
}
