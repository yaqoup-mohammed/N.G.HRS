using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class EmployeeAccount
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //=====================================
        public List<Employee> employeesList { get; set; }
        public List<FinanceAccountType> financeAccountTypesList { get; set;}
        public List<FinanceAccount> financeAccountsList { get; set; }

    }
}
