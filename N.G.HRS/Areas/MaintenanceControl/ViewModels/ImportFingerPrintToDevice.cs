using N.G.HRS.Areas.GeneralConfiguration.Models;

namespace N.G.HRS.Areas.MaintenanceControl.ViewModels
{
    public class ImportFingerPrintToDevice
    {
        public FingerprintDevices FingerprintDevices { get; set; }
        public List<FingerprintDevices> FingerprintDevicesList { get; set; }
        public string path { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
