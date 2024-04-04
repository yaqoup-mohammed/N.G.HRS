using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Controllers;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.EmployeesAffsirs.Models
{
    public class EmployeeMovements  
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "التاريخ")]
        public DateTime  Date { get; set; }
        [Required]
        [Display(Name = "تاريخ النقل")]
        public DateTime DateDown { get; set; }
        [Display(Name = "ملاحظات")]
        public string? Note { get; set; }

        
        //==============================================

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        //==============================================
       
        public int jopdescriptionId { get; set; }
        public JobDescription? jopdescription { get; set; }



        public string? CurrentJop { get; set; }
        public string? LastJop { get; set; }
    }
}
