using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.MaintenanceControl.Models
{
    public class AdditionalUnsupportedEmployees//الإضافي الغير معتمد 
    {
        public int Id { get; set; }
        [Display(Name = "الموظف")]
        public int EmployeeId { get; set; }
        [Display(Name = "الموظف")]

        public Employee Employee { get; set; }
        [Display(Name = "من وقت")]

        public DateTime FromDate { get; set; }
        [Display(Name = "الى وقت")]

        public DateTime ToDate { get; set; }
        [Display(Name = "الإضافي الغير معتمد")]

        public int AdditionalUnsupported { get; set; }
        [Display(Name = "الإضافي معتمد")]

        public int AdditionalSupported { get; set; }
        [Display(Name = "الملاحظة")]

        public string? Note { get; set; }
        public bool migration { get; set; }//ترحيل
    }
}
