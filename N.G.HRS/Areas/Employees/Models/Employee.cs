using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
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
        public DateOnly RehireDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Stopping Work")]
        public DateOnly DateOfStoppingWork { get; set; }
        public bool UsedFingerprint { get; set; }
        public bool SubjectToInsurance { get; set; }//خاضع للتامين 
        [DataType(DataType.Date)]
        [Display(Name = "Date Insurance ")]
        public DateOnly DateInsurance { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }
        //يرتبط مع جدول(الادارة) وجدول (القسم) وجدول (الوصف الوظيفي) وجدول (جهاز البصمة) و علاقة(self)
        //========================================================
        public List<Departments> departmentsList { get; set; }
        public List<Sections> sectionsList { get; set; }
        public List<JobDescription> jobDescriptionsList {  get; set; }
        public List<PracticalExperiences> practicalExperiencesList {  get; set; }
        public List<StatementOfEmployeeFiles> statementOfEmployeeFilesList { get; set; }
        public List<TrainingCourses> trainingCoursesList {  get; set; }
        public List<EmployeeArchives> employeeArchivesList { get; set; }
        public List<FingerprintDevices> fingerprintDevicesList { get; set; }
        //========================================================
        public PersonalData personalData { get; set; }
        public FinancialStatements financialStatements { get; set; }
        //===============================================
        //          |
        //علاقة self v
        public int? ManagerId { get; set; }
        // Navigation property for Manager
        public Employee? Manager { get; set; }
        // Navigation property for Subordinates 
        public List<Employee>? Subordinates { get; set; }
        //=================================================
        [ForeignKey("EmployeeAccountId")]
        public int EmployeeAccountId { get; set; }
        public EmployeeAccount employeeAccount { get; set; }
        //====================================




    }
}
