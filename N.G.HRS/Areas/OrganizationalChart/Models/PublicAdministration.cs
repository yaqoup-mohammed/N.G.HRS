using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class PublicAdministration
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string PublicAdministrationName { get; set; }
        [StringLength(255)]
        public string? Nots { get; set; }
    }
}
