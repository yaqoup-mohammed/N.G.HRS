using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Sectors//القطاعات
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string SectorsName { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //===============================================
        [ForeignKey("BranchesId")]
        public int? BranchesId { get; set; }
        public Branches? Branches { get; set; }
        //=======================
        public List<Departments>? DepartmentsList { get; set; }



    }
}
