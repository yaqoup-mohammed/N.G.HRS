using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class FunctionalClass //الدرجة الوظيفية
    {
        [Key]  
        public int Id { get; set; }
        [Required (ErrorMessage = "!!  اسم الدرجة الوظيفية مطلوب من فضلك")]
        [Display(Name = "اسم الدرجة الوظيفية")]
        [StringLength(150)]
        public string Name { get; set; }
        [Required (ErrorMessage = "!!المرتب الاساسي مطلوب من فضلك")]
        [Display(Name = "المرتب الاساسي")]
        [Range(0, 999999.999)]
        public decimal BasicSalary { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //===============================================
        [ForeignKey("CurrencyId")]
        public int? CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        //=========================================
        public List<JobDescription>? JobDescriptionsList { get; set; }

    }
}
