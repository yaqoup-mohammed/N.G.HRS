using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AalariesAndWages.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Finance.Models
{
    public class FinanceAccount
    {
        [Key]

        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]

        [Display( Name = "اسم الحساب" )]
        public string? Name { get; set; }
        [Required]
        [Display( Name = "نوع الحساب" )]
        public string? Type { get; set; }

        [Display( Name = "الملاحظات" )]
        public string? Notes { get; set; }
        //==========================================
        public List<EmployeeAccount>? EmployeeAccountsList { get; set; }
        //=
        public List<SectionsAccounts>? SectionsAccountsList { get; set; }
    }
}
