using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace N.G.HRS.Areas.Employees.Models
{
    public class FinancialStatements//البيانات المالية
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string NatureOfEmployment { get; set; }
        [Required]
        [Range(0, 999999.99)]
        public decimal BasicSalary { get; set; }
        [Required]

        public int? InsuranceAccountNumber { get; set; }
        [Required]
        public int? BankAccountNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Salary Start Date")]
        public DateOnly SalaryStartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Salary End Date")]
        public DateOnly SalaryEndDate { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //يرتبط بجدول (الموظف) وجدول (العملة)ي
        //==================================================
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public virtual Employee employee { get; set; }
        //===============================================
        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        public virtual Currency? Currency { get; set; }
    }
}
