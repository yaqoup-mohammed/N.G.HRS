using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class PublicHolidays//الاجازات العامة
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
        //=========================================
        [ForeignKey("OpeningBalancesForVacationsId")]
        public int OpeningBalancesForVacationsId { get; set; }
        public OpeningBalancesForVacations openingBalancesForVacations { get; set; }
    }
}
