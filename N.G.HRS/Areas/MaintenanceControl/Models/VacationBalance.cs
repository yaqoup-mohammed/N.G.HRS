using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.MaintenanceControl.Models
{
    public class VacationBalance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employees { get; set; }
        [Display(Name = "الافتتاحي")]
        public int Editorial { get; set; }  //الافتتاحي
        [Display(Name = "السنوي")]
        public int Annual { get; set; } //السنوي
        [Display(Name = "المنقول")]
        public int Transferred { get; set; } //المنقول
        [Display(Name = "المستهلك")]
        public int Expendables { get; set; }//المستهلك
        [Display(Name = "المتبقي")]
        public int Residual { get; set; }//المتبقي
        public int ShiftHour { get; set; }
    }
}
