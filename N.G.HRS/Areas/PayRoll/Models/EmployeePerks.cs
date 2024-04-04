using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EmployeePerks//اكراميات الموظفين
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        [Range(1, 100)]
        public int Percentage { get; set; }
        public string? Notes { get; set; }

    }
}
