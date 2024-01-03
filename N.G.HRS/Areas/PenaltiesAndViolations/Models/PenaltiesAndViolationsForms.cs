using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PenaltiesAndViolations.Models
{
    public class PenaltiesAndViolationsForms
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        public int NumberOfTime { get; set; }
        //=========================================
        public List<Violations> ViolationsList { get; set; }
        public List<Penalties> PenaltiesList { get; set; }
    }
}
