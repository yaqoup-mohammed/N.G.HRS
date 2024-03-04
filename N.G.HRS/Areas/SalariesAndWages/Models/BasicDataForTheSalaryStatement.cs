using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class BasicDataForTheSalaryStatement
    {
        [Key]
        public int Id { get; set; }
        public bool HealthInsuranceIncluded { get; set; }
        public bool RetirementInsuranceIncluded { get; set; }
        public bool IncludesTheWorkShareInRetirementInsurance { get; set; }
        public bool IncludesTaxCalculation { get; set; }
        public string TaxFrom { get; set; }
        public bool AllowancesIncluded { get; set; }
        public bool IncludesAdditionalData { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateOnly FromDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateOnly ToDate { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        [Required]
        [Range(0, 100)]
        public int Percentage { get; set; }
        [Required]
        [Range(0, 100)]
        public int PercentageOnEmployee { get; set; }
        [Required]
        [Range(0, 100)]
        public int PercentageOnCompany { get; set; }



    }
}
