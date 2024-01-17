using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class StaffTime//دوام الموظفين
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly WorksFullTimeFromDate { get; set; }
        //====================================================
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        //=
        [ForeignKey("PermanenceModelsId")]
        public int PermanenceModelsId { get; set; }
        public PermanenceModels PermanenceModels { get; set; }
        //=
        [ForeignKey("SectionsId")]
        public int SectionsId { get; set; }
        public Sections Sections { get; set; }



    }
}
