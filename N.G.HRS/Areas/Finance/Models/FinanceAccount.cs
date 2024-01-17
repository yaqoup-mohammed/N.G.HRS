using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AalariesAndWages.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Finance.Models
{
    public class FinanceAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Notes { get; set; }
        //==========================================
        public List<EmployeeAccount> EmployeeAccountsList { get; set; }
        //=
        public List<SectionsAccounts> SectionsAccountsList { get; set; }
    }
}
