using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class BoardOfDirectors
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        public DateOnly? Date { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [StringLength(150)]
        [Display(Name = "اسم المجلس")]

        public string? CouncilName { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [StringLength(150)]
        [Display(Name = "الاسم")]
        public string? NameOfMembership { get; set; }
        //=========================================

        public List<Company>? CompanyList { get; set; }

        //=
        [ForeignKey("MembershipOfTheBoardOfDirectorsId")]
        [Display(Name = "اسم العضوية")]

        public int? MembershipOfTheBoardOfDirectorsId { get; set; }
        [Display(Name = "اسم العضوية")]

        public MembershipOfTheBoardOfDirectors? MembershipOfTheBoardOfDirectors { get; set; }

        //==========================================
    }
}
