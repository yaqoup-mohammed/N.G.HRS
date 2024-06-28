using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Governorate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "المحافظة")]
        public string Name { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //==================================

        [ForeignKey("CountryId")]   
        [Display(Name = "الدولة")]
        public int? CountryId { get; set; }
        [Display(Name = "الدولة")]

        public Country? CountryOne { get; set; }
        //=
        public List<Branches>? BranchesList { get; set; }

        //=

        public List<Directorate>? DirectoratesList { get; set; }
        //==========================================


    }
}
