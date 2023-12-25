using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Sectors
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string SectorsName { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }

    }
}
