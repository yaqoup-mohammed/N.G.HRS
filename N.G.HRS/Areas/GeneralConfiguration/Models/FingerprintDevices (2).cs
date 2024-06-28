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
        public string DevicesName { get; set; }

        [Required]
        [StringLength(50)]
        public string DeviceType { get; set; }
        [Required]
        [StringLength(50)]
        public string DeviceStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string? ConnectionType { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfPurchase { get; set; }
        [Required]
        [StringLength(150)]
        public string VendorName { get; set; }
        [Required]
        [StringLength(13)]
        [Phone]
        public string VendorPhon{ get; set;}
        [Required]
        [StringLength(255)]
        public string VendorAdress { get; set;}
        [Required]
        [StringLength(255)]
        public string ManufactureCompany { get; set;}
        [Required]
        [StringLength(255)]
        public string DeviceSpecifications { get; set;}
        public string? IpAddress { get; set;}
        public bool IsConnected { get; set; }=false;

        [StringLength(255)]
        public string? Notes { get; set; }
        //======================================
    }
}
