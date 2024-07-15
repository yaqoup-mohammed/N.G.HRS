using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EmployeeLoans//قروض الموظفين
    {


        public int Id { get; set; }
        [Display(Name = "الموظف")]

        public int EmployeeId { get; set; }
        [Display(Name ="الموظف")]
        public Employee? Employee { get; set; }

        [Required(ErrorMessage = "الرجاء تحديد التاريخ")]
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]

        public DateTime Date { get; set; }
        [Required(ErrorMessage = "الرجاء تحديد التقسيط")]
        [Display(Name = "تاريخ بداية التقسيط")]
        [DataType(DataType.Date)]
 
        public DateTime InstallmentStartDate { get; set; }//تاريخ بداية التقسيط
        [Display(Name = "العملة")]
        public int CurrencyId { get; set; }
        [Display(Name = "العملة")]
        public Currency? Currency { get; set; }
        [Display(Name = "التوقيف")]
        public bool Arrest { get; set; }// ايقاف التقسيط
        [Required(ErrorMessage = "الرجاء تحديد المبلغ")]
        [Display(Name = "المبلغ")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "الرجاء تحديد مبلغ التقسيط")]
        [Display(Name = "مبلغ التقسيط")]


        public double InstallmentAmount { get; set; }

        [Display(Name = " عدد الشهور التقسيط")]

        public double NumberOfInstallmentMonths { get; set; }// عدد الشهور التقسيط
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }
    }
}
