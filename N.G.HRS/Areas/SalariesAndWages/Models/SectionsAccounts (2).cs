using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class SectionsAccounts//حسابات الاقسام
    {
        [Key]
        public int id { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }
        //===============================================
        [ForeignKey("FinanceAccountTypeId")]

        public int? FinanceAccountTypeId { get; set; }
        public FinanceAccountType? FinanceAccountType { get; set; }
        //=
        [ForeignKey("FinanceAccountId")]
        public int? FinanceAccountId { get; set; }
        public FinanceAccount? FinanceAccount { get; set; }
        //=
        [ForeignKey("SectionsId")]
        public int? SectionsId { get; set; }
        public Sections? Sections { get; set; }

    }
}
