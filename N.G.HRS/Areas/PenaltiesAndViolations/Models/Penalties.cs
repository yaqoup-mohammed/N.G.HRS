using N.G.HRS.Areas.Employees;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PenaltiesAndViolations.Models
{
    public class Penalties//العقوبات
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="هذا الحق مطلوب")]
        [Display(Name ="اسم المخالفة")]
        [StringLength(150)]
        public string? PenaltiesName { get; set; }
        [Display(Name = "خصمية ")]
        public bool Deduction { get; set; }//خصمية
        [Display(Name = "خصم من ساعات الدوام ")]

        public bool DiscountFromWorkingHours { get; set; }
        [Display(Name = "خصم من الاجر اليومي ")]

        public bool DeductionFromTheDailyWage { get; set; }
        [Display(Name = "خصم من الراتب ")]

        public bool DeductionFromSalary { get; set; }
        [Range(1,99999.99)]
        [Display(Name = "القيمة ")]
        public int? Value { get; set; }
        [Range(1,100)]
        [Display(Name = "النسبة ")]

        public int? Percent { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]

        public string? Notes { get; set; }
        //=============================================================
        public List<PenaltiesAndViolationsForms>? PenaltiesAndViolationsFormsList { get; set;}


    }
}
