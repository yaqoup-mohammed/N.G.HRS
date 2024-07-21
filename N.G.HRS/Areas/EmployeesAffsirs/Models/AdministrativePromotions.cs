using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.EmployeesAffsirs.Models
{
    public class AdministrativePromotions//الترفيعات الادارية
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "التاريخ")]
        public DateTime Date { get; set; }
        [Display(Name = "الموظف")]
        public int? EmployeeId { get; set; }
        [Display(Name = "الموظف")]

        public Employee? Employee { get; set; }
        [Display(Name = "الادارة")]
        public int DepartmentsId { get; set; }
        [Display(Name = "الادارة")]

        public Departments? Departments { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "من تاريخ")]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "الى تاريخ")]
        public DateTime ToDate { get; set; }
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }


    }
}
