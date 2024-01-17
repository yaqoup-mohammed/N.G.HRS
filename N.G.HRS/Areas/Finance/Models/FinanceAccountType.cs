using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AalariesAndWages.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Finance.Models
{
    public class FinanceAccountType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int AccountNumber { get; set; }
        public string Description { get; set; }
        //====================================


        //=
        public List<SectionsAccounts> SectionsAccountsList { get; set; }
        //=
        public List<EmployeeAccount> EmployeeAccountsList { get; set; }
        //=




    }
}
