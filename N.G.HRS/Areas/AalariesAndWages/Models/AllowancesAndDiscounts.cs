using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class AllowancesAndDiscounts
    {
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
        public decimal Amount { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }

    }
}
