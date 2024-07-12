using DocumentFormat.OpenXml.Office2010.PowerPoint;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class Salaries
    {
        public int Id { get; set; }


        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal Additinal { get; set; }//الاضافي
        public decimal BaseSalary { get; set; }//الراتب الاساسي
        public decimal WorkedHours { get; set; }//
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public double allowances { get; set; }//البدلات

        [DataType(DataType.Date)]
        public DateTime SelectedMonth { get; set; }
        public decimal Gratuities { get; set; }//الاكراميات
        public decimal Bonuses { get; set; }//العلاوات
        public decimal Late { get; set; }//التأخير
        public decimal Abcents { get; set; }//الغياب
        public decimal HalfAbcents { get; set; }//نص غياب
        public decimal Entitlements { get; set; }//الاستحقاقات
        public decimal Deductions { get; set; }//الخصميات
        public decimal EarlyLeave { get; set; }//انصؤاف مبكر
        public decimal RetirementInsurance { get; set; }//تأمين تقاعدي
        public decimal Another { get; set; }

    }

}