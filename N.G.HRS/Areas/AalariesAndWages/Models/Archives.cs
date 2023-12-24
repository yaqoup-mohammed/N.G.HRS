using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class Archives
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateOnly Date { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        [Required]
        [Range(0, 255)]
        public string? Description { get; set; }
        public string File { get; set; }
    }
}
