using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EmployeeAdvances//سلف الموظفين
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int DepartmentId { get; set; }
        public Departments Departments { get; set; }
        public int SectionId { get; set; }
        public Sections Sections { get; set; }
        public int EmployeeAccountId { get; set; }
        public EmployeeAccount EmployeeAccount { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }
}
