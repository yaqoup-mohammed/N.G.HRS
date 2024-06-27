using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;

namespace N.G.HRS.Areas.MaintenanceControl.ViewModels
{
    public class UplodeFingerPrintFromDeviceVM
    {
        public Departments Departments {  get; set; }
        public Sections Sections {  get; set; }
        public FingerprintDevices FingerprintDevices { get; set; }
        public Employee Employee { get; set; }
        public List<Employee> EmployeeList { get; set; }
        public List<FingerprintDevices> FingerprintDevicesList { get; set; }
        public List<Departments> DepartmentList { get; set; }
        public List<Sections> SectionList { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}
