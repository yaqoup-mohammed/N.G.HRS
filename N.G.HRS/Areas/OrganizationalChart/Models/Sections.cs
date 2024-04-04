using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using N.G.HRS.Areas.PayRoll.Models;

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
        //=
        public List<LinkingEmployeesToShiftPeriods>? LinkingEmployeesToShiftPeriodsList { get; set; }

        //=
        public List<EmployeeAdvances>? EmployeeAdvancesList { get; set; }
        public List<AutomaticallyApprovedAdd_on>? AutomaticallyApprovedAdd_onList { get; set; }


        //========================================
        [ForeignKey("DepartmentsId")]
        [Display(Name = "الادارة")]
        public int? DepartmentsId { get; set; }
        public Departments? Departments { get; set; }
        //=
        public List<StaffTime>? staffTimeList { get; set; }


    }
}
