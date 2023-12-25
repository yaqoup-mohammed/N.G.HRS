using System.ComponentModel.DataAnnotations;

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
    }
}
