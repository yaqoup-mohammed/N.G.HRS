using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.AalariesAndWages.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.FingerPrintSetting;

namespace N.G.HRS.Areas.OrganizationalChart.Models
{
    public class Sections :Base//الاقسام
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "القسم")]
        public string? SectionsName { get; set; }
        [StringLength(255)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }

        //=============================================
        //=
        public List<SectionsAccounts>? SectionsAccountsList { get; set; }
        public List<StaffVacations>? StaffVacationsList { get; set; }
        //=
        public List<LinkingEmployeesToShiftPeriods>? LinkingEmployeesToShiftPeriodsList { get; set; }

        //=
        public List<EmployeeAdvances>? EmployeeAdvancesList { get; set; }
        public List<AttendanceRecord>? AttendanceRecordList { get; set; }
        public List<AutomaticallyApprovedAdd_on>? AutomaticallyApprovedAdd_onList { get; set; }
        public List<AttendanceAndAbsenceProcessing>? AttendanceAndAbsenceProcessingList { get; set; }
        public List<MachineInfo>? MachineInfoList { get; set; }

        //========================================
        [ForeignKey("DepartmentsId")]
        [Display(Name = "الادارة")]
        public int? DepartmentsId { get; set; }
        public Departments? Departments { get; set; }
        //=
        public List<StaffTime>? staffTimeList { get; set; }


    }
}
