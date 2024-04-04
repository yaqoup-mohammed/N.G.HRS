using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class EmployeeLoans//قروض الموظفين
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوبي")]
        [Display(Name = "  الموظف")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
 //===================================================
        [Required(ErrorMessage = "هذا الحقل مطلوبي")]
        [Display(Name = " التاريخ ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage ="هذا الحقل مطلوبي")]
        [Display(Name = " تاريخ بداية التقسيط")]
        [DataType(DataType.Date)]
        public DateTime InstallmentStartDate { get; set; }//تاريخ بداية التقسيط

       

        //===================================================
        [Display(Name = "  العملة")]
        public int CurrencyId { get; set; }
        [Display(Name = " العملة")]
        public Currency? Currency { get; set; }
        //===================================================

        [Display(Name = " ايقاف التقسيط")]
        public bool Arrest { get; set; }// ايقاف التقسيط

        [Required(ErrorMessage = "هذا الحقل مطلوبي")]
        [Display(Name = " الكمية")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوبي")]
        [Display(Name = "كمية القسط")]
        public double InstallmentAmount{ get; set; }

          [Required(ErrorMessage = "هذا الحقل مطلوبي")]
        [Display(Name = "عدد اشهر التقسيط")]
        public double NumberOfInstallmentMonths { get; set; }// عدد الشهور التقسيط

        [Display(Name = "ملاحضات")]
        public string? Notes { get; set; }
    }
}
