using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Models
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
        public Boolean Taxable { get; set; }
        public Boolean AddedToAllEmployees { get; set; }
        public Boolean CumulativeAllowance { get; set; }
        public Boolean SubjectToInsurance { get; set; }
        public Decimal Amount { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }

    }
}
