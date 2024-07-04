using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Finance.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "هذا الحقل مطلوب")]
        [StringLength(70)]
        [Display(Name = "اسم العملة")]
        public string? CurrencyName { get; set; }
        [Required]
        [StringLength(70)]
        [DataType(DataType.Currency)]
        [Display(Name = "رمز العملة")]
        public string? CurrencyCode { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? CurrencyNotes { get; set; }
        [Required]
        [Range(0, 9999.99)]
        [Display(Name = "السعر الحالي")]
        public decimal CurrentPriceOfCurrency { get; set; }
        [Required]
        [Range(0, 9999.99)]
        [Display(Name = "السعر السابق")]
        public decimal PreviousPriceOfCurrency { get; set; }

        //========================================================
            public List<FunctionalCategories>? FunctionalCategoriesList { get; set; }
            public List<FunctionalClass>? FunctionalClassList { get; set; }
            public List<FinancialStatements>? FinancialStatementsList { get; set; }
            public List<EntitlementsAndDeductions>? EntitlementsAndDeductionsList { get; set; }
            public List<AllowancesAndDiscounts>? AllowancesAndDiscountsList { get; set; }
        public List<AdministrativeDecisions>? AdministrativeDecisionsList { get; set; }
        public List<EmployeeLoans>? EmployeeLoansList { get; set; }
        public List<Salaries>? SalariesList { get; set; }


        //=


    }
}
