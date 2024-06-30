using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "هذا الحقل مطلوب")]
        [StringLength(150)]
        [Display(Name = "الدولة")]
        public string? Name { get; set; }
      
        public string? Data { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }
        //=======================================================
        public List<Governorate>? governoratesList { get; set; }
        //==
        public List<Branches>? BranchesList { get; set; }



        //========================================================



    }
}
