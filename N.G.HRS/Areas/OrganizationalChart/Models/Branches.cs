using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        //==========================================
        //=

        //=
        public List<Sectors>? SectorsList { get; set; }

        //=
        [ForeignKey("CompanyId")]
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        //=
        [ForeignKey("CountryId")]
        public int? CountryId { get; set; }
        public Country? Country { get; set; }
        //=
        [ForeignKey("GovernorateId")]
        public int? GovernorateId { get; set; }
        public Governorate? Governorate { get; set; }
        //=
        [ForeignKey("DirectorateId")]
        public int? DirectorateId { get; set; }
        public Directorate? Directorate { get; set; }
        //======
        //===
        //==========================================
    }
}
