using N.G.HRS.Areas.ViolationsAndPenaltiesAffairs.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PenaltiesAndViolations.Models
{
    public class Violations//المخالفات
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = " اسم المخالفة")]
        [StringLength(150)]
        public string ViolationsName { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //========================================
        public List<PenaltiesAndViolationsForms>? PenaltiesAndViolationsFormsList { get; set; }
        //========================================================
        public List<EmployeeViolations>? EmployeeViolationsList { get; set; }



    }

}
