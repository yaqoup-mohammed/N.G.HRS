using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PenaltiesAndViolations.Models
{
    public class Violations//المخالفات
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string ViolationsName { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //========================================
        [ForeignKey("PenaltiesAndViolationsFormsId")]
        public int PenaltiesAndViolationsFormsId { get; set; }
        public PenaltiesAndViolationsForms PenaltiesAndViolationsForms { get; set; }
    }
}
