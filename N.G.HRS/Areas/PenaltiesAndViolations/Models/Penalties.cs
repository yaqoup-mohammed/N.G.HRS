using N.G.HRS.Areas.Employees;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PenaltiesAndViolations.Models
{
    public class Penalties//العقوبات
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string PenaltiesName { get; set; }
        public bool Deduction { get; set; }//خصمية
        public bool DiscountFromWorkingHours { get; set; }
        public bool DeductionFromTheDailyWage { get; set; }
        public bool DeductionFromSalary { get; set; }
        [Range(1,99999.99)]
        public int? Value { get; set; }
        [Range(1,100)]  
        public int? Percent { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //=============================================================
        [ForeignKey("PenaltiesAndViolationsFormsId")]
        public int PenaltiesAndViolationsFormsId { get; set; }
        public PenaltiesAndViolationsForms PenaltiesAndViolationsForms { get; set; }

    }
}
