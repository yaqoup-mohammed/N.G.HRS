using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class BoardOfDirectors
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        [Required]
        [StringLength(150)]
        public string CouncilName { get; set; }
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
        [Required]
        [StringLength(150)]
        public string NameOfMembership { get; set; }
        //=========================================
        public List<MembershipOfTheBoardOfDirectors> membershipOfTheBoardOfDirectorsList { get; set; }
        //==========================================
        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        //==========================================
    }
}
