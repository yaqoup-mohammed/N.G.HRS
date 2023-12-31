using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using System.Configuration;

namespace N.G.HRS.Date
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //علاقات التهيئة العامة
            //علاقة الدولة مع المحافظات
            modelBuilder.Entity<Governorate>()
                .HasOne(p => p.CountryOne)
                .WithMany(p => p.governoratesList)
                .HasForeignKey(p => p.CountryId);
            //=============================================================
            //علاقة المحافظات مع المديريات
            modelBuilder.Entity<Directorate>()
                .HasOne(p => p.Governorate)
                .WithMany(p => p.directoratesList)
                .HasForeignKey(p => p.GovernorateId);
            //=============================================================
            //علاقة العقود مع شروط العقود
            modelBuilder.Entity<ContractTerms>()
                .HasOne(p => p.Contracts)
                .WithMany(p => p.contractTermsList)
                .HasForeignKey(p => p.ContractsId);
            //==============================================================
          //علاقات الموظفين
            // علاقات الموظفين مع الادارات
            modelBuilder.Entity<Departments>()
                .HasOne(p => p.Employee)
                .WithMany(p => p.departmentsList)
                .HasForeignKey(p => p.EmployeeId);
            //علاقة الموظفين مع الاقسام
            modelBuilder.Entity<Sections>()
                .HasOne(p => p.Employee)
                .WithMany(p => p.sectionsList)
                .HasForeignKey(p => p.EmployeeId);
            //علاقات الموظفين مع البيانات الشخصية
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.personalData)
                .WithOne(p => p.employee)
                .HasForeignKey<PersonalData>(b => b.Id);
            //========================================
            //علاقة self مع جدول الموظفين
            modelBuilder.Entity<Employee>()
           .HasOne(e => e.Manager)              // Specifies that each employee has one manager.
           .WithMany(e => e.Subordinates)       // Specifies that each manager can have many Subordinates
           .HasForeignKey(e => e.ManagerId)     // Specifies the foreign key property for this relationship.
           .OnDelete(DeleteBehavior.Restrict);
            //==========================================================================
            //علاقة الموظف بالوظيفة "الوصف الوظيفي" س
            modelBuilder.Entity<JobDescription>()
                .HasOne(p => p.Employee)
                .WithMany(p => p.jobDescriptionsList)
                .HasForeignKey(p => p.EmployeeId);
            //علاقة الموظف مع المؤهل

                                                             //<-------غير مكتمل
            //علاقة الموظف بالبيانات المالية
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.financialStatements)
                .WithOne(p => p.employee)
                .HasForeignKey<FinancialStatements>(b => b.Id);
            //==================================================
            //علاقة الموظف بالخبرات العملة
            modelBuilder.Entity<PracticalExperiences>()
                .HasOne(p => p.Employee)
                .WithMany(p => p.practicalExperiencesList)
                .HasForeignKey(p => p.EmployeeId);
            //==================================================
            //علاقة الموظف بملفات الموظف
            modelBuilder.Entity<StatementOfEmployeeFiles>()
                .HasOne(p => p.Employee)
                .WithMany(p => p.statementOfEmployeeFilesList)
                .HasForeignKey(p => p.EmployeeId);
            //==================================================
            //علاقة الموظف بالدورات التدريبية
            modelBuilder.Entity<TrainingCourses>()
                .HasOne(p => p.Employee)
                .WithMany(p => p.trainingCoursesList)
                .HasForeignKey(p => p.EmployeeId);
            //==================================================
          //--البانات الشخصية--
            //البيانات الشخصية مع الجنس
            modelBuilder.Entity<Sex>()
                .HasOne(p => p.personalData)
                .WithMany(p => p.sexList)
                .HasForeignKey(p => p.PersonalDataId);
            //البيانات الشخصية مع الجنسيات
            modelBuilder.Entity<Nationality>()
                .HasOne(p => p.personalData)
                .WithMany(p => p.nationalitiesList)
                .HasForeignKey(p => p.PersonalDataId);
            //البيانات الشخصية مع الديانة
            modelBuilder.Entity<Religion>()
                .HasOne(p => p.personalData)
                .WithMany(p => p.religionsList)
                .HasForeignKey(p => p.PersonalDataId);
            //البيانات الشخصية مع الضمين
            modelBuilder.Entity<Guarantees>()
               .HasOne(p => p.personalData)
               .WithOne(p => p.guarantees)
               .HasForeignKey<PersonalData>(b => b.Id);
            //البيانات الشخصية مع الحالة الاجتماعية
            modelBuilder.Entity<MaritalStatus>()
                .HasOne(p => p.PersonalData)
                .WithMany(p => p.maritalStatusList)
                .HasForeignKey(p => p.PersonalDataId)
                .OnDelete(DeleteBehavior.NoAction);
            //الضمين مع الحالة الاجتماعية
            modelBuilder.Entity<MaritalStatus>()
                .HasOne(p => p.guarantees)
                .WithMany(p => p.maritalStatusList)
                .HasForeignKey(p => p.GuaranteesId);
            //العلاقة بين الموظف والارشيف
            modelBuilder.Entity<EmployeeArchives>()
                .HasOne(p => p.employee)
                .WithMany(p => p.employeeArchivesList)
                .HasForeignKey(p => p.EmployeeId);
            //المؤهل مع المؤهل التعليمي
            modelBuilder.Entity<EducationalQualification>()
                .HasOne(p => p.qualification)
                .WithMany(p => p.educationalQualificationsList)
                .HasForeignKey(p => p.QualificationId);
            ////========================================================
            modelBuilder.Entity<Specialties>()
                 .HasOne(p => p.qualification)
                 .WithMany(p => p.specialtiesList)
                 .HasForeignKey(p => p.QualificationId);


        }

        //تهيئة الرواتب والاجور
        public DbSet<AdditionalAccountInformation> additionalAccountInformation { get; set; }
        public DbSet<AllowancesAndDiscounts> allowancesAndDiscounts { get; set; }
        public DbSet<Archives> archives { get; set; }
        public DbSet<BasicDataForTheSalaryStatement> basicDataForTheSalaryStatements { get; set; }
        public DbSet<BasicDataForWagesAndSalaries> basicDataForWagesAndSalaries { get; set; }
        public DbSet<DepartmentAccounts>  departmentAccounts { get; set; }
        //تهيئة الموظفين
        public DbSet<Employee> employee { get; set; }
        public DbSet<EmployeeAccounts> employeeAccounts { get; set; }
        public DbSet<FinancialStatements> financialStatements { get; set; }
        public DbSet<Guarantees>  guarantees { get; set; }
        public DbSet<PersonalData> personalDatas { get; set; }
        public DbSet<PracticalExperiences> practicalExperiences { get; set; }
        public DbSet<Qualifications> qualifications { get; set; }
        public DbSet<StatementOfEmployeeFiles>  statementOfEmployeeFiles { get; set; }
        public DbSet<TrainingCourses>  trainingCourses { get; set; }
        //التهيئة العامة
        public DbSet<Contracts> contracts { get; set; }
        public DbSet<ContractTerms>  contractTerms { get; set; }
        public DbSet<Country> country { get; set; }
        public DbSet<Directorate>  directorates { get; set; }
        public DbSet<EducationalQualification> educationalQualifications { get; set; }
        public DbSet<FingerprintDevices>  fingerprintDevices { get; set; }
        public DbSet<FunctionalFiles>  functionalFiles { get; set; }
        public DbSet<Governorate>  governorates { get; set; }
        public DbSet<MaritalStatus>  maritalStatuses { get; set; }
        public DbSet<Nationality> nationality { get; set; }
        public DbSet<OfficialVacations>  officialVacations { get; set; }
        public DbSet<Permissions> permissions { get; set; }
        public DbSet<PublicHolidays>  publicHolidays { get; set; }
        public DbSet<RelativesType>  relativesTypes { get; set; }
        public DbSet<Religion> religion { get; set; }
        public DbSet<Sex> sex { get; set; }
        public DbSet<Specialties> specialties { get; set; }
        //الهيكل التنظيمي
        public DbSet<BoardOfDirectors> boardOfDirectors { get; set; }
        public DbSet<Branches> branches { get; set; }
        public DbSet<Company> company { get; set; }
        public DbSet<MembershipOfTheBoardOfDirectors>  membershipOfTheBoardOfs { get; set; }
        public DbSet<PublicAdministration> publicAdministrations { get; set; }
        public DbSet<Sectors> sectors { get; set; }
        //التخطيط والتوصيف الوظيفي
        public DbSet<FunctionalCategories>  functionalCategories { get; set; }
        public DbSet<FunctionalClass>  functionalClasses { get; set; }
        public DbSet<JobDescription> jobDescriptions { get; set; }
        public DbSet<JobRanks> jobRanks { get; set; }
        //تهيئة الحضور والانصراف
        public DbSet<AdjustingTime> adjustingTimes { get; set; }
        public DbSet<LinkingEmployeesToShiftPeriods> linkingEmployeesToShiftPeriods { get; set; }
        public DbSet<Months> months { get; set; }
        public DbSet<OneFingerprint> oneFingerprints { get; set; }
        public DbSet<OpeningBalancesForVacations> openingBalancesForVacations { get; set; }
        public DbSet<Periods> periods { get; set; }
        public DbSet<PermanenceModels> permanenceModels { get; set; }
        public DbSet<PermissionToAttendAndLeave> permissionToAttendAndLeaves { get; set; }
        public DbSet<SetPeriods> setPeriods { get; set; }
        public DbSet<StaffTime> staffTimes { get; set; }
        public DbSet<Weekends> weekends { get; set; }
        public DbSet<WeekendsForFlexibleWorking> weekendsForFlexibleWorkings { get; set; }





    }
}
