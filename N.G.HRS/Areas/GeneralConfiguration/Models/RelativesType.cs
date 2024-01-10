using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class RelativesType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string RelativeName { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //==============================================
        [ForeignKey("FamilyId")]
        public int FamilyId { get; set; }
        public Family family { get; set; }
    }
}
