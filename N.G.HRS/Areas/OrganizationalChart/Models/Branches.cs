using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Branches
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string BranchesName { get; set; }
        [Required]
        [StringLength (255)]
        public string BranchesAdress { get; set; }
        [Required]
        [StringLength (13)]
        [Phone]
        public string BranchesPhone { get; set; }
        [Required]
        [EmailAddress]
        public string BranchesEmail { get; set; }
        
        [StringLength(255)]
        public string? Notes { get; set; }
    }
}
