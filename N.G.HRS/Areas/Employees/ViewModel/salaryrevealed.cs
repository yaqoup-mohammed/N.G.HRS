using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Areas.PayRoll.Models;

namespace N.G.HRS.Areas.Employees.ViewModel
{
    public class salaryrevealed
    {
        //public bool migration { get; set; }
        public DateTime Month { get; set; }
        public Employee Employee { get; set; }
        public Salaries Salaries { get; set; }

        public List<Salaries>? Salarieslist { get; set; }
        public List<Employee>? Employeeslist { get; set; }
        public Sections Section { get; set; }
        public List<Sections> SectionList { get; set; }
        public Departments Departments { get; set; }
        public List<Departments>? Departmentslist { get; set; } 
        public FinancialStatements FinancialStatements { get; set; }
        public List<FinancialStatements>? FinancialStatementslist { get; set; }


    }
}
