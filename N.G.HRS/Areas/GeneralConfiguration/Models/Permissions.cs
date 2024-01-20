using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Permissions
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string PermissionName { get; set; }
        public bool PermissionStatus { get; set; }
        [Required]
        [Range(0,100)]
        public int PermissionsDuration { get; set; }
        [Required]
        [Range(0,30)]
        public int RepeatPermissionDuringTheMonth { get; set; }
        public bool Paid { get; set; }
        [StringLength(255)]
        public string? Notes { get; set;}

    }
}
