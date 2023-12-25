using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class FunctionalFiles
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string FileName { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
    }
}
