using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Employees.Models
{
    public class PersonalData
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("EmployeeId")]
        [Required]
        public int EmployeeId { get; set; }//
        public virtual Employee? employee { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الميلاد")]
        public DateOnly DateOfBirth { get; set; }//
        [Required]
        [Range(0, 100, ErrorMessage = "يجب ان يكون العمر اكبر من الصفر")]
        public int Age { get; set; }//


        [ForeignKey("SexId")]
        [Required]
        public int SexId { get; set; }//
        public virtual Sex? Sex { get; set; }

        [ForeignKey("NationalityId")]
        [Required]
        public int NationalityId { get; set; }//
        public virtual Nationality? Nationality { get; set; }

        [ForeignKey("ReligionId")]
        [Required]
        public int ReligionId { get; set; }//
        public virtual Religion? Religion { get; set; }

        [ForeignKey("MaritalStatusId")]
        [Required]
        public int MaritalStatusId { get; set; }//
        public virtual MaritalStatus? MaritalStatus { get; set; }

        //=
        [StringLength(13)]
        [Phone]
        public string? HomePhone { get; set; }//
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }//
        [Required]
        [StringLength(13)]
        [Phone]
        public string PhoneNumber { get; set; }//
        [Required]
        [StringLength(255)]
        public string? Address { get; set; }//
        [StringLength(255)]
        public string? Notes { get; set; }//
        [Required]
        [StringLength(100)]
        public string CardType { get; set; }//
        [Required]
        [StringLength(100)]
        public string ToRelease { get; set; }//
        [Required]
        [Range(0, 1000000000)]
        public int CardNumber { get; set; }//
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الاصدار")]
        public DateOnly ReleaseDate { get; set; }//
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الانتهاء")]
        public DateOnly CardExpiryDate { get; set; }//

        //يتم الربط مع جدول (الجنس) وجدول (الجنسية) وجدول( الديانة) و(الموظف)و (الحالة الاجتماعية)
        //============================================================

        [ForeignKey("GuaranteesId")]
        [Required]
        public int GuaranteesId { get; set; }//
        public virtual Guarantees? guarantees { get; set; }

        //============================================================






    }
}
