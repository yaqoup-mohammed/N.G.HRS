using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Departments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string SubAdministration { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //=========================================
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        //=
        [ForeignKey("SectionsId")]
        public int SectionsId { get; set; }
        public Sections sections { get; set; }
        //=========================================
        public List<Sectors> SectorsList { get; set; }

    }
}
