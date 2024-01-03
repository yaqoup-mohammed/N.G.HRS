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
            //علاقة الموظف بأجهزة البصمة
            modelBuilder.Entity<FingerprintDevices>()
                .HasOne(p => p.employee)
                .WithMany(p => p.fingerprintDevicesList)
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
            //التخصصات مع المؤهل
            modelBuilder.Entity<Specialties>()
                 .HasOne(p => p.qualification)
                 .WithMany(p => p.specialtiesList)
                 .HasForeignKey(p => p.QualificationId);
            //الجامعات مع المؤهل
            modelBuilder.Entity<Universities>()
                 .HasOne(p => p.qualifications)
                 .WithMany(p => p.universitiesList)
                 .HasForeignKey(p => p.QualificationsId);
            //البيانات المالية مع العملة
            modelBuilder.Entity<Currency>()
                 .HasOne(p => p.financialStatements)
                 .WithMany(p => p.currenciesList)
                 .HasForeignKey(p => p.FinancialStatementsId);
            //الملفات الوظيفية مع ملفات الموظف
            modelBuilder.Entity<FunctionalFiles>()
                 .HasOne(p => p.statementOfEmployeeFiles)
                 .WithMany(p => p.FunctionalFilesList)
                 .HasForeignKey(p => p.StatementOfEmployeeFilesId);
            //=============================================================
            //الاسرة مع نوع القرابة
            modelBuilder.Entity<RelativesType>()
                .HasOne(p => p.family)
                .WithMany(p => p.relativesTypesList)
                .HasForeignKey(p => p.FamilyId);
            //=============================================================
            //اعضاء مجلس الادارة مع مجلس الادارة
            modelBuilder.Entity<MembershipOfTheBoardOfDirectors>()
                .HasOne(p => p.boardOfDirectors)
                .WithMany(p => p.membershipOfTheBoardOfDirectorsList)
                .HasForeignKey(p => p.BoardOfDirectorsId);
            //================================================================
            //الشركة مع مجلس الادارة
            modelBuilder.Entity<BoardOfDirectors>()
                .HasOne(p => p.Company)
                .WithMany(p => p.BoardOfDirectorsList)
                .HasForeignKey(p => p.CompanyId);
            //===========================================================
            //الشركة مع الفروع
            modelBuilder.Entity<Branches>()
                .HasOne(p => p.company)
                .WithMany(p => p.branchesList)
                .HasForeignKey(p => p.CompanyId);
            //============================================================
            //الفروع مع بالدول
            modelBuilder.Entity<Country>()
                .HasOne(p => p.branches)
                .WithMany(p => p.CountryList)
                .HasForeignKey(p => p.BranchesId)
                .OnDelete(DeleteBehavior.NoAction);

            //الفروع مع المحافظات
            modelBuilder.Entity<Governorate>()
                .HasOne(p => p.branches)
                .WithMany(p => p.GovernoratesList)
                .HasForeignKey(p => p.BranchesId)
                .OnDelete(DeleteBehavior.NoAction);
            //الفروع مع المديريات
            modelBuilder.Entity<Directorate>()
                .HasOne(p => p.branches)
                .WithMany(p => p.DirectoratesList)
                .HasForeignKey(p => p.BranchesId)
                .OnDelete(DeleteBehavior.NoAction);

            //==============================================================
            //القطاعات مع الفروع
            modelBuilder.Entity<Branches>()
                .HasOne(p => p.sectors)
                .WithMany(p => p.BranchesList)
                .HasForeignKey(p => p.SectorsId)
                .OnDelete(DeleteBehavior.NoAction);
            //===================================================================
            //الادارات مع الفروع
            modelBuilder.Entity<Sectors>()
                .HasOne(p => p.departments)
                .WithMany(p => p.SectorsList)
                .HasForeignKey(p => p.DepartmentsId)
                .OnDelete(DeleteBehavior.NoAction);
            //===================================================================
            //الاقسام  مع الادارات
            modelBuilder.Entity<Departments>()
                .HasOne(p => p.sections)
                .WithMany(p => p.departmentsList)
                .HasForeignKey(p => p.SectionsId)
                .OnDelete(DeleteBehavior.NoAction);
            //====================================================================
            //الدرجة الوظيفية مع العملة
            modelBuilder.Entity <Currency>()
                .HasOne(p => p.functionalClass)
                .WithMany(p => p.CurrencyList)
                .HasForeignKey(p => p.FunctionalClassId);
            //====================================================
            //الوصف الوظيفي مع الفئات الوظيفية
            modelBuilder.Entity<FunctionalCategories>()
                .HasOne(p => p.jobDescription)
                .WithMany(p => p.FunctionalCategoriesList)
                .HasForeignKey(p => p.JobDescriptionId);
            //الوصف الوظيفي مع الدرجة الوظيفية

            modelBuilder.Entity<FunctionalClass>()
                .HasOne(p => p.jobDescription)
                .WithMany(p => p.functionalClassesList)
                .HasForeignKey(p => p.JobDescriptionId)
                .OnDelete(DeleteBehavior.NoAction);

            //الوصف الوظيفي مع الرتب الوظيفية

            modelBuilder.Entity<JobRanks>()
               .HasOne(p => p.jobDescription)
               .WithMany(p => p.JobRanksList)
               .HasForeignKey(p => p.JobDescriptionId);
            //==============================================
            //حسابات الاقسام مع نوع الحساب

            modelBuilder.Entity<FinanceAccountType>()
               .HasOne(p => p.departmentAccounts)
               .WithMany(p => p.FinanceAccountsTypeList)
               .HasForeignKey(p => p.DepartmentAccountsId);
            //حسابات الاقسام مع الحساب
            modelBuilder.Entity<FinanceAccount>()
               .HasOne(p => p.departmentAccounts)
               .WithMany(p => p.FinanceAccountsList)
               .HasForeignKey(p => p.DepartmentAccountsId);

            //حسابات الاقسام مع الاقسام
            modelBuilder.Entity<Sections>()
               .HasOne(p => p.departmentAccounts)
               .WithMany(p => p.SectionsList)
               .HasForeignKey(p => p.DepartmentAccountsId);

            //===================================
            //حسابا الموظف مع الموظف
            modelBuilder.Entity<Employee>()
               .HasOne(p => p.employeeAccount)
               .WithMany(p => p.employeesList)
               .HasForeignKey(p => p.EmployeeAccountId);

            //حسابات الموظف مع نوع الحساب

            modelBuilder.Entity<FinanceAccountType>()
              .HasOne(p => p.employeeAccount)
              .WithMany(p => p.financeAccountTypesList)
              .HasForeignKey(p => p.EmployeeAccountId);
            // حسابات الموظف مع الحساب
            modelBuilder.Entity<FinanceAccount>()
              .HasOne(p => p.employeeAccount)
              .WithMany(p => p.financeAccountsList)
              .HasForeignKey(p => p.EmployeeAccountId);
            //==============================================
            //دوام الموظفين مع الموظف
            modelBuilder.Entity<Employee>()
              .HasOne(p => p.staffTime)
              .WithMany(p => p.EmployeesList)
              .HasForeignKey(p => p.StaffTimeId);
            //==============================================
            //دوام الموظفين مع نماذج الدوام
            modelBuilder.Entity<PermanenceModels>()
              .HasOne(p => p.staffTime)
              .WithMany(p => p.PermanenceModelsList)
              .HasForeignKey(p => p.StaffTimeId);
            //============================================
            //ربط الموظفين بفترات المناوبة مع الادارات
            modelBuilder.Entity<Departments>()
              .HasOne(p => p.linkingEmployeesToShiftPeriods)
              .WithMany(p => p.DepartmentsList)
              .HasForeignKey(p => p.LinkingEmployeesToShiftPeriodsId)
              .OnDelete(DeleteBehavior.NoAction);
            //ربط الموظف بفترات المناوبة مع الاقسام
            modelBuilder.Entity<Sections>()
              .HasOne(p => p.linkingEmployeesToShiftPeriods)
              .WithMany(p => p.SectionsList)
              .HasForeignKey(p => p.LinkingEmployeesToShiftPeriodsId)
              .OnDelete(DeleteBehavior.NoAction);
            //ربط الموظف بفترات المناوبة مع الموظفين
            modelBuilder.Entity<Employee>()
              .HasOne(p => p.linkingEmployeesToShiftPeriods)
              .WithMany(p => p.EmployeeList)
              .HasForeignKey(p => p.LinkingEmployeesToShiftPeriodsId)
              .OnDelete(DeleteBehavior.NoAction);

            //ربط الموظف بفترات المناوبة مع الدوام
            modelBuilder.Entity<PermanenceModels>()
              .HasOne(p => p.linkingEmployeesToShiftPeriods)
              .WithMany(p => p.PermanencesList)
              .HasForeignKey(p => p.LinkingEmployeesToShiftPeriodsId)
              .OnDelete(DeleteBehavior.NoAction);

            //ربط الموظف بفترات المناوبة مع الفترات
            modelBuilder.Entity<Periods>()
              .HasOne(p => p.linkingEmployeesToShiftPeriods)
              .WithMany(p => p.PeriodsList)
              .HasForeignKey(p => p.LinkingEmployeesToShiftPeriodsId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================================
            //البصمة الواحدة مع الموظف
            modelBuilder.Entity<Employee>()
              .HasOne(p => p.OneFingerprint)
              .WithMany(p => p.employeesList)
              .HasForeignKey(p => p.OneFingerprintId);
            //========================================================
            //الاجازات الاسبوعية مع الدوام
            modelBuilder.Entity<PermanenceModels>()
              .HasOne(p => p.weekends)
              .WithMany(p => p.PermanenceModelsList)
              .HasForeignKey(p => p.WeekendsId)
              .OnDelete(DeleteBehavior.NoAction);

            //الاجازات الاسبوعية مع الفترات
            modelBuilder.Entity<Periods>()
              .HasOne(p => p.weekends)
              .WithMany(p => p.PeriodsList)
              .HasForeignKey(p => p.WeekendsId)
              .OnDelete(DeleteBehavior.NoAction);
            //========================================================
            //الاجازات الاسبوعية للدوام المرن مع الدوام
            //modelBuilder.Entity<PermanenceModels>()
            //  .HasOne(p => p.weekendsForFlexibleWorking)          --->{{تحت النظر}}<---
            //  .WithMany(p => p.PermanenceModelsList)
            //  .HasForeignKey(p => p.WeekendsForFlexibleWorkingId);
            //=========================================================
            //الارصدة الافتتاحية للاجازات مع الموظف
            modelBuilder.Entity<Employee>()
              .HasOne(p => p.openingBalancesForVacations)
              .WithMany(p => p.EmployeeList)
              .HasForeignKey(p => p.OpeningBalancesForVacationsId)
              .OnDelete(DeleteBehavior.NoAction);
            //الارصدة الافتتاحية للاجازات مع الاجازات العامة
            modelBuilder.Entity<PublicHolidays>()
              .HasOne(p => p.openingBalancesForVacations)
              .WithMany(p => p.publicHolidaysList)
              .HasForeignKey(p => p.OpeningBalancesForVacationsId)
              .OnDelete(DeleteBehavior.NoAction);
            //===========================================================
            //نماذج العقوبات والمخالفات مع المخالفات
            modelBuilder.Entity<Violations>()
              .HasOne(p => p.PenaltiesAndViolationsForms)
              .WithMany(p => p.ViolationsList)
              .HasForeignKey(p => p.PenaltiesAndViolationsFormsId)
              .OnDelete(DeleteBehavior.NoAction);
            //نماذج العقوبات والمخالفات مع العقوبات
            modelBuilder.Entity<Penalties>()
              .HasOne(p => p.PenaltiesAndViolationsForms)
              .WithMany(p => p.PenaltiesList)
              .HasForeignKey(p => p.PenaltiesAndViolationsFormsId)
              .OnDelete(DeleteBehavior.NoAction);





        }

        //تهيئة الرواتب والاجور
        public DbSet<AdditionalAccountInformation> additionalAccountInformation { get; set; }
        public DbSet<AllowancesAndDiscounts> allowancesAndDiscounts { get; set; }
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
        public DbSet<PenaltiesAndViolationsForms> penaltiesAndViolationsForms { get; set; }





    }
}
