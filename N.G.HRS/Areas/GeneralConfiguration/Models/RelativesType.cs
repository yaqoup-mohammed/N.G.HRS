using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class RelativesType
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " من فضلك ادخل الاسم ") ]
        [Display(Name = " اسم القرابة  ")]
        [StringLength(150)]
        public string RelativeName { get; set; }
        [StringLength(255)]
        [Display ( Name = "الملاحظات")]
        public string? Notes { get; set; }
        //==============================================
        public List<Family>? FamiliesList { get; set; }
    }
}
