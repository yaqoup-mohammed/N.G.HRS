using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.FingerPrintSetting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Departments : Base//الادارات
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [StringLength(150)]
        [Display(Name = "الادارة")]
        public string SubAdministration { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        //=========================================
        //=
        public List<Sections>? SectionsList { get; set; }
        //=
        public List<LinkingEmployeesToShiftPeriods>? LinkingEmployeesToShiftPeriodsList { get; set; }
        //=
        //=
        public List<EmployeeAdvances>? EmployeeAdvancesList { get; set; }
        //=
        public List<AdministrativePromotions>? AdministrativePromotionsList { get; set; }
        //=
        public List<AttendanceAndAbsenceProcessing>? AttendanceAndAbsenceProcessingList { get; set; }
        //=
        public List<MachineInfo>? MachineInfoList { get; set; }

        //=========================================
        [ForeignKey("SectorsId")]
        [Display(Name = "القطاع")]
        public int? SectorsId { get; set; }
        public Sectors? Sectors { get; set; }

    }
}
