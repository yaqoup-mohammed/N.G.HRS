using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class MembershipOfTheBoardOfDirectors
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب !!")]
        [StringLength(150)]
        [Display(Name = "اسم العضوية")]
        public string? TypeOFMembership { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //==============================================
        public List<BoardOfDirectors>? BoardOfDirectorsList { get; set; }


    }
}
