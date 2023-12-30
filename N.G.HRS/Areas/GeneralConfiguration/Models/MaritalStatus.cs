using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class MaritalStatus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }

        //==========================================
        public int PersonalDataId { get; set; }
        public PersonalData PersonalData { get; set; }

        //==========================================
        public int GuaranteesId { get; set; }
        public Guarantees guarantees { get; set; }
       
        

    }
}
