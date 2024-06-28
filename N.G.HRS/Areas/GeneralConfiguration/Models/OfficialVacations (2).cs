using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class OfficialVacations
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " أسم الاجازة الرسمية مطلوب!!")]
        [Display (Name = " أسم الاجازة الرسمية ")]
        [StringLength(150)]
        public string VacationsName { get; set; }
        [Display (Name =" تاريخ البدء ")]
        [Required(ErrorMessage ="تاريخ البدء مطلوب!!")]
        [DataType(DataType.Date)]
        public DateOnly FromDate { get; set; }
        [Display (Name =" تاريخ الانتهاء ")]
        [Required(ErrorMessage ="تاريخ الانتهاء مطلوب!!")]
        [DataType(DataType.Date)]
        public DateOnly ToDate { get; set; }

    }
}
