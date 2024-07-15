using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EndOfServiceClearance//مخالصة الانتهاء من الخدمة
    {
        public int Id { get; set; }
        [Display(Name = "التاريخ ")]

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }=DateTime.Now;
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ نهاية الخدمة ")]

        public DateTime EndOfServiceDate { get; set; }//تاريخ انتهاء الخدمة
        [Display(Name = "الموظف ")]

        public int EmployeeId { get; set; }
        [Display(Name = "الموظف ")]
        public Employee? Employee { get; set; }

        [Display(Name = "سبب التصفية ")]
        public string ReasonForClearance { get; set; }//سبب الانتهاء من الخدمة
        [Display(Name = "الراتب المعتمد ")]

        public decimal LastApprovedSalary { get; set; }//الراتب السابق الموافق عليه
        [Display(Name = "مدة الخدمة بالسنة ")]

        public int ServicePeriodPerYear { get; set; }//مدة الخدمة بالسنوات
        [Display(Name = "مستحقات نهاية الخدمة ")]

        public decimal EndOfServiceBenefits { get; set; }//المستحقات الخاصة بالانتهاء من الخدمة
        [Display(Name = "السلف والقروض ")]

        public decimal AdvancesAndLoans { get; set; }//السلف والقروض
        [Display(Name = "مستحقات الاجازة ")]

        public decimal VacationEntitlements { get; set; }//المستحقات الاجازات
        [Display(Name = "الغياب ")]

        public decimal Absence { get; set; }//الغياب/تأخبر/اجزة بدون راتب/حضور بصمة واحدة
        [Display(Name = "مستحقات اخرى ")]

        public decimal OtherEntitlements { get; set; }//المستحقات الاخري (مرتبات او اجور) ت
        [Display(Name = "خصومات اخرى ")]

        public decimal OtherDiscounts { get; set; }//الخصومات الاخرى
        [Display(Name = "المجموع ")]

        public decimal Total { get; set; }//المجموع


    }
}
