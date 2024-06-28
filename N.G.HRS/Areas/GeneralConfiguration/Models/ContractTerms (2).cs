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
        public string ModelName { get; set; }
        [Required]
        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        [Column(TypeName = "text")]
        public string? StatementOfConditions { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //=========================================================
        [ForeignKey("ContractsId")]
        public int? ContractsId { get; set; }
        public Contracts? Contracts { get; set; }
        //==========================================================
    }
}
