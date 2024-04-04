using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EntitlementsAndDeductions
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }= DateTime.Now;
        [Range(1, 12)]
        public int Month { get; set; } = DateTime.Now.Month;
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Type { get; set; }
        public bool Taxable { get; set; }
        public int? FinanceAccountTypeId { get; set; }
        public FinanceAccountType Account { get; set; }
        public double? Amount { get; set; } = 0;
        public int? Percentage { get; set; } = 0;
        public int? CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public string? Note { get; set; }


    }
}
