using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Models
{
    public class JobDescription
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]     
        public string JopName { get; set; }
        [Required]
        [StringLength(255)]
        public string JobQualifications { get; set; }
        [Required]
        [StringLength(255)]
        public string Authorities { get; set; }
        [Required]
        [StringLength(255)]
        public string Responsibilities { get; set; }
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
        //============================================
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
