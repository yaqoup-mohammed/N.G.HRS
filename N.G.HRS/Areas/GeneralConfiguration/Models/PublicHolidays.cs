using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class PublicHolidays
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string HolidayName { get; set; }
        public bool VacationsBalance { get; set; }
        public bool Paid { get; set;}
        [Required]
        [Range(0,100)]
        public int DayCount { get;set; }

        public int RotationDuration { get; set; }
        
        [StringLength(255)]
        public string? Notes { get; set;}

    }
}
