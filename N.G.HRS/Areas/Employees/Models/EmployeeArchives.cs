using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Employees.Models
{
    public class EmployeeArchives
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }
        public string? Descriotion { get; set; }
        public string? File { get; set; }
        [NotMapped]
        public IFormFile? FileUpload { get; set; }
        //====================================================
        public int EmployeeId { get; set; }
        public virtual Employee? employee { get; set; }

    }
}
