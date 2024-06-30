using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Employees.Models
{
    public class TrainingCourses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "اسم الدورة")]
        public string? NameCourses { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "مكان الحصول عليه")]
        public string? WhereToGetIt { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "من تاريخ")]
        public DateOnly FromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "الى تاريخ")]

        public DateOnly ToDate { get; set; }
        //=====================================================
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public virtual Employee? EmployeeOne { get; set; }
    }
}
