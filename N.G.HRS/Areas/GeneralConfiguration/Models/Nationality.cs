using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Nationality
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string NationalityName { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //===========================================
        public List<PersonalData> personalDatasList { get; set; }
    }
}
