using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Sections
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string SectionsName { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }

        //=============================================
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
