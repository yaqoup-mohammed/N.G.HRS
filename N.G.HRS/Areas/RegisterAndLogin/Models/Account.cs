using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.RegisterAndLogin.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "الرقم الوظيفي")]
        public string EmployeeId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "الايميل")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = " تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

       
        [Display(Name = "العرض")]
        public bool CanShow { get; set; }

        [Display(Name = " التعديل")]
        public bool CanEdit { get; set; }

        [Display(Name = "الاضافة")]
        public bool CanAdd { get; set; }

        [Display(Name = " الحذف")]
        public bool CanDelete { get; set; }
       
        [Display(Name = "عرض الصور الرجال")]
        public bool ShowMaleImages { get; set; }

        [Display(Name = " عرض الصور النساء")]
        public bool ShowFemaleImages { get; set; }
    }
}
