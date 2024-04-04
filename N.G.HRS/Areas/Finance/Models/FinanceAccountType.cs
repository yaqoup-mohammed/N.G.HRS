using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.PayRoll.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Finance.Models
{
    public class FinanceAccountType
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = " اسم الحساب مطلوب")]
        [Display(Name = "اسم الحساب ")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "نوع الحساب مطلوب")]
        [Display(Name = "نوع الحساب ")]

        public string? Type { get; set; }
         [Required(ErrorMessage = "رقم الحساب مطلوب")]
        [Display(Name = "رقم الحساب ")]
        public int AccountNumber { get; set; }
        [Display(Name = "ملاحظات")]
        public string? Note { get; set; }
        //====================================


        //=
        public List<SectionsAccounts>? SectionsAccountsList { get; set; }
        //=
        public List<EmployeeAccount>? EmployeeAccountsList { get; set; }
        //=
        public List<EntitlementsAndDeductions>? EntitlementsAndDeductionsList { get; set; }
        //=




    }
}
