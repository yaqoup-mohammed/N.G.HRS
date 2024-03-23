using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Permissions
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "من فضلك ادخل أسم الأذون !!")]
        [Display(Name = " أسم الأذون  ")]

        [StringLength(150)]
        public string PermissionName { get; set; }
        [Display (Name = "حالة الأذون")]
        public bool PermissionStatus { get; set; }
        [Required (ErrorMessage = "من فضلك ادخل مدة الأذون !!")]
        [Display(Name = "مدة الأذون  ")]
        [Range(0,100)]
        public int PermissionsDuration { get; set; }
        [Required (ErrorMessage = "من فضلك ادخل عدد مرات الأذون !!")]
        [Display(Name = "عدد مرات الأذون  ")]
        [Range(0,30)]
        public int RepeatPermissionDuringTheMonth { get; set; }
        [Display (Name = "مدفوعة")]
        public bool Paid { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحضة")]
        public string? Notes { get; set;}

    }
}
