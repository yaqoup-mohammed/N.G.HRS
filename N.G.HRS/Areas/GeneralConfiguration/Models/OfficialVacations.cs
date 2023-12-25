using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class OfficialVacations
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string VacationsName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly FromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly ToDate { get; set; }

    }
}
