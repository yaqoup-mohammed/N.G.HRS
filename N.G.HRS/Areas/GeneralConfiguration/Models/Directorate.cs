using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Directorate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //===========================================
        //=
        [ForeignKey("GovernorateId")]
        public int? GovernorateId { get; set; }
        public Governorate? Governorate { get; set; }
        //=
        public List<Branches>? BranchesList { get; set; }
        //=

        //===========================================
    }
}
