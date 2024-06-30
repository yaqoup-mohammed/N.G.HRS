using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.FingerPrintSetting
{
    public class MachineInfo
    {
        public int Id { get; set; }
        public int MachineNumber { get; set; }
        public string? EmployeeName { get; set; }
        public int SectionId { get; set; }
        public Sections Section { get; set; }
        public int DepartmentId { get; set; }
        public Departments Department { get; set; }
        public string? State { get; set; }
        public int IndRegID { get; set; }
        public string? DateTimeRecord { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly DateOnlyRecord { get; set; }
        [DataType(DataType.Time)]

        public DateTime? TimeOnlyRecord { get; set; }
        public bool IsProcessed { get; set; }
    }
}