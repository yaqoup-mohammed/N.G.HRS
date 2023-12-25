using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class JobRanks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string RankName { get; set; }
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
    }
}
