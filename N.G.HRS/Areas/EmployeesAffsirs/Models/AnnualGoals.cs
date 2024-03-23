using N.G.HRS.Areas.Employees.Models;

namespace N.G.HRS.Areas.EmployeesAffsirs.Models
{
    public class AnnualGoals
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public string? Notes { get; set; }
        public string? Goals { get; set; }
    }
}
