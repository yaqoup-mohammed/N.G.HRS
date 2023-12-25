using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class ContractTerms
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string ModelName { get; set; }
        [Required]
        [StringLength(255)]
        public string StatementOfConditions { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
    }
}
