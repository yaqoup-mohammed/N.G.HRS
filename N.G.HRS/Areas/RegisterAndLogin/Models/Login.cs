using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace N.G.HRS.Areas.RegisterAndLogin.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "رقم الوظيفي")]
        public string JobNumber { get; set; }
    }
}
