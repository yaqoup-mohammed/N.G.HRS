using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class VacationAllowances//بدلات الاجازات
    {
        [Display(Name = "التاريخ")]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="التاريخ")]
        public DateTime Date { get; set; }
        [Display(Name = "الموظف")]
        public int EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public Employee Employee { get; set; }
        [Display(Name = " لفففففففففففف")]
        public double VacationBalance { get; set; }
        [Display(Name = "الكمية")]
        public double Amount { get; set; }
        [Display(Name = " الرصيد المرحل")]
        public decimal CarryoverBalance { get; set; }
        [Display(Name = "الملاحظات")]

        public string? Notes { get; set; }
    }
}
