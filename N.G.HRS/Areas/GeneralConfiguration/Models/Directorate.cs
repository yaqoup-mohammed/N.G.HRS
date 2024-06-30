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
        [Display(Name = "المديرية")]
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //===========================================
        //=
        [ForeignKey("GovernorateId")]
        [Display(Name = "المحافظة")]
        public int? GovernorateId { get; set; }
        [Display(Name = "المحافظة")]

        public Governorate? Governorate { get; set; }
        //=
        public List<Branches>? BranchesList { get; set; }
        //=

        //===========================================
    }
}
