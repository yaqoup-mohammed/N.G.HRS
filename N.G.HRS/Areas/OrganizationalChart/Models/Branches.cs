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
        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public Company company { get; set; }
        //=
        [ForeignKey("SectorsId")]
        public int SectorsId { get; set; }
        public Sectors sectors { get; set; }
        //==========================================
        public List<Country> CountryList { get; set; }
        public List<Governorate> GovernoratesList { get; set; }
        public List<Directorate> DirectoratesList { get; set;}
    }
}
