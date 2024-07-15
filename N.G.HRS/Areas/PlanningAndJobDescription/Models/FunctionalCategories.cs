using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class FunctionalCategories
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "!!اسم الفئة مطلوب من فضلك")]
        [Display(Name = "اسم الفئة")]

        public string CategoriesName { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }
        //==================================================
        public List<JobDescription>? JobDescriptionsList { get; set; }
        //=


    }
}
