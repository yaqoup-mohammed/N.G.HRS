using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class BasicDataForTheSalaryStatement
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "يشمل التأمين الصحي")]
        public string? HealthInsuranceIncluded { get; set; }
        [Display(Name = "يشمل التأمين التقاعدي")]
        public string? RetirementInsuranceIncluded { get; set; }
        [Display(Name = "يشمل حصة العمل في التأمين التقاعدي")]
        public string? IncludesTheWorkShareInRetirementInsurance { get; set; }
        [Display(Name = "يشمل حساب الضريبة")]
        public string? IncludesTaxCalculation { get; set; }
        [Display(Name = "ضريبة من")]
        public string? TaxFrom { get; set; }
        [Display(Name = "يشمل  البدلات")]
        public string? AllowancesIncluded { get; set; }
        [Display(Name = "يشمل بيانات الاضافي")]
        public string? IncludesAdditionalData { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "من تاريخ")]
        public DateOnly FromDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "الى تاريخ")]
        public DateOnly ToDate { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        [Range(1, 100)]
        [Display(Name = " النسبة")]
        public int? Percentage { get; set; }
        [Range(0, 100)]
        [Display(Name = "النسبة على الموظف")]
        public int? PercentageOnEmployee { get; set; }
        [Display(Name = "النسبة على الشركة")]
        [Range(0, 100)]
        public int? PercentageOnCompany { get; set; }



    }
}
