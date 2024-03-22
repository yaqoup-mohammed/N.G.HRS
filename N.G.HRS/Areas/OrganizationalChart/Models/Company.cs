using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحق مطلوب!!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date.yyyy")]
        [Display(Name = "التاريخ")]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required(ErrorMessage = "هذا الحق مطلوب!!")]
        [StringLength(150)]
        [Display(Name = "اسم الشركة")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "هذا الحق مطلوب!!")]
        [Range(0, 1000000000)]
        [Display(Name = "رقم الرخصة")]
        public int LicenseNumber { get; set; }
        [Required(ErrorMessage = "هذا الحق مطلوب!!")]
        [StringLength(50)]
        [Display(Name = "نوع نشاط الشركة")]
        public string TypeOfBusinessActivity { get; set; }
        [Required(ErrorMessage = "هذا الحق مطلوب!!")]
        [StringLength(255)]
        [Display(Name = "شعار الشركة")]
        public string? ComponyLogo { get; set; }
        [Required(ErrorMessage = "هذا الحق مطلوب!!")]
        [StringLength(255)]
        [Display(Name = "عنوان الشركة")]
        public string? ComponyAddress { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //====================================================================
        //=
        //=
        public List<Branches>? BranchesList { get; set; }
        //===
        [ForeignKey("BoardOfDirectorsId")]
        [Display(Name = "مجلس الادارة")]
        public int? BoardOfDirectorsId { get; set; }
        public BoardOfDirectors? BoardOfDirectors { get; set; }


        //=
        //====================================================================


    }
}
