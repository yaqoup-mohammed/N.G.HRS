using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.Employees
{
    public class Family
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //======================================================
        public List<RelativesType> relativesTypesList { get; set;}
    }
}
