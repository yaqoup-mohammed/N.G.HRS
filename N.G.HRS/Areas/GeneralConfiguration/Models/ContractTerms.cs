using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class ContractTerms
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "اسم النموذج")]

        public string ModelName { get; set; }
        [Required]
        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        [Column(TypeName = "text")]
        [Display(Name = "نص العقد")]

        public string? StatementOfConditions { get; set; }
        [StringLength(255)]
        [Display(Name = "الملاحظات")]

        public string? Notes { get; set; }
        //=========================================================
        [ForeignKey("ContractsId")]
        [Display(Name = "اسم العقد")]

        public int? ContractsId { get; set; }
        [Display(Name = "اسم العقد")]

        public Contracts? Contracts { get; set; }
        //==========================================================
    }
}
