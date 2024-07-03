using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;

namespace N.G.HRS.Areas.Employees.ViewModel
{
    public class salaryrevealed
    {
        public bool migration { get; set; }

        public Employee Employee { get; set; }
        public List<Employee>? Employeeslist { get; set; }

        public Section Section { get; set; }
        public List<Section>? Sectionlist { get; set; }

        public Departments Departments { get; set; }
        public List<Departments>? Departmentslist { get; set; } 
        public Departments FinancialStatements { get; set; }
        public List<Departments>? FinancialStatementslist { get; set; }


    }
}
