using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.AalariesAndWages.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Sections //الاقسام
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string SectionsName { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }

        //=============================================
        public List<Employee> EmployeesList { get; set; }
        //=
        public List<SectionsAccounts> SectionsAccountsList { get; set; }
        //=
        public List<LinkingEmployeesToShiftPeriods> LinkingEmployeesToShiftPeriodsList { get; set; }

        //=

        //========================================
        [ForeignKey("DepartmentsId")]
        public int DepartmentsId { get; set; }
        public Departments Departments { get; set; }
        //=
        public List<StaffTime> staffTimeList { get; set; }


    }
}
