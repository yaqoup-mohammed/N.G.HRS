using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class JobDescription :Base
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "اسم  الوظيفة مطلوب ")]
        [Display(Name = "اسم  الوظيفة")]
        [StringLength(150)]         
        public string? JopName { get; set; }

        [Required (ErrorMessage = "اسم  المؤهل الوظيفي مطلوب ")]
        [Display(Name = "اسم المؤهل")]
        [StringLength(255)]
        public string? JobQualifications { get; set; }

        [Required ]
        [StringLength(255)]
        [Display(Name = "السلطات")]
        public string Authorities { get; set; }
        [Required (ErrorMessage = "المسؤولية الوظيفة مطلوبة ")]
        [Display(Name = "مسؤولية الوظيفة")]
        [StringLength(255)]
        public string Responsibilities { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //============================================
        //=========================================
        [ForeignKey("FunctionalCategoriesId")]
        [Display(Name = "الفئات الوظيفة")]
        public int? FunctionalCategoriesId { get; set; }
        [Display(Name = "الفئات الوظيفة")]

        public FunctionalCategories? FunctionalCategories { get; set; }
        //=
        [ForeignKey("FunctionalClassId")]
        [Display(Name = "الدرجة الوظيفة")]

        public int? FunctionalClassId { get; set; }
        [Display(Name = "الدرجة الوظيفة")]

        public FunctionalClass? FunctionalClass { get; set; }
        //=
        [ForeignKey("JobRanksId")]

        
        [Display(Name = "الرتبة الوظيفية")]
        public int? JobRanksId { get; set; }
        [Display(Name = "الرتبة الوظيفية")]

        public JobRanks? JobRanks { get; set; }

        //===============================================
        public List<EmployeeMovements>? EmployeeMovementsList { get; set; }
        //public string? CurrentJop { get; internal set; }
        //====================================


    }
}
