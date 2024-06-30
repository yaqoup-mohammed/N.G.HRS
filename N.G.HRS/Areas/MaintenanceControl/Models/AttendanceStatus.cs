using Microsoft.EntityFrameworkCore;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.MaintenanceControl.Models
{
    public class AttendanceStatus
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<AttendanceAndAbsenceProcessing>? AttendanceAndAbsenceProcessingList { get; set; }
    }
   
}
