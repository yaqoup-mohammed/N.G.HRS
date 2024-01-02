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
        [ForeignKey("EmployeeAccountId")]
        public int EmployeeAccountId { get; set; }
        public EmployeeAccount employeeAccount { get; set; }
        //=
        [ForeignKey("DepartmentAccountsId")]
        public int DepartmentAccountsId { get; set; }
        public DepartmentAccounts departmentAccounts { get; set; }

    }
}
