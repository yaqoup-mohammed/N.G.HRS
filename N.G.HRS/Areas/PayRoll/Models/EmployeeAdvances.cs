using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EmployeeAdvances//سلف الموظفين
    {
        public int Id { get; set; }

        [Display(Name = "الموظف")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        //=====================================================================
        [Display(Name = "الادارة")]

        public int DepartmentId { get; set; }
        public Departments? Departments { get; set; }
        //=====================================================================
        [Display(Name = "القسم")]

        public int SectionId { get; set; }
        public Sections? Sections { get; set; }
        //=====================================================================
        [Display(Name = "حساب الموظف")]

        public int EmployeeAccountId { get; set; }
        public EmployeeAccount? EmployeeAccount { get; set; }
        //=====================================================================
        [Display(Name = "العملة")]

        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        //=====================================================================
        [Display(Name = "المبلغ")]

        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }
}
