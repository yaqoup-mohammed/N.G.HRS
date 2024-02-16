using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class AdditionalAccountInformation
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Range(1, 100)]
        public int PercentageOnTheCompany { get; set; }
        [Required]
        [Range(1, 100)]
        public int PercentageOnTheEmployee { get; set; }
        [Required]
        [Range(1, 100)]
        public int percentage{ get; set; }
        public bool HealthInsuranceIncluded { get; set; }
        public bool IncludesRetirementInsurance { get; set; }
        public bool IncludesTheWorkShareInRetirementInsurance { get; set; }
        public bool IncludesTaxCalculation { get; set; }
        public bool AllowancesIncluded { get; set; }
        public bool IncludesAdditionalData { get; set; }
        public string TaxFrom { get; set; }
        public DateOnly Day { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateOnly FromDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateOnly ToDate { get; set; }

    }
}
