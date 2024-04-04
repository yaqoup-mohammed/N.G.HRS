using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Branches
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب!!")]
        [StringLength(150)]
        [Display(Name = "اسم الفرع")]
        public string BranchesName { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب!!")]
        [StringLength (255)]
        [Display(Name = "عنوان الفرع")]
        public string? BranchesAdress { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب!!")]
        [StringLength (13)]
        [Phone]
[Display(Name = "رقم الهاتف")]
        public string? BranchesPhone { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب!!")]
        [EmailAddress]
[Display(Name = "البريد الالكتروني")]
        public string? BranchesEmail { get; set; }
        
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //==========================================
        //=

        //=
        public List<Sectors>? SectorsList { get; set; }

        //=
        [ForeignKey("CompanyId")]
        [Display(Name = "الشركة ")]

        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        //=
        [ForeignKey("CountryId")]
        [Display(Name = "الدولة")]
        public int? CountryId { get; set; }
        public Country? Country { get; set; }
        //=
        [ForeignKey("GovernorateId")]
        [Display(Name = "المحافظة")]
        public int? GovernorateId { get; set; }
        public Governorate? Governorate { get; set; }
        //=
        [ForeignKey("DirectorateId")]
        [Display(Name = "المديرية")]
        public int? DirectorateId { get; set; }
        public Directorate? Directorate { get; set; }
        //======
        //===
        //==========================================
    }
}
