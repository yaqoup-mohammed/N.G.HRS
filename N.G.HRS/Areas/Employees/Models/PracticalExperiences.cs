using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Employees.Models
{
    public class PracticalExperiences
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "اسم الخبرة")]
        public string? ExperiencesName { get; set; }
        [Required]
        [StringLength(70)]
        [Display(Name = "مكان الحصول على الخبرة")]
        public string? PlacToGainExperience { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "من تاريخ")]
        public DateOnly FromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "الى تاريخ")]
        public DateOnly ToDate { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "المدة")]

        public string Duration { get; set; }
        //يرتبط مع جدول الموظفين
        //=================================================

        [ForeignKey("EmployeeId")]
        [Required]
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }



    }
}
