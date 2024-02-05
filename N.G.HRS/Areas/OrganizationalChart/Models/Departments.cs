using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Departments : Base//الادارات
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string SubAdministration { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //=========================================
        //=
        public List<Sections>? SectionsList { get; set; }
        //=
        public List<LinkingEmployeesToShiftPeriods>? LinkingEmployeesToShiftPeriodsList { get; set; }
        //=

        //=========================================
        [ForeignKey("SectorsId")]
        public int? SectorsId { get; set; }
        public Sectors? Sectors { get; set; }

    }
}
