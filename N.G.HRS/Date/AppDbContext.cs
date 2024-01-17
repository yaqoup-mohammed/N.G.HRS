using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Areas.PenaltiesAndViolations.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using System.Configuration;
using System.Threading.Tasks.Dataflow;
using N.G.HRS.Areas.AalariesAndWages.Models;

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
                .WithMany(p => p.DirectoratesList)
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
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.Departments)
                .WithMany(p => p.EmployeesList)
                .HasForeignKey(p => p.DepartmentsId);
            //علاقة الموظفين مع الاقسام
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.Sections)
                .WithMany(p => p.EmployeesList)
                .HasForeignKey(p => p.SectionsId);
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
            //علاقة الموظف بالوظيفة "الوصف الوظيفي"
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.JobDescription)
                .WithMany(p => p.employeesList)
                .HasForeignKey(p => p.JobDescriptionId);

            //علاقة الموظف بالبيانات المالية
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.financialStatements)
                .WithOne(p => p.employee)
                .HasForeignKey<FinancialStatements>(b => b.Id);
            //==================================================
            //علاقة الموظف بالخبرات العملة
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.PracticalExperiences)
                .WithMany(p => p.EmployeesList)
                .HasForeignKey(p => p.PracticalExperiencesId);
            //==================================================
            //علاقة الموظف بملفات الموظف
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.StatementOfEmployeeFiles)
                .WithMany(p => p.EmployeesList)
                .HasForeignKey(p => p.StatementOfEmployeeFilesId);
            //==================================================
            //علاقة الموظف بالدورات التدريبية
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.TrainingCourses)
                .WithMany(p => p.EmployeesList)
                .HasForeignKey(p => p.TrainingCoursesId);
            //علاقة الموظف بأجهزة البصمة
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.FingerprintDevices)
                .WithMany(p => p.EmployeesList)
                .HasForeignKey(p => p.FingerprintDevicesId);
            //==================================================
            //--البانات الشخصية--
            //البيانات الشخصية مع الجنس
            modelBuilder.Entity<PersonalData>()
                .HasOne(p => p.Sex)
                .WithMany(p => p.PersonalDataList)
                .HasForeignKey(p => p.SexId);
            //البيانات الشخصية مع الجنسيات
            modelBuilder.Entity<PersonalData>()
                .HasOne(p => p.Nationality)
                .WithMany(p => p.personalDatasList)
                .HasForeignKey(p => p.NationalityId);
            //البيانات الشخصية مع الديانة
            modelBuilder.Entity<PersonalData>()
                .HasOne(p => p.Religion)
                .WithMany(p => p.PersonalDataList)
                .HasForeignKey(p => p.ReligionId);
            //البيانات الشخصية مع الضمين
            modelBuilder.Entity<Guarantees>()
               .HasOne(p => p.personalData)
               .WithOne(p => p.guarantees)
               .HasForeignKey<PersonalData>(b => b.Id);
            //البيانات الشخصية مع الحالة الاجتماعية
            modelBuilder.Entity<PersonalData>()
                .HasOne(p => p.MaritalStatus)
                .WithMany(p => p.PersonalDataList)
                .HasForeignKey(p => p.MaritalStatusId)
                .OnDelete(DeleteBehavior.NoAction);
            //الضمين مع الحالة الاجتماعية
            modelBuilder.Entity<Guarantees>()
                .HasOne(p => p.MaritalStatus)
                .WithMany(p => p.GuaranteesList)
                .HasForeignKey(p => p.MaritalStatusId);
            //العلاقة بين الموظف والارشيف
            modelBuilder.Entity<Employee>()
                    .HasOne(p => p.employeeArchives)
                    .WithOne(p => p.employee)
                    .HasForeignKey<EmployeeArchives>(b => b.Id);
            //المؤهل مع المؤهل التعليمي
            modelBuilder.Entity<Qualifications>()
                .HasMany(j => j.EducationalQualification)
                .WithMany(j => j.qualifications)
                .UsingEntity(j => j.ToTable("EducationalQualificationAndQualification"));
            ////========================================================
            //التخصصات مع المؤهل
            modelBuilder.Entity<Qualifications>()
                .HasMany(j => j.Specialties)
                .WithMany(j => j.qualifications)
                .UsingEntity(j => j.ToTable("SpecialtiesAndQualification"));

            //الجامعات مع المؤهل
            modelBuilder.Entity<Qualifications>()
                .HasMany(j => j.universities)
                .WithMany(j => j.qualifications)
                .UsingEntity(j => j.ToTable("UniversitiesAndQualification"));

            //البيانات المالية مع العملة
            modelBuilder.Entity<FunctionalCategories>()
                 .HasOne(p => p.Currency)
                 .WithMany(p => p.FunctionalCategoriesList)
                 .HasForeignKey(p => p.CurrencyId);

            //الملفات الوظيفية مع ملفات الموظف
            modelBuilder.Entity<StatementOfEmployeeFiles>()
                .HasMany(j => j.FunctionalFiles)
                .WithMany(j => j.StatementOfEmployeeFiles)
                .UsingEntity(j => j.ToTable("FunctionalFilesOfStatementOfEmployeeFiles"));
            //=============================================================
            //الاسرة مع نوع القرابة
            modelBuilder.Entity<Family>()
                .HasOne(p => p.RelativesType)
                .WithMany(p => p.FamiliesList)
                .HasForeignKey(p => p.RelativesTypeId);
            //=============================================================
            //اعضاء مجلس الادارة مع مجلس الادارة
            modelBuilder.Entity<BoardOfDirectors>()
                .HasOne(p => p.MembershipOfTheBoardOfDirectors)
                .WithMany(p => p.BoardOfDirectorsList)
                .HasForeignKey(p => p.MembershipOfTheBoardOfDirectorsId);
            //================================================================
            //الشركة مع مجلس الادارة
            modelBuilder.Entity<Company>()
                .HasOne(p => p.BoardOfDirectors)
                .WithMany(p => p.CompanyList)
                .HasForeignKey(p => p.BoardOfDirectorsId);
            //===========================================================
            //الشركة مع الفروع
            modelBuilder.Entity<Branches>()
                .HasOne(p => p.Company)
                .WithMany(p => p.BranchesList)
                .HasForeignKey(p => p.CompanyId);
            //============================================================
            //الفروع مع بالدول
            modelBuilder.Entity<Branches>()
                .HasOne(p => p.Country)
                .WithMany(p => p.BranchesList)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            //الفروع مع المحافظات
            modelBuilder.Entity<Branches>()
                .HasOne(p => p.Governorate)
                .WithMany(p => p.BranchesList)
                .HasForeignKey(p => p.GovernorateId)
                .OnDelete(DeleteBehavior.NoAction);
            //الفروع مع المديريات
            modelBuilder.Entity<Branches>()
                .HasOne(p => p.Directorate)
                .WithMany(p => p.BranchesList)
                .HasForeignKey(p => p.DirectorateId)
                .OnDelete(DeleteBehavior.NoAction);

            //==============================================================
            //القطاعات مع الفروع
            modelBuilder.Entity<Sectors>()
                .HasOne(p => p.Branches)
                .WithMany(p => p.SectorsList)
                .HasForeignKey(p => p.BranchesId)
                .OnDelete(DeleteBehavior.NoAction);
            //===================================================================
            //الادارات مع الفروع
            modelBuilder.Entity<Departments>()
                .HasOne(p => p.Sectors)
                .WithMany(p => p.DepartmentsList)
                .HasForeignKey(p => p.SectorsId)
                .OnDelete(DeleteBehavior.NoAction);
            //===================================================================
            //الاقسام  مع الادارات
            modelBuilder.Entity<Sections>()
                .HasOne(p => p.Departments)
                .WithMany(p => p.SectionsList)
                .HasForeignKey(p => p.DepartmentsId)
                .OnDelete(DeleteBehavior.NoAction);
            //====================================================================
            //الدرجة الوظيفية مع العملة
            modelBuilder.Entity <FunctionalClass>()
                .HasOne(p => p.Currency)
                .WithMany(p => p.FunctionalClassList)
                .HasForeignKey(p => p.CurrencyId);
            //====================================================
            //الوصف الوظيفي مع الفئات الوظيفية
            modelBuilder.Entity<JobDescription>()
                .HasOne(p => p.FunctionalCategories)
                .WithMany(p => p.JobDescriptionsList)
                .HasForeignKey(p => p.FunctionalCategoriesId);

            //الوصف الوظيفي مع الدرجة الوظيفية
            modelBuilder.Entity<JobDescription>()
                .HasOne(p => p.FunctionalClass)
                .WithMany(p => p.JobDescriptionsList)
                .HasForeignKey(p => p.FunctionalClassId)
                .OnDelete(DeleteBehavior.NoAction);

            //الوصف الوظيفي مع الرتب الوظيفية

            modelBuilder.Entity<JobDescription>()
               .HasOne(p => p.JobRanks)
               .WithMany(p => p.JobDescriptionList)
               .HasForeignKey(p => p.JobRanksId);
            //==============================================
            //حسابات الاقسام مع نوع الحساب
            modelBuilder.Entity<SectionsAccounts>()
               .HasOne(p => p.FinanceAccountType)
               .WithMany(p => p.SectionsAccountsList)
               .HasForeignKey(p => p.FinanceAccountTypeId)
              .OnDelete(DeleteBehavior.NoAction);

            //حسابات الاقسام مع الحساب
            modelBuilder.Entity<SectionsAccounts>()
               .HasOne(p => p.FinanceAccount)
               .WithMany(p => p.SectionsAccountsList)
               .HasForeignKey(p => p.FinanceAccountId);

            //حسابات الاقسام مع الاقسام
            modelBuilder.Entity<SectionsAccounts>()
               .HasOne(p => p.Sections)
               .WithMany(p => p.SectionsAccountsList)
               .HasForeignKey(p => p.SectionsId);

            //===================================
            //حسابا الموظف مع الموظف
            modelBuilder.Entity<EmployeeAccount>()
               .HasOne(p => p.employee)
               .WithMany(p => p.EmployeeAccountList)
               .HasForeignKey(p => p.EmployeeId);

            //حسابات الموظف مع نوع الحساب
            modelBuilder.Entity<EmployeeAccount>()
              .HasOne(p => p.FinanceAccountType)
              .WithMany(p => p.EmployeeAccountsList)
              .HasForeignKey(p => p.FinanceAccountTypeId);

            // حسابات الموظف مع الحساب
            modelBuilder.Entity<EmployeeAccount>()
              .HasOne(p => p.FinanceAccount)
              .WithMany(p => p.EmployeeAccountsList)
              .HasForeignKey(p => p.FinanceAccountId);
            //==============================================
            //   |هنا
            //   v
            //دوام الموظفين مع الموظف
            modelBuilder.Entity<StaffTime>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.StaffTimeList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //==============================================
            //دوام الموظفين مع نماذج الدوام
            modelBuilder.Entity<StaffTime>()
              .HasOne(p => p.PermanenceModels)
              .WithMany(p => p.StaffTimesList)
              .HasForeignKey(p => p.PermanenceModelsId);
            //=
            //دوام الموظف مع الاقسام
            modelBuilder.Entity<StaffTime>()
              .HasOne(p => p.Sections)
              .WithMany(p => p.staffTimeList)
              .HasForeignKey(p => p.SectionsId);
            //============================================
            //ربط الموظفين بفترات المناوبة مع الادارات
            modelBuilder.Entity<LinkingEmployeesToShiftPeriods>()
              .HasOne(p => p.Departments)
              .WithMany(p => p.LinkingEmployeesToShiftPeriodsList)
              .HasForeignKey(p => p.DepartmentsId)
              .OnDelete(DeleteBehavior.NoAction);
            //ربط الموظف بفترات المناوبة مع الاقسام
            modelBuilder.Entity<LinkingEmployeesToShiftPeriods>()
              .HasOne(p => p.Sections)
              .WithMany(p => p.LinkingEmployeesToShiftPeriodsList)
              .HasForeignKey(p => p.SectionsId)
              .OnDelete(DeleteBehavior.NoAction);
            //ربط الموظف بفترات المناوبة مع الموظفين
            modelBuilder.Entity<LinkingEmployeesToShiftPeriods>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.LinkingEmployeesToShiftPeriodsList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);

            //ربط الموظف بفترات المناوبة مع الدوام
            modelBuilder.Entity<LinkingEmployeesToShiftPeriods>()
              .HasOne(p => p.PermanenceModels)
              .WithMany(p => p.LinkingEmployeesToShiftPeriodsList)
              .HasForeignKey(p => p.PermanenceModelsId)
              .OnDelete(DeleteBehavior.NoAction);

            //ربط الموظف بفترات المناوبة مع الفترات
            modelBuilder.Entity<LinkingEmployeesToShiftPeriods>()
              .HasOne(p => p.Periods)
              .WithMany(p => p.LinkingEmployeesToShiftPeriodsList)
              .HasForeignKey(p => p.PeriodsId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================================
            //البصمة الواحدة مع الموظف
            modelBuilder.Entity<OneFingerprint>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.OneFingerprintList)
              .HasForeignKey(p => p.EmployeeId);
            //========================================================
            //الاجازات الاسبوعية مع الدوام
            modelBuilder.Entity<Weekends>()
              .HasOne(p => p.PermanenceModels)
              .WithMany(p => p.WeekendsList)
              .HasForeignKey(p => p.PermanenceModelsId)
              .OnDelete(DeleteBehavior.NoAction);

            //الاجازات الاسبوعية مع الفترات
            modelBuilder.Entity<Weekends>()
              .HasOne(p => p.Periods)
              .WithMany(p => p.WeekendsList)
              .HasForeignKey(p => p.PeriodsId)
              .OnDelete(DeleteBehavior.NoAction);
            //========================================================
            //الاجازات الاسبوعية للدوام المرن مع الدوام
            //modelBuilder.Entity<WeekendsForFlexibleWorking>()
            //  .HasOne(p => p.PermanenceModels)
            //  .WithMany(p => p.WeekendsForFlexibleWorkingList)
            //  .HasForeignKey(p => p.PermanenceModelsId)
            //  .OnDelete(DeleteBehavior.NoAction);

            //=========================================================
            //الارصدة الافتتاحية للاجازات مع الموظف
            modelBuilder.Entity<OpeningBalancesForVacations>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.OpeningBalancesForVacationsList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //الارصدة الافتتاحية للاجازات مع الاجازات العامة
            modelBuilder.Entity<OpeningBalancesForVacations>()
              .HasOne(p => p.PublicHolidays)
              .WithMany(p => p.OpeningBalancesForVacationsList)
              .HasForeignKey(p => p.PublicHolidaysId)
              .OnDelete(DeleteBehavior.NoAction);
            //===========================================================
            //نماذج العقوبات والمخالفات مع المخالفات
            modelBuilder.Entity<PenaltiesAndViolationsForms>()
              .HasOne(p => p.Violations)
              .WithMany(p => p.PenaltiesAndViolationsFormsList)
              .HasForeignKey(p => p.ViolationsId)
              .OnDelete(DeleteBehavior.NoAction);
            //نماذج العقوبات والمخالفات مع العقوبات
            modelBuilder.Entity<PenaltiesAndViolationsForms>()
              .HasOne(p => p.Penalties)
              .WithMany(p => p.PenaltiesAndViolationsFormsList)
              .HasForeignKey(p => p.PenaltiesId)
              .OnDelete(DeleteBehavior.NoAction);
            //======================================================
            modelBuilder.Entity<Family>()
                .HasOne(p => p.Employees)
                .WithOne(p => p.Families)
                .HasForeignKey<Employee>(b => b.Id);
            //==============================================
            modelBuilder.Entity<Employee>()
              .HasMany(j => j.qualifications)
              .WithMany(j => j.employees)
              .UsingEntity(j => j.ToTable("EmployeesQualifications"));




        }

        //تهيئة الرواتب والاجور
        public DbSet<AdditionalAccountInformation> additionalAccountInformation { get; set; }
        public DbSet<AllowancesAndDiscounts> allowancesAndDiscounts { get; set; }
        public DbSet<BasicDataForTheSalaryStatement> basicDataForTheSalaryStatements { get; set; }
        public DbSet<BasicDataForWagesAndSalaries> basicDataForWagesAndSalaries { get; set; }
        public DbSet<SectionsAccounts> SectionsAccounts { get; set; }
        //تهيئة الموظفين
        public DbSet<Employee> employee { get; set; }
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
        public DbSet<FinanceAccountType> FinanceAccountType { get; set; }
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
        public DbSet<PenaltiesAndViolationsForms> penaltiesAndViolationsForms { get; set; }
        public DbSet<N.G.HRS.Areas.OrganizationalChart.Models.Departments> Departments { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.Employees.Models.Family> Family { get; set; } = default!;





    }
}
