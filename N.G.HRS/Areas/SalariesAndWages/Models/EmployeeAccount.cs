using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class EmployeeAccount
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //=====================================
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee employee { get; set; }
        //=

        //=

        [ForeignKey("FinanceAccountTypeId")]
        public int FinanceAccountTypeId { get; set; }
        public FinanceAccountType FinanceAccountType { get; set; }
        //=
        [ForeignKey("FinanceAccountId")]
        public int FinanceAccountId { get; set; }
        public FinanceAccount FinanceAccount { get; set; }

    }
}
