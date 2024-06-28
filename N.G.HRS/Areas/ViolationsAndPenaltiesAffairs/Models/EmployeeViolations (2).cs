using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.PenaltiesAndViolations.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.ViolationsAndPenaltiesAffairs.Models
{
    public class EmployeeViolations
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "تاريخ المخالفة")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ViolationDate { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "  التاريخ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOnly { get; set; }
        [Display(Name = "الخصم")]
        public decimal Discounts { get; set; }
        [Display(Name = "أستلام الإشعار")]
        public bool ReceiptOfNotifications { get; set; }
        [Display(Name = "معفي")]
        public bool Exempt { get; set; }
        //==============================================

        [Display(Name = "الموظف")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        //==============================================

        [Display(Name = "المخالفة")]
        public int? ViolationId { get; set; }
        public Violations? Violations { get; set; }
        //==============================================
        [Display(Name = "العقوبة")]
        public int? PenaltiesId { get; set; }
        public Penalties? Penalties { get; set; }
        //==============================================
        [Display(Name = "عدد العقوبات")]
        [Range(0, 9999, ErrorMessage = "يجب ان يكون عدد العقوبات رقم")]
        public int NumberPenalties { get; set; }
        [Display(Name = "ملاحظات")]
        public string? Note { get; set; }

    }
}
