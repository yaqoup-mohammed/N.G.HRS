using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class RelativesType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string RelativeName { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
    }
}
