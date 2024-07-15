using N.G.HRS.Areas.MaintenanceControl.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Permissions
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "من فضلك ادخل أسم الأذون !!")]
        [Display(Name = " أسم الأذن  ")]

        [StringLength(150)]
        public string PermissionName { get; set; }
        [Display (Name = "تفعيل الأذن")]
        public bool PermissionStatus { get; set; }
        [Required (ErrorMessage = "من فضلك ادخل مدة الأذن !!")]
        [Display(Name = "مدة الأذن  ")]
        [Range(0,100)]
        public int PermissionsDuration { get; set; }
        [Required (ErrorMessage = "من فضلك ادخل عدد مرات الأذن !!")]
        [Display(Name = "تكرار الإذن خلال الشهر  ")]
        [Range(0,30)]
        public int RepeatPermissionDuringTheMonth { get; set; }
        [Display (Name = "مدفوعة")]
        public bool Paid { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set;}

        //========================================= 
        public List<EmployeePermissions>? EmployeePermissionsList { get; set; }


    }
}
