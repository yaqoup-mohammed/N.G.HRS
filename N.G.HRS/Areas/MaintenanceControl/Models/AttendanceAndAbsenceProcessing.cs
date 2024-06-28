using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.MaintenanceControl.Models
{
    public class AttendanceAndAbsenceProcessing
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public Departments? Department { get; set; }
        public int SectionId { get; set; }
        public Sections? Section { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employees { get; set; }
        public int AttendanceStatusId { get; set; }
        public AttendanceStatus? AttendanceStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        [DisplayFormat(DataFormatString = "{HH:mm tt}", ApplyFormatInEditMode = true)]

        public TimeSpan FromTime { get; set; }
        [DisplayFormat(DataFormatString = "{HH:mm tt}", ApplyFormatInEditMode = true)]
        public TimeSpan ToTime { get; set; }
        public int? TotalWorkMinutes { get; set; }
        public int? MinutesOfLate { get; set; }
        public string? Description { get; set; }
        public int? periodId { get; set; }
        public Periods? periods { get; set; }
        public int? permenenceId { get; set; }
        public PermanenceModels? PermenenceModel { get; set; }
        public bool IsProcssessed { get; set; }
        public bool IsProcssessedBefore { get; set; }
        //public string? UplodeFrom { get; set; }
    }
 
}
