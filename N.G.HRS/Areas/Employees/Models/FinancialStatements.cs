using System.ComponentModel.DataAnnotations;
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
        [Range(0, 999.99)]
        public decimal BasicSalary { get; set; }
        [Required]

        public int InsuranceAccountNumber { get; set; }
        [Required]
        public int BankAccountNumber { get; set; }
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
        public Employee employee { get; set; }
    }
}
