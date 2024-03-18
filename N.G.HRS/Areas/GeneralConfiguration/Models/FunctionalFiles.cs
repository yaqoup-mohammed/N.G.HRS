using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class FunctionalFiles
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "اسم الملف  الوظيفية مطلوب!!")]
        [Display (Name = " اسم الملف الوظيفية")]
        [StringLength(150)]
        public string FileName { get; set; }
        [StringLength(255)]
        [Display (Name = " الملاحظة")]
        public string? Notes { get; set; }
        //===============================================
        public ICollection<StatementOfEmployeeFiles>? StatementOfEmployeeFiles { get; set;}
    }
}
