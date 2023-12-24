using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Models
{
    public class BasicDataForTheSalaryStatement
    {
        public int Id { get; set; }
        public Boolean HealthInsuranceIncluded { get; set; }
        public Boolean RetirementInsuranceIncluded { get; set; }
        public Boolean IncludesTheWorkShareInRetirementInsurance { get; set; }
        public Boolean IncludesTaxCalculation { get; set; }
        public Boolean TaxFrom { get; set; }
        public Boolean AllowancesIncluded { get; set; }
        public Boolean IncludesAdditionalData { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateOnly FromDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateOnly ToDate { get; set; }
        [Required]
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
