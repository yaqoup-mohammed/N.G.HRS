using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EndOfServiceClearance//مخالصة الانتهاء من الخدمة
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "التاريخ")]
        [Required(ErrorMessage = "هذا الحقل مطلوب ")]
        public DateTime? Date { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "تاريخ انتهاء الخدمة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]

        public DateTime? EndOfServiceDate { get; set; }//تاريخ انتهاء الخدمة
        //==================================================
        [Display(Name = "الموظف")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        //==================================================
        [Display(Name = "سبب الانتهاء من الخدمة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]

        public string? ReasonForClearance { get; set; }//سبب الانتهاء من الخدمة

        //=======================================================
        [Display(Name = "الراتب السابق الموافق عليه")]
        public decimal? LastApprovedSalary { get; set; }//الراتب السابق الموافق عليه

        [Display(Name = "مدة الخدمة بالسنوات")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int ServicePeriodPerYear { get; set; }//مدة الخدمة بالسنوات

        [Display(Name = "المستحقات الخاصة بالانتهاء من الخدمة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public decimal? EndOfServiceBenefits { get; set; }//المستحقات الخاصة بالانتهاء من الخدمة

        [Display(Name = "السلف والقروض")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public decimal? AdvancesAndLoans { get; set; }//السلف والقروض

        [Display(Name = "المستحقات الاجازات")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public decimal? VacationEntitlements { get; set; }//المستحقات الاجازات

        [Display(Name = "الغياب/تأخبر/اجزة بدون راتب/حضور بصمة واحدة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public decimal? Absence { get; set; }//الغياب/تأخبر/اجزة بدون راتب/حضور بصمة واحدة

        [Display(Name = "المستحقات الاخري (مرتبات او اجور)")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public decimal? OtherEntitlements { get; set; }//المستحقات الاخري (مرتبات او اجور) ت

        [Display(Name = "الخصومات الاخرى")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public decimal? OtherDiscounts { get; set; }//الخصومات الاخرى

        [Display(Name = "المجموع")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public decimal? Total { get; set; }//المجموع


    }
}
