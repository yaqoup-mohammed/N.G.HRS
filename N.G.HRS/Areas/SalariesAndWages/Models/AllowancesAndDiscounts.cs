using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class AllowancesAndDiscounts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(170)]
        public string Name { get; set; }
        [Required]
        [StringLength(150)]
        public string Type { get; set; }
        public bool Taxable { get; set; }
        public bool AddedToAllEmployees { get; set; }
        public bool CumulativeAllowance { get; set; }
        public bool SubjectToInsurance { get; set; }
        [Range(1, 10000000)]
        public decimal? Amount { get; set; } = 0;
        [Range(1, 100)]
        public decimal? Percentage { get; set; } = 0;
        [StringLength(255)]
        public string? Notes { get; set; }

    }
}