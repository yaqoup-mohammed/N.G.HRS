using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.MaintenanceControl.Models
{
    public class AdditionalExternalOfWork//تكليف عمل خارجي والاضافي
    {
        public int Id { get; set; }
        [Display(Name = "الموظف")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int EmployeeId { get; set; }
        [Display(Name = "الموظف")]
        public Employee? Employee { get; set; }
        [Display(Name = "الموظف البديل")]
        public int? SubstituteEmployeeId { get; set; }
        [Display(Name = "الموظف البديل")]
        public Employee? SubstituteEmployee { get; set; }
        [Display(Name = "التاريخ")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
        [Display(Name = "من تاريخ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FromDate { get; set; }
        [Display(Name = "إلى تاريخ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }
            [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "من وقت")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss tt}")]
        public DateTime FromTime { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "إلى وقت")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss tt}")]
        public DateTime ToTime { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الساعات")]
        public int Hours { get; set; }
        [Display(Name = "إجمالي الساعات")]
        public int TotalHours { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الدقائق")]
        public int Minutes { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "المهمة")]
        [DataType(DataType.MultilineText)]
        public string? Mission { get; set; }
        [Display(Name = "جهة المهمة")]
        public string? TaskDestination { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "نوع التكليف")]
        public int AssignmentId { get; set; }
        [Display(Name = "نوع التكليف")]
        public Assignment? Assignment { get; set; }
        public bool BetweenToDate { get; set; }
        [Display(Name = "الملاحظات")]
        public string? Note { get; set; }
        public bool IsProccessed { get; set; }







    }
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AdditionalExternalOfWork> AdditionalExternalOfWorkList { get; set;}
    }
}
