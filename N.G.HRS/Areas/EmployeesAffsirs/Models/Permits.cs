using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.EmployeesAffsirs.Models
{
    public class Permits//التصاريح
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "التاريخ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "نوع التصريح")]
        public string? PermitsType { get; set; }

        [Display(Name = "لغير الموظف")]
        public bool IsNotEmployee { get; set; }
        [Display(Name = "الموظف")]
        public int? EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public virtual Employee? Employee { get; set; }
        [Display(Name = "الاسم")]
        public string? NotEmployee { get; set; }
        [Display(Name = "ادخال / اخراج")]
        public string? For { get; set; }
        [Display(Name = "الغرض")]
        public string? Purpose { get; set; }
        [Display(Name = "الملاحظات")]
        public string? Note { get; set; }

    }
}
