using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.Employees.Models
{

    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, 500000)]
        public int EmployeeNumber { get; set; }
        [Required]
        [StringLength(170)]
        public string EmployeeName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Employment")]
        public DateOnly DateOfEmployment { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Placement Date")]
        public DateOnly PlacementDate { get; set; }//تاريخ التثبيت
        [Required]
        [StringLength(100)]
        public string EmploymentStatus { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Rehire Date")]
        public DateOnly? RehireDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Stopping Work")]
        public DateOnly? DateOfStoppingWork { get; set; }
        public bool UsedFingerprint { get; set; }
        public bool SubjectToInsurance { get; set; }//خاضع للتامين 
        [DataType(DataType.Date)]
        [Display(Name = "Date Insurance ")]
        public DateOnly? DateInsurance { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //يرتبط مع جدول(الادارة) وجدول (القسم) وجدول (الوصف الوظيفي) وجدول (جهاز البصمة) و علاقة(self)
        //========================================================
        [ForeignKey("DepartmentsId")]
        public int? DepartmentsId { get; set; }
        public Departments? Departments { get; set; }
        //=
        [ForeignKey("SectionsId")]
        public int? SectionsId { get; set; }
        public Sections? Sections { get; set; }
        //=
        [ForeignKey("JobDescriptionId")]
        public int? JobDescriptionId { get; set; }
        public JobDescription? JobDescription { get; set; }
        //
        [ForeignKey("PracticalExperiencesId")]
        public int? PracticalExperiencesId { get; set; }
        public PracticalExperiences? PracticalExperiences { get; set; }
        //
        //=
        [ForeignKey("StatementOfEmployeeFilesId")]
        public int? StatementOfEmployeeFilesId { get; set; }
        public StatementOfEmployeeFiles? StatementOfEmployeeFiles { get; set; }
        //=       
        [ForeignKey("TrainingCoursesId")]
        public int? TrainingCoursesId { get; set; }
        public TrainingCourses? TrainingCourses { get; set; }
        //=
        [ForeignKey("FingerprintDevicesId")]
        public int? FingerprintDevicesId { get; set; }
        public FingerprintDevices? FingerprintDevices { get; set; }
        //=============================
        public EmployeeArchives? employeeArchives { get; set; }
        //========================================================
        public PersonalData? personalData { get; set; }
        public FinancialStatements? financialStatements { get; set; }
        public Family? Families { get; set; }
        //===============================================
        //{
        //          |
        //علاقة self v
        public int? ManagerId { get; set; }
        // Navigation property for Manager
        public Employee? Manager { get; set; }
        // Navigation property for Subordinates 
        public List<Employee>? Subordinates { get; set; }
        //}
        //=================================================
        public List<EmployeeAccount>? EmployeeAccountList { get; set; }
        //=
        public List<StaffTime>? StaffTimeList { get; set; }
        //=
        public List<LinkingEmployeesToShiftPeriods>? LinkingEmployeesToShiftPeriodsList { get; set; }

        //=
        public List<OneFingerprint>? OneFingerprintList { get; set; }

        //=
        public List<OpeningBalancesForVacations>? OpeningBalancesForVacationsList { get; set; }

        //=

        //=

        //=

        //====================================
        public ICollection<Qualifications>? qualifications { get; set; }



    }
}
