using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace N.G.HRS.Areas.AalariesAndWages.Models
{
    public class BasicDataForWagesAndSalaries
    {
        

        [Key]
        public int Id { get; set; }
        [Range(0, 31)]
        [Display(Name = "عدد ايام الشهر")]
        public int? NumberOfMonthsDays {get;set;}
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "يرجى إدخال أرقام فقط")]

        [Range(0, 99999999.99)]
        [Display(Name = "الغياب بالساعة")]

        public decimal AbsencePerHour { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "يرجى إدخال أرقام فقط")]
        [Range(0, 99999999.99)]
        [Display(Name = "التأخبر بالدقيقة")]

        public decimal DelayPerHour { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "يرجى إدخال أرقام فقط")]
        [Range(0, 9999999.99)]
        [Display(Name = "تأخبر البصمة الواحدة بالساعة")]

        public decimal OneFingerPrintPerHourDelay { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "من تاريخ")]
        public DateOnly FromDate { get; set; }/*= DateOnly.FromDateTime(DateTime.Now.Month == 1 ? new DateTime(DateTime.Now.Year - 1, 12, 1) : new DateTime(DateTime.Now.Year, 1, 1));*/

        [DataType(DataType.Date)]
        [Display(Name = "الى تاريخ")]
        public DateOnly ToDate { get; set; }

        [StringLength(150)]
        [Display(Name = "نوع الاجر")]
        public string? TypeOfWage { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]

        public string? Notes { get; set; }


    }
}
