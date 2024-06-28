using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Religion
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "اسم الدين مطلوب!!")]
        [Display (Name = "اسم الدين")]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(255)]
        [Display (Name = "الملاحضة")]
        public string? Notes { get; set; }
        //======================================================
        public List<PersonalData>?  PersonalDataList { get; set; }
    }
}
