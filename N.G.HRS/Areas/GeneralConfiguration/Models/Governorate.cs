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
        public string Name { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //==================================
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public Country CountryOne { get; set; } = default!;
        //==========================================

        public List<Directorate> directoratesList { get; set; }

    }
}
