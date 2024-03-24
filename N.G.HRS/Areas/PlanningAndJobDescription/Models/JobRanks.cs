using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class JobRanks//الرتب الوظيفية
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "اسم الرتبة الوظيفة مطلوب ")]
        [Display (Name = "اسم الرتبة الوظيفة")]
        [StringLength(150)]
        public string RankName { get; set; }
        [StringLength(255)]
        [Display (Name = "ملاحظات")]
        public string? Notes { get; set; }

        //=====================================
        public List<JobDescription>? JobDescriptionList { get; set; }

    }
}
