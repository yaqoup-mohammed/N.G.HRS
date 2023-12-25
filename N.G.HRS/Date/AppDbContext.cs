using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;

namespace N.G.HRS.Date
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

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
