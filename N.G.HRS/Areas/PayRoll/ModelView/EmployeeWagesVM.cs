using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.Employees.Models;

namespace N.G.HRS.Areas.PayRoll.ModelView
{
    public class EmployeeWagesVM
    {
        public FinancialStatements FinancialStatements { get; set; }
        public AllowancesAndDiscounts AllowancesAndDiscounts { get; set; }
        //باقي الحجوزات الادارية    
        //public int EmployeeId { get; set; }
        //public Employee Employee { get; set; }
        public List<AllowancesAndDiscounts> AllowancesAndDiscountsList { get; set; }
        public List<FinancialStatements> FinancialStatementsList { get; set; }
        public List<Employee> EmployeeList { get; set; }
    }
}
