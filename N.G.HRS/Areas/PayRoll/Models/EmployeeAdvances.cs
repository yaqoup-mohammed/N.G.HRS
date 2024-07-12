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
        [Required ( ErrorMessage = "الرجاء تحديد الموظف") ]
        [Display(Name = "الموظف")]

        public int EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public Employee?  Employee { get; set; }

        [Required(ErrorMessage = "الرجاء تحديد الادارة")]
        [Display(Name = "الادارة")]
        public int DepartmentId { get; set; }
        public Departments? Departments { get; set; }

        [Required(ErrorMessage = "الرجاء تحديد القسم")]
        [Display(Name = "القسم")]
        public int SectionId { get; set; }
        public Sections? Sections { get; set; }

      [Required(ErrorMessage = "الرجاء تحديد الحساب الموظف")]
        [Display(Name = "حساب الموظف")]
        public int EmployeeAccountId { get; set; }
        [Display(Name = "حساب الموظف")]

        public EmployeeAccount? EmployeeAccount { get; set; }

        [Required(ErrorMessage = "الرجاء تحديد العملة")]
        [Display(Name = "العملة")]
        public int CurrencyId { get; set; }
        [Display(Name = "العملة")]
        public Currency? Currency { get; set; }

        [Required(ErrorMessage = "  الرجاء تحديد المبلغ")]
        [Display(Name = "المبلغ")]

        public decimal Amount { get; set; }
        [Display(Name = "الملاحظات")]

        public string? Notes { get; set; }
    }
}
