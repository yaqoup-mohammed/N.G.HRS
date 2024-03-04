using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EndOfServiceClearance//مخالصة الانتهاء من الخدمة
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }=DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime EndOfServiceDate { get; set; }//تاريخ انتهاء الخدمة
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string ReasonForClearance { get; set; }//سبب الانتهاء من الخدمة
        public decimal LastApprovedSalary { get; set; }//الراتب السابق الموافق عليه
        public int ServicePeriodPerYear { get; set; }//مدة الخدمة بالسنوات
        public decimal EndOfServiceBenefits { get; set; }//المستحقات الخاصة بالانتهاء من الخدمة
        public decimal AdvancesAndLoans { get; set; }//السلف والقروض
        public decimal VacationEntitlements { get; set; }//المستحقات الاجازات
        public decimal Absence { get; set; }//الغياب/تأخبر/اجزة بدون راتب/حضور بصمة واحدة
        public decimal OtherEntitlements { get; set; }//المستحقات الاخري (مرتبات او اجور) ت
        public decimal OtherDiscounts { get; set; }//الخصومات الاخرى
        public decimal Total { get; set; }//المجموع


    }
}
