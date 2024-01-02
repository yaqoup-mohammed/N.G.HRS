using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        
        public string? Name { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //=======================================================
        public List<Governorate> governoratesList { get; set; }
        //========================================================
        [ForeignKey("BranchesId")]
        public int BranchesId { get; set; }
        public Branches branches { get; set; }


    }
}
