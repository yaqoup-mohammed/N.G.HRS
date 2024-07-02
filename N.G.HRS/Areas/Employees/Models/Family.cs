using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Employees.Models
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
        [ForeignKey("RelativesTypeId")]
        [Required]
        public int RelativesTypeId { get; set; }
        public virtual RelativesType? RelativesType { get; set; }
        //=====================================================
        [ForeignKey("EmployeeId")]
        [Required]
        public int EmployeeId { get; set; }
        public virtual Employee? Employees { get; set; }
    }
}
