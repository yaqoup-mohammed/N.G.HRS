using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
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
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        //=
        [ForeignKey("DepartmentAccountsId")]
        public int DepartmentAccountsId { get; set; }
        public DepartmentAccounts departmentAccounts { get; set; }
        //=
        [ForeignKey("LinkingEmployeesToShiftPeriodsId")]
        public int LinkingEmployeesToShiftPeriodsId { get; set; }
        public LinkingEmployeesToShiftPeriods linkingEmployeesToShiftPeriods { get; set; }
        //========================================
        public List<Departments> departmentsList {  get; set; } 

    }
}
