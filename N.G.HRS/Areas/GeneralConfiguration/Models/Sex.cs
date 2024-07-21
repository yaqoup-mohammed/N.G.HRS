using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Sex
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "اسم الجنس مطلوب من فضلك!!")]
        [Display(Name = "اسم الجنس")]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }
        //=================================================
        public List<PersonalData>? PersonalDataList { get; set;}
    }
}
