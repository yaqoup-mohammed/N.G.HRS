using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.Employees.Models
{
    public class PersonalData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الميلاد")]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        [Range(0, 100)]
        public int Age { get; set; }
        [Required]
        [StringLength(13)]
        [Phone]
        public string HomePhone { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(13)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        [Required]
        [StringLength(100)]
        public string CardType { get; set; }
        [Required]
        [Range(0, 15)]
        public int CardNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الاصدار")]
        public DateOnly ReleaseDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الانتهاء")]
        public DateOnly CardExpiryDate { get; set; }

        //يتم الربط مع جدول (الجنس) وجدول (الجنسية) وجدول( الديانة) و(الموظف)و (الحالة الاجتماعية)
        //============================================================
        public Employee employee { get; set; }
        public Guarantees guarantees { get; set; }

        //============================================================
        public List<Sex> sexList { get; set; }
        public List<Nationality> nationalitiesList { get; set; }
        public List<Religion> religionsList { get; set; }
        public List<MaritalStatus> maritalStatusList1 { get; set; }
        



    }
}
