using System.Security.Principal;

namespace N.G.HRS.Models
{
    public class FinancialStatements//البيانات المالية
    {
        public int ID { get; set; }
        public string NatureOfEmployment { get; set; }
        public Decimal BasicSalary { get; set; }
        public int InsuranceAccountNumber { get; set; }
        public int BankAccountNumber { get; set; }
        public string SalaryStartDate { get; set; }
        public string SalaryEndDate { get; set; }
        public string Notes { get; set; }
        //يرتبط بجدول (الموظف) وجدول (العملة)ي
    }
}
