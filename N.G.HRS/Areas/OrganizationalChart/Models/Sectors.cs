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
        [Display(Name = "القطاع")]
        public string SectorsName { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //===============================================
        [ForeignKey("BranchesId")]
        [Display(Name = "الفرع")]
        public int? BranchesId { get; set; }
        [Display(Name = "الفرع")]
        public Branches? Branches { get; set; }
        //=======================
        public List<Departments>? DepartmentsList { get; set; }



    }
}
