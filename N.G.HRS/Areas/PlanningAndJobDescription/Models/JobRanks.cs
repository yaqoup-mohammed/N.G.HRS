using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class JobRanks//الرتب الوظيفية
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string RankName { get; set; }
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
        //=====================================
        [ForeignKey("JobDescriptionId")]
        public int JobDescriptionId { get; set; }
        public JobDescription jobDescription { get; set; }
    }
}
