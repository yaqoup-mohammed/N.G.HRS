using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class EducationalQualification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "اسم المؤهل")]

        public string Name { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]

        public string? Notes { get; set; }
        //========================================================
        public ICollection<Qualifications>? qualifications { get; set; }

    }
}
