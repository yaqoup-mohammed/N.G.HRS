using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EntitlementsAndDeductions
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display (Name ="التاريخ")]
        public DateTime Date { get; set; }= DateTime.Now;
        [Display(Name = "الشهر")]
        public int Month { get; set; } = DateTime.Now.Month;
        //=============================================================================

        [Display(Name = "الموظف")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        //=============================================================================
        [Display(Name = "النوع")]
        public string? Type { get; set; }
        //=============================================================================
        [Display(Name = "خاضع للضريبة")]
        public bool Taxable { get; set; }
        [Display(Name = "  اسم الحساب")]
        public int? FinanceAccountTypeId { get; set; }
        public FinanceAccountType? Account { get; set; }
        [Display(Name = "المبلغ")]
        public double? Amount { get; set; } = 0;
        [Display(Name = "النسبه")]
        public int? Percentage { get; set; } = 0;
        //=============================================================================
        [Display(Name = "العملة")]
        public int? CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        //=============================================================================
        [Display(Name ="الملاحظة")]
        public string? Note { get; set; }


    }
}
