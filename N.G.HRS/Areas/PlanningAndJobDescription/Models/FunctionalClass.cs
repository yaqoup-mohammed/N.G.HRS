using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class FunctionalClass
    {
        [Key]  
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [Range(0, 9999.999)]
        public decimal BasicSalary { get; set; }
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
    }
}
