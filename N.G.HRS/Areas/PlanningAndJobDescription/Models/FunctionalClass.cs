using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class FunctionalClass //الدرجة الوظيفية
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
        //===============================================
        public List<Currency> CurrencyList { get; set; }
        //=========================================
        [ForeignKey("JobDescriptionId")]
        public int JobDescriptionId { get; set; }
        public JobDescription jobDescription { get; set; }
    }
}
