using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class AllowancesAndDiscounts
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="هذا الحق مطلوب!!")]
        [StringLength(170)]
        [Display(Name = "الاسم")]
        public string Name { get; set; }
        [Required(ErrorMessage = "هذا الحق مطلوب!!")]
        [StringLength(150)]
        [Display(Name = "النوع")]
        public string Type { get; set; }
        [Display(Name = "خاضع للضريبة")]
        public bool Taxable { get; set; }
        [Display(Name = "يضافة لجميع الموظفين")]
        public bool AddedToAllEmployees { get; set; }
        [Display(Name = "بدل تراكمي")]
        public bool CumulativeAllowance { get; set; }
        [Display(Name = "خاضع للتأمين ")]
        public bool SubjectToInsurance { get; set; }
        [Range(1, int.MaxValue)]
        [Display(Name = "المبلغ")]
        public decimal? Amount { get; set; } 
        [Range(1, 100)]
        [Display(Name = "النسبة")]
        public int? Percentage { get; set; } 
        [StringLength(255)]
        [Display(Name="الملاحظات")]
        public string? Notes { get; set; }
        //==================================================================
        [Display(Name = "العملة")]
        public int? CurrencyId { get; set; }
        [Display(Name = "العملة")]
        public Currency? Currency { get; set; }

    }
}