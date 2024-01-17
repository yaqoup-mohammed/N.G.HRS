using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class FunctionalCategories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string CategoriesName { get; set; }
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
        //==================================================
        public List<JobDescription> JobDescriptionsList { get; set; }
        //=
        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

    }
}
