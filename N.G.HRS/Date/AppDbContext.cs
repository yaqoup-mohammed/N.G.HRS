using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Areas.PenaltiesAndViolations.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using System.Configuration;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Areas.ViolationsAndPenaltiesAffairs.Models;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.FingerPrintSetting;

namespace N.G.HRS.Date
{
    public class AppDbContext:IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AttendanceStatus>().HasData(
            new AttendanceStatus() { Id = 1, Name = "حضور" },
            new AttendanceStatus() { Id = 2, Name = "غياب" },
            new AttendanceStatus() { Id = 3, Name = "سماحية انصراف مبكر" },
            new AttendanceStatus() { Id = 4, Name = "سماحية حضور متأخر" },
            new AttendanceStatus() { Id = 5, Name = "اذن" },
            new AttendanceStatus() { Id = 6, Name = "اجازة" },
            new AttendanceStatus() { Id = 7, Name = "اجازة رسمية" },
            new AttendanceStatus() { Id = 8, Name = "اجازة اسبوعية" },
            new AttendanceStatus() { Id = 9, Name = "إضافي معتمد" },
            new AttendanceStatus() { Id = 10, Name = "إضافي غير معتمد" },
            new AttendanceStatus() { Id = 11, Name = "انصراف بدون عذر" },
            new AttendanceStatus() { Id = 12, Name = "تأخير" },
            new AttendanceStatus() { Id = 13, Name = "غياب نصف يوم" },
            new AttendanceStatus() { Id = 14, Name = "سماحية حضور وانصراف" },
            new AttendanceStatus() { Id = 15, Name = "تكليف خارجي " }
   // Add more seed data as needed
   );
            modelBuilder.Entity<Assignment>().HasData(
           new Assignment() { Id = 1, Name = "تكليف إضافي" },
                new Assignment() { Id = 2, Name = "تكليف خارجي" }
   // Add more seed data as needed
   );
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
                .WithMany(p => p.EmployeeList)
                .HasForeignKey(p => p.DepartmentsId);
            //علاقة الموظفين مع الاقسام
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.Sections)
                .WithMany(p => p.EmployeeList)
                .HasForeignKey(p => p.SectionsId);
            //علاقات الموظفين مع البيانات الشخصية
            //========================================MO-AL-MO
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.personalData)
                .WithOne(p => p.employee)
                .HasForeignKey<PersonalData>(b => b.EmployeeId);
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
                .WithMany(p => p.EmployeeList)
                .HasForeignKey(p => p.JobDescriptionId);

            //علاقة الموظف بالبيانات المالية
            //================================================== MO-AL-MO
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.financialStatements)
                .WithOne(p => p.employee)
                .HasForeignKey<FinancialStatements>(b => b.EmployeeId);
            //================================================== MO-AL-MO
            //علاقة الموظف بالخبرات العملة
            modelBuilder.Entity<PracticalExperiences>()
                 .HasOne(p => p.Employee)
                 .WithMany(p => p.PracticalExperiencesList)
                 .HasForeignKey(p => p.EmployeeId);
            //================================================== MO-AL-MO
            //علاقة الموظف بملفات الموظف
            modelBuilder.Entity<StatementOfEmployeeFiles>()
                .HasOne(p => p.EmployeeOne)
                .WithMany(p => p.StatementOfEmployeeFilesList)
                .HasForeignKey(p => p.EmployeeId);
            //================================================== MO-AL-MO
            //علاقة الموظف بالدورات التدريبية
            modelBuilder.Entity<TrainingCourses>()
                .HasOne(p => p.EmployeeOne)
                .WithMany(p => p.TrainingCoursesList)
                .HasForeignKey(p => p.EmployeeId);
            //علاقة الموظف بأجهزة البصمة
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.FingerprintDevices)
                .WithMany(p => p.EmployeeList)
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
            //======================================================MO-AL-MO
            modelBuilder.Entity<Guarantees>()
               .HasOne(p => p.personalData)
               .WithOne(p => p.guarantees)
               .HasForeignKey<PersonalData>(b => b.GuaranteesId);
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
            //======================================================
            modelBuilder.Entity<EmployeePermissions>()
                    .HasOne(p => p.Employee)
                    .WithMany(p => p.EmployeePermissionsList)
                    .HasForeignKey(p => p.EmployeeId)
                    .OnDelete(DeleteBehavior.NoAction);
            //======================================================
            modelBuilder.Entity<EmployeePermissions>()
                    .HasOne(p => p.Permission)
                    .WithMany(p => p.EmployeePermissionsList)
                    .HasForeignKey(p => p.PermissionId)
                    .OnDelete(DeleteBehavior.NoAction);
            //======================================================
            modelBuilder.Entity<EmployeePermissions>()
                    .HasOne(p => p.Period)
                    .WithMany(p => p.EmployeePermissionsList)
                    .HasForeignKey(p => p.PeriodId)
                    .OnDelete(DeleteBehavior.NoAction);
            //======================================================
            modelBuilder.Entity<EmployeePermissions>()
                    .HasOne(p => p.Supervisor)
                    .WithMany(p => p.SupervisorEPList)
                    .HasForeignKey(p => p.SupervisorId)
                    .OnDelete(DeleteBehavior.NoAction);
            //======================================================MO-AL-MO
            modelBuilder.Entity<Employee>()
                    .HasOne(p => p.employeeArchives)
                    .WithOne(p => p.employee)
                    .HasForeignKey<EmployeeArchives>(b => b.EmployeeId);
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
            modelBuilder.Entity<FinancialStatements>()
                 .HasOne(p => p.Currency)
                 .WithMany(p => p.FinancialStatementsList)
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
            modelBuilder.Entity<AttendanceRecord>()
                .HasOne(p => p.Sections)
                .WithMany(p => p.AttendanceRecordList)
                .HasForeignKey(p => p.SectionId)
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
            //==============================================
            //دوام الموظفين مع نماذج الفترة
            modelBuilder.Entity<StaffTime>()
              .HasOne(p => p.Periods)
              .WithMany(p => p.StaffTimeList)
              .HasForeignKey(p => p.PeriodId);
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
            //ضبط الدوام مع الدوام
            modelBuilder.Entity<AdjustingTime>()
              .HasOne(p => p.PermanenceModels)
              .WithMany(p => p.AdjustingTimeList)
              .HasForeignKey(p => p.PermanenceModelsId)
              .OnDelete(DeleteBehavior.NoAction);
            //ضبط الدوام مع الفترات
            modelBuilder.Entity<AdjustingTime>()
              .HasOne(p => p.Periods)
              .WithMany(p => p.AdjustingTimeList)
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
            //======================================================MO-AL-MO
            modelBuilder.Entity<Family>()
                .HasOne(p => p.Employees)
                .WithMany(p => p.FamilyList)
                  .HasForeignKey(p => p.EmployeeId);
            //==============================================
            modelBuilder.Entity<Employee>()
              .HasMany(j => j.qualifications)
              .WithMany(j => j.employees)
              .UsingEntity(j => j.ToTable("EmployeesQualifications"));

            //===============================================
            modelBuilder.Entity<AutomaticallyApprovedAdd_on>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.AutomaticallyApprovedAdd_onList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=================================================
            modelBuilder.Entity<EmployeeAdvances>()
              .HasOne(p => p.EmployeeAccount)
              .WithMany(p => p.EmployeeAdvancesList)
              .HasForeignKey(p => p.EmployeeAccountId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<EmployeeAdvances>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.EmployeeAdvancesList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<AnnualGoals>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.AnnualGoalsList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<AdministrativePromotions>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.AdministrativePromotionsList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<AdministrativePromotions>()
              .HasOne(p => p.Departments)
              .WithMany(p => p.AdministrativePromotionsList)
              .HasForeignKey(p => p.DepartmentsId)
              .OnDelete(DeleteBehavior.NoAction); 
            //=======================================
            modelBuilder.Entity<AdditionalExternalOfWork>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.AdditionalExternalOfWorkList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<AdditionalExternalOfWork>()
              .HasOne(p => p.SubstituteEmployee)
              .WithMany(p => p.SEAEOWList)
              .HasForeignKey(p => p.SubstituteEmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<EmploymentStatusManagement>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.EmploymentStatusManagementList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<EmployeeMovements>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.EmployeeMovementsList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<EmployeeMovements>()
              .HasOne(p => p.jopdescription)
              .WithMany(p => p.EmployeeMovementsList)
              .HasForeignKey(p => p.jopdescriptionId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<EmployeeViolations>()
              .HasOne(p => p.Violations)
              .WithMany(p => p.EmployeeViolationsList)
              .HasForeignKey(p => p.ViolationId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            //=======================================

            modelBuilder.Entity<AdministrativeDecisions>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.AdministrativeDecisionsList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<Permits>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.PermitsList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<AdministrativeDecisions>()
              .HasOne(p => p.Currency)
              .WithMany(p => p.AdministrativeDecisionsList)
              .HasForeignKey(p => p.CurrencyId)
              .OnDelete(DeleteBehavior.NoAction);  
            //=======================================
            modelBuilder.Entity<StaffVacations>()
              .HasOne(p => p.Employee)
              .WithMany(p => p.StaffVacationsList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<StaffVacations>()
              .HasOne(p => p.Sections)
              .WithMany(p => p.StaffVacationsList)
              .HasForeignKey(p => p.SectionId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<StaffVacations>()
              .HasOne(p => p.SubstituteStaffMember)
              .WithMany(p => p.SubstituteStaffMemberList)
              .HasForeignKey(p => p.SubstituteStaffMemberId)
              .OnDelete(DeleteBehavior.NoAction);
                        //=======================================
            modelBuilder.Entity<VacationBalance>()
              .HasOne(p => p.Employees)
              .WithMany(p => p.VacationBalanceList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<AttendanceAndAbsenceProcessing>()
              .HasOne(p => p.Employees)
              .WithMany(p => p.AttendanceAndAbsenceProcessingList)
              .HasForeignKey(p => p.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<AttendanceAndAbsenceProcessing>()
              .HasOne(p => p.Section)
              .WithMany(p => p.AttendanceAndAbsenceProcessingList)
              .HasForeignKey(p => p.SectionId)
              .OnDelete(DeleteBehavior.NoAction)
              ;//=======================================
            modelBuilder.Entity<AttendanceAndAbsenceProcessing>()
              .HasOne(p => p.Department)
              .WithMany(p => p.AttendanceAndAbsenceProcessingList)
              .HasForeignKey(p => p.DepartmentId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<AttendanceAndAbsenceProcessing>()
              .HasOne(p => p.periods)
              .WithMany(p => p.AttendanceAndAbsenceProcessing)
              .HasForeignKey(p => p.periodId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
            modelBuilder.Entity<AttendanceAndAbsenceProcessing>()
              .HasOne(p => p.PermenenceModel)
              .WithMany(p => p.AttendanceAndAbsenceProcessing)
              .HasForeignKey(p => p.permenenceId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
              modelBuilder.Entity<AttendanceAndAbsenceProcessing>()
              .HasOne(p => p.AttendanceStatus)
              .WithMany(p => p.AttendanceAndAbsenceProcessingList)
              .HasForeignKey(p => p.AttendanceStatusId)
              .OnDelete(DeleteBehavior.NoAction);
            //=======================================
              modelBuilder.Entity<AdditionalExternalOfWork>()
              .HasOne(p => p.Assignment)
              .WithMany(p => p.AdditionalExternalOfWorkList)
              .HasForeignKey(p => p.AssignmentId)
              .OnDelete(DeleteBehavior.NoAction);


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
        public DbSet<Departments> Departments { get; set; }
        //================================================== MO-AL-MO
        public DbSet<Family> Family { get; set; } 
        public DbSet<AttendanceLog> AttendanceLog { get; set; } 
        public DbSet<MachineInfo> MachineInfo { get; set; } 
        public DbSet<EmployeeArchives> EmployeeArchives { get; set; } = default!;
        public DbSet<Universities> Universities { get; set; } = default!;
        public DbSet<Sections> Sections { get; set; } = default!;
        public DbSet<JobDescription> JobDescription { get; set; } = default!;
        public DbSet<EmployeeAccount> EmployeeAccount { get; set; } = default!;
        public DbSet<Penalties> Penalties { get; set; } = default!;
        public DbSet<Violations> Violations { get; set; } = default!;
        public DbSet<Periods> Periods { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.Finance.Models.Currency> Currency { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.Finance.Models.FinanceAccount> FinanceAccount { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.PayRoll.Models.AutomaticallyApprovedAdd_on> AutomaticallyApprovedAdd_on { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.PayRoll.Models.EmployeeAdvances> EmployeeAdvances { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.PayRoll.Models.EmployeeLoans> EmployeeLoans { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.PayRoll.Models.EmployeePerks> EmployeePerks { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.PayRoll.Models.EndOfServiceClearance> EndOfServiceClearance { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.PayRoll.Models.EntitlementsAndDeductions> EntitlementsAndDeductions { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.PayRoll.Models.VacationAllowances> VacationAllowances { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.EmployeesAffsirs.Models.AdministrativePromotions> AdministrativePromotions { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.EmployeesAffsirs.Models.EmploymentStatusManagement> EmploymentStatusManagement { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.EmployeesAffsirs.Models.EmployeeMovements> EmployeeMovements { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.EmployeesAffsirs.Models.AnnualGoals> AnnualGoals { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.ViolationsAndPenaltiesAffairs.Models.EmployeeViolations> EmployeeViolations { get; set; } = default!;
        public object Employee { get; internal set; }
        public DbSet<N.G.HRS.Areas.EmployeesAffsirs.Models.AdministrativeDecisions> AdministrativeDecisions { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.EmployeesAffsirs.Models.Permits> Permits { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.MaintenanceControl.Models.StaffVacations> StaffVacations { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.MaintenanceControl.Models.VacationBalance> VacationBalance { get; set; } = default!;
        public DbSet<N.G.HRS.Areas.MaintenanceControl.Models.AttendanceRecord> AttendanceRecord { get; set; } 
        public DbSet<N.G.HRS.Areas.MaintenanceControl.Models.AdditionalExternalOfWork> AdditionalExternalOfWork { get; set; }
        public DbSet<N.G.HRS.Areas.MaintenanceControl.Models.EmployeePermissions> EmployeePermissions { get; set; } 
        public DbSet<AdditionalUnsupportedEmployees> AdditionalUnsupportedEmployees { get; set; } 
        public DbSet<AttendanceAndAbsenceProcessing> AttendanceAndAbsenceProcessing { get; set; } 
        public DbSet<AttendanceStatus> AttendanceStatus { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
       



    }
}
