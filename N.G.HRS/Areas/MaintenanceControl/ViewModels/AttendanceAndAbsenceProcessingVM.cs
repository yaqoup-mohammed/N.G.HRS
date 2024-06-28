using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;

namespace N.G.HRS.Areas.MaintenanceControl.ViewModels
{
    public class AttendanceAndAbsenceProcessingVM
    {
        public Departments? Departments { get; set; }
        public Sections? Sections { get; set; }
        public Employee? Employee { get; set; }
        public IEnumerable<AdditionalExternalOfWork>? AddExtOfWorkList { get; set; }
        public IEnumerable<AdditionalExternalOfWork>? AddWorkList { get; set; }
        public IEnumerable<AdditionalUnsupportedEmployees>? AdditionalUnsupportedEmployeesList { get; set; }
        public IEnumerable<AttendanceRecord>? AttendanceRecordList { get; set; }
        public IEnumerable<EmployeePermissions>? EmployeePermissionsList { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
     
    }
}
