using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("ViolationsId")]
        public int ViolationsId { get; set; }
        public Violations Violations { get; set; }
        //=
        public List<Penalties> PenaltiesList { get; set; }
        [ForeignKey("PenaltiesId")]
        public int PenaltiesId { get; set; }
        public Penalties Penalties { get; set; }
    }
}
