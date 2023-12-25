using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class JobDescription
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]     
        public string JopName { get; set; }
        [Required]
        [StringLength(255)]
        public string JobQualifications { get; set; }
        [Required]
        [StringLength(255)]
        public string Authorities { get; set; }
        [Required]
        [StringLength(255)]
        public string Responsibilities { get; set; }
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }

    }
}
