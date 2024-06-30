using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Areas.PayRoll.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class EmployeeAccount
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]
        public string? Notes{ get; set; }
        //=====================================
        [ForeignKey("Employee")]
        [Required (ErrorMessage = "الحقل هذا مطلوب")]
        [Display(Name = "الموظف")]
        public int? EmployeeId { get; set; }
        public Employee? employee { get; set; }
        //=

        //=

        [ForeignKey("FinanceAccountTypeId")]
        [Required(ErrorMessage = "الحقل هذا مطلوب")]
        [Display(Name = "نوع الحساب المالي")]
        public int? FinanceAccountTypeId { get; set; }
        public FinanceAccountType? FinanceAccountType { get; set; }
        //=
        [ForeignKey("FinanceAccountId")]
        [Required(ErrorMessage = "الحقل هذا مطلوب")]
        [Display(Name = "معرف الحساب المالي")]
        public int? FinanceAccountId { get; set; }
        public FinanceAccount? FinanceAccount { get; set; }

        public List<EmployeeAdvances>? EmployeeAdvancesList { get; set; }


    }
}
