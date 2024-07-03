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
        public double Additinal { get; set; }//الاضافي
        public decimal Salary { get; set; }//الراتب
        //public int CurrencyId { get; set; }
        //public Currency Currency { get; set; }
        public double allowances { get; set; }//البدلات
        [DataType(DataType.Date)]
        public DateTime SelectedMonth { get; set; }
        public double Gratuities { get; set; }//الاكراميات
        public double Bonuses { get; set; }//العلاوات
        public double Late { get; set; }//التأخير
        public double Abcents { get; set; }//الغياب
        public double Entitlements { get; set; }//الاستحقاقات
        public double Deductions { get; set; }//الخصميات
        public double Another { get; set; }

    }

}

