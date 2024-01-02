using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class DepartmentAccounts
    {
        [Key]
        public int id { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //===============================================
        public List<FinanceAccount> FinanceAccountsList { get; set;}
        public List<FinanceAccountType> FinanceAccountsTypeList { get; set; }
        public List<Sections> SectionsList { get; set; }
    }
}
