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
        [StringLength(3)]
        public string? NatureOfEmployment { get; set; }
        [Required]
        [Range(1, 999999999.99)]
        public decimal BasicSalary { get; set; }
        [Range(1, 9999999999999999.99)]
        public int? InsuranceAccountNumber { get; set; }
        [Range(1, 9999999999999999.99)]

        public int? BankAccountNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SalaryStartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SalaryEndDate { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //يرتبط بجدول (الموظف) وجدول (العملة)ي
        //==================================================
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public virtual Employee? employee { get; set; }
        //===============================================
        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        public virtual Currency? Currency { get; set; }
    }
}
