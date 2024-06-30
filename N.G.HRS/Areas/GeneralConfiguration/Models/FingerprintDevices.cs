using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class FingerprintDevices : Base
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "أسم الجهاز")]
        public string DevicesName { get; set; }
        [Required]
        [Display(Name = "رقم الجهاز")]
        [Range(1, 1000, ErrorMessage = "رقم الجهاز يجب ان يكون بين 1 - 1000")]
        public int DevicesNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "نوع الجهاز")]

        public string DeviceType { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "حالة الجهاز")]

        public string DeviceStatus { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "نوع الاتصال")]
        public string? ConnectionType { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الشراء")]
        public DateOnly DateOfPurchase { get; set; }
        [StringLength(150)]
        [Display(Name = "اسم المورد")]
        public string? VendorName { get; set; }
        [StringLength(13)]
        [Phone]
        [Display(Name = "هاتف المورد")]
        public string? VendorPhon{ get; set;}
        [StringLength(255)]
        [Display(Name = "عنوان المورد")]
        public string? VendorAdress { get; set;}
        [Required]
        [StringLength(255)]
        [Display(Name = "الشركة المصنعة")]
        public string ManufactureCompany { get; set;}
        [Required]
        [StringLength(255)]
        [Display(Name = "وصف الجهاز")]
        public string DeviceSpecifications { get; set;}
        [Display(Name = "عنوان IP")]
        [DataType(DataType.Text)]
        public string? IpAddress { get; set;}
        [Display(Name = " متصل")]
        public bool IsConnected { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }
        //======================================
    }
}
