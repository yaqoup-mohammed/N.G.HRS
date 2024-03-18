using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class JobDescription :Base
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "اسم  الوظيفة مطلوب من فضلك")]
        [Display(Name = "اسم  الوظيفة")]
        [StringLength(150)]     
        public string JopName { get; set; }

        [Required (ErrorMessage = "اسم  المؤهل الوظيفي مطلوب من فضلك")]
        [Display(Name = "اسم المؤهل")]
        [StringLength(255)]
        public string JobQualifications { get; set; }

        [Required ]
        [StringLength(255)]
        public string Authorities { get; set; }
        [Required (ErrorMessage = "المسؤولية الوظيفة مطلوبة من فضلك")]
        [Display(Name = "مسؤولية الوظيفة")]
        [StringLength(255)]
        public string Responsibilities { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //============================================
        //=========================================
        [ForeignKey("FunctionalCategoriesId")]
        [Display(Name = "تصنيف الوظيفة")]
        public int? FunctionalCategoriesId { get; set; }
        
        public FunctionalCategories? FunctionalCategories { get; set; }
        //=
        [ForeignKey("FunctionalClassId")]

        public int? FunctionalClassId { get; set; }
        public FunctionalClass? FunctionalClass { get; set; }
        //=
        [ForeignKey("JobRanksId")]

        
        public int? JobRanksId { get; set; }

        public JobRanks? JobRanks { get; set; }

    }
}
