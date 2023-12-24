using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.Employees.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [Range(0, 500000)]
        public int EmployeeNumber { get; set; }
        [Required]
        [StringLength(170)]
        public string EmployeeName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Employment")]
        public DateOnly DateOfEmployment { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Placement Date")]
        public DateOnly PlacementDate { get; set; }//تاريخ التثبيت
        [Required]
        [StringLength(100)]
        public string EmploymentStatus { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Rehire Date")]
        public DateOnly RehireDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Stopping Work")]
        public DateOnly DateOfStoppingWork { get; set; }
        public bool UsedFingerprint { get; set; }
        public bool SubjectToInsurance { get; set; }//خاضع للتامين 
        [DataType(DataType.Date)]
        [Display(Name = "Date Insurance ")]
        public DateOnly DateInsurance { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //يرتبط مع جدول(الادارة) وجدول (القسم) وجدول (الوصف الوظيفي) وجدول (جهاز البصمة) و علاقة(self)




    }
}
