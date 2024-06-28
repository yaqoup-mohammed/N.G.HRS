using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.GeneralConfiguration.Models
{
    public class Contracts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name =" اسم العقد")]
        
        public string? Name { get; set; }
        [StringLength(255)]
        [Display(Name = "  الملاحظات")]

        public string? Notes { get; set; }
        //==========================================
        public List<ContractTerms>? contractTermsList { get; set; }
    }
}
