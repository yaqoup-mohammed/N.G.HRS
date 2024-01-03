using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class LinkingEmployeesToShiftPeriods
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfStartWork { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfEndWork { get; set;}
        //===================================================
        public List<Departments> DepartmentsList { get; set; }
        public List<Employee> EmployeeList { get; set; }
        public List<Sections> SectionsList { get; set; }
        public List<PermanenceModels> PermanencesList { get; set; }
        public List<Periods> PeriodsList { get; set; }
    }
}
