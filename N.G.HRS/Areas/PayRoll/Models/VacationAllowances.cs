using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class VacationAllowances//بدلات الاجازات
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public double VacationBalance { get; set; }
        public double Amount { get; set; }
        public decimal CarryoverBalance { get; set; }
        public string? Notes { get; set; }
    }
}
