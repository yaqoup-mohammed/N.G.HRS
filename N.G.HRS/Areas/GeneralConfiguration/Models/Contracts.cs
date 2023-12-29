using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Contracts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //==========================================
        public List<ContractTerms> contractTermsList { get; set; }
    }
}
