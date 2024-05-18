using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Areas.PayRoll.ModelView;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using zkemkeeper;


namespace N.G.HRS.Areas.Employees.Models
{

    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, 500000)]
        public string? EmployeeNumber { get; set; }//
        [Required]
        [StringLength(170)]
        public string EmployeeName { get; set; }//
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Employment")]
        public DateOnly DateOfEmployment { get; set; }//
        [DataType(DataType.Date)]
        [Display(Name = "Placement Date")]
        public DateOnly PlacementDate { get; set; }//تاريخ التثبيت//
        [Required]
        [StringLength(100)]
        public string EmploymentStatus { get; set; }//
        [DataType(DataType.Date)]
        [Display(Name = "Rehire Date")]
        public DateOnly? RehireDate { get; set; }//
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Stopping Work")]
        public DateOnly? DateOfStoppingWork { get; set; }//
        public bool UsedFingerprint { get; set; }//
        public bool SubjectToInsurance { get; set; }//خاضع للتامين //
        [DataType(DataType.Date)]
        [Display(Name = "Date Insurance ")]
        public DateOnly? DateInsurance { get; set; }//
        public byte FingerPrintImage { get; set; }//
        public string? ImageFile { get; set; }
        [NotMapped]
        public IFormFile ?FileUpload { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }//
        //يرتبط مع جدول(الادارة) وجدول (القسم) وجدول (الوصف الوظيفي) وجدول (جهاز البصمة) و علاقة(self)
        //========================================================
        [ForeignKey("DepartmentsId")]
        [Required]
        public int DepartmentsId { get; set; }//
        public virtual Departments? Departments { get; set; }
        //=====================================
        [ForeignKey("SectionsId")]
        [Required]
        public int SectionsId { get; set; }//
        public virtual Sections? Sections { get; set; }
        //=====================================
        [ForeignKey("JobDescriptionId")]
        [Required]
        public int JobDescriptionId { get; set; }//
        public JobDescription? JobDescription { get; set; }
        //====================================
        public List<PracticalExperiences>? PracticalExperiencesList { get; set; }
        //=====================================
        public List<StatementOfEmployeeFiles>? StatementOfEmployeeFilesList { get; set; }
        //=====================================           
        public List<TrainingCourses>? TrainingCoursesList { get; set; }
        //=====================================           
        public List<EmployeeAdvances>? EmployeeAdvancesList { get; set; }
        //=====================================           
        public List<VacationAllowances>? VacationAllowancesList { get; set; }

        //=====================================
        [ForeignKey("FingerprintDevicesId")]
        public int? FingerprintDevicesId { get; set; }
        public virtual FingerprintDevices? FingerprintDevices { get; set; }
        //=============================
        public virtual EmployeeArchives? employeeArchives { get; set; }
        //========================================================
        public virtual PersonalData? personalData { get; set; }
        public FinancialStatements? financialStatements { get; set; }
       
        //===============================================

        //علاقة self v
        public int? ManagerId { get; set; }//
        // Navigation property for Manager
        public virtual Employee? Manager { get; set; }
        // Navigation property for Subordinates 
        public List<Employee>? Subordinates { get; set; }
    
        //=================================================
        public List<EmployeeAccount>? EmployeeAccountList { get; set; }
        //=====================================
        public List<StaffTime>? StaffTimeList { get; set; }
        //=====================================
        public List<EmployeeLoans>? EmployeeLoansList { get; set; }
        //=====================================
        public List<LinkingEmployeesToShiftPeriods>? LinkingEmployeesToShiftPeriodsList { get; set; }

        //=====================================
        public List<OneFingerprint>? OneFingerprintList { get; set; }
        public List<AdministrativePromotions>? AdministrativePromotionsList { get; set; }

        //====================================
        public List<EntitlementsAndDeductions>? EntitlementsAndDeductionsList { get; set; }

        //====================================
        public List<AutomaticallyApprovedAdd_on>? AutomaticallyApprovedAdd_onList { get; set; }

        //====================================
        public List<OpeningBalancesForVacations>? OpeningBalancesForVacationsList { get; set; }
        public List<EmployeeMovements>? EmployeeMovementsList { get; set; }
        //==================================== 
        public List<Family>? FamilyList { get; set; }
        //==================================== 
        public List<EmployeePerks>? EmployeePerksList { get; set; }
        //====================================
        public List<EndOfServiceClearance>? EndOfServiceClearanceList { get; set; }
        //====================================
        public List<EmploymentStatusManagement>? EmploymentStatusManagementList { get; set; }
        //====================================
        public List<AdministrativeDecisions>? AdministrativeDecisionsList { get; set; }
        //====================================
        public List<AnnualGoals>? AnnualGoalsList { get; set; }
        //==================================== 
        //public List<EmployeeWagesVM>? EmployeeWagesVM { get; set; }
        //====================================
        public ICollection<Qualifications>? qualifications { get; set; }
        public string? CurrentJop { get; internal set; }
        public double? Salary { get; internal set; }

        //=========================================
        public void FingerCapture()
        {
            CZKEM objCZKEM = new CZKEM();
            var localFingerPrintImage = FingerPrintImage;

            bool fingerprintData = objCZKEM.CaptureImage(true, 500, 500, ref localFingerPrintImage, ImageFile);
        }

    }
}
