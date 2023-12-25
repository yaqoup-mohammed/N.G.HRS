using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class DepartmentAccounts
    {
        [Key]
        public int id { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //الربط مع جدول (.الاقسام) وجدول (الحسابات) و جدول (انواع الحسابات) .
    }
}
