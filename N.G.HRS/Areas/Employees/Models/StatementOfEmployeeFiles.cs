using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Employees.Models
{
    public class StatementOfEmployeeFiles
    {
        //يرتبط بجدول ملفات الموظفين والموظفين
        [Key]
        public int Id { get; set; }
        public bool FilesStatus { get; set; }
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
        //==========================================
            public List<Employee> EmployeesList { get; set; }
        //====================================================
        public ICollection<FunctionalFiles> FunctionalFiles { get; set; }
    }
}
