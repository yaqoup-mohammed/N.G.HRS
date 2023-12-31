using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace N.G.HRS.Areas.Employees.Models
{
    public class Guarantees//الضمين
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(13)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(100)]
        public string NameOfTheBusiness { get; set; }//الاسم التجاري
        [Required]
        public int CommercialRegistrationNo { get; set; }//رقم السجل التجاري
        [Required]
        [StringLength(255)]
        public string ShopAddress { get; set; }
        [Required]
        [StringLength(255)]
        public string HomeAdress { get; set; }
        [Required]
        [Range(0, 50)]
        public int NumberOfDependents { get; set; }
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
        //=============================================
        public int PersonalDataId { get; set; }
        public PersonalData personalData { get; set; }
        //================================================
        public List<MaritalStatus> maritalStatusList { get; set; }

        //يرتبط مع جدول(الموظف)وجدول (الضمين)
        //==============================================

    }
}
