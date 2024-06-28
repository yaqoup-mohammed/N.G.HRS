using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
namespace N.G.HRS.Areas.PayRoll.ModelView
{
    public class EmployeeDeductionsAllowancesVM
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public AllowancesAndDiscounts AllowancesAndDiscounts { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }= DateTime.Now;

        [DataType(DataType.Date)]
        public DateOnly FromDate { get; set; }

        [DataType(DataType.Date)]
        public DateOnly ToDate { get; set; }
        public string? Notes { get; set; }
    }
}
