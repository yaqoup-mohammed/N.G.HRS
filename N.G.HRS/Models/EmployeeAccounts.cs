using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Models
{
    public class EmployeeAccounts
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
        //يتم الربط مع جدول (الموظفين) وجدول (الحسابات) وجدول(نوع الحسابات)
    }
}
