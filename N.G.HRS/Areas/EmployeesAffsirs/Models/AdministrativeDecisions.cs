using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.EmployeesAffsirs.Models
{
    public class AdministrativeDecisions//القرارات الادارية
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "طبيعة التوظيف")]
        public string? EmployeeStatus { get; set; }
        [Display(Name = "الموظف")]
        public int EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public Employee? Employee { get; set; }
        [Display(Name = "تاريخ بداية الاجر")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SalaryStartDate { get; set; }
        [Display(Name = "تاريخ نهاية الاجر")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SalaryEndtDate { get; set; }
        [Display(Name = "الراتب")]
        public decimal Salary { get; set; }
        [Display(Name = "العملة")]
        public int CurrencyId { get; set; }
        [Display(Name = "العملة")]
        public Currency? Currency { get; set; }
        [Display(Name = "أسباب التوظيف")]
        public string? EmployeementReson {  get; set; }
        [Display(Name = "نوع القرار")]
        public string? DecisionsType {  get; set; }
        [Display(Name = " التوظيف بناء على")]
        public string? EmployeementOn {  get; set; }
        [Display(Name = "الملاحظات")]
        public string? Note { get; set; }


    }
}
