using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EmployeeLoans//قروض الموظفين
    {
        private double _installmentAmount;
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime InstallmentStartDate { get; set; }//تاريخ بداية التقسيط
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public bool Arrest { get; set; }// ايقاف التقسيط
        public double Amount { get; set; }
        public double InstallmentAmount
        {
            get
            {
                return _installmentAmount;
            }
            set
            {
                _installmentAmount = Amount / NumberOfInstallmentMonths;
            }
        }
        public double NumberOfInstallmentMonths { get; set; }// عدد الشهور التقسيط
        public string? Notes { get; set; }
    }
}
