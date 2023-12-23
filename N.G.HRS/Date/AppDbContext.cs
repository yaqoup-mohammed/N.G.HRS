using Microsoft.EntityFrameworkCore;
using N.G.HRS.Models;

namespace N.G.HRS.Date
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<AdditionalAccountInformation> additionalaccountanformation { get; set; }
        public DbSet<AllowancesAndDiscounts> allowancesanddiscounts { get; set; }
        public DbSet<Archives> archives { get; set; }
        public DbSet<BasicDataForTheSalaryStatement> basicdataforthesalarystatement { get; set; }
        public DbSet<BasicDataForWagesAndSalaries> basicdataforwagesandsalaries { get; set; }
        public DbSet<DepartmentAccounts> departmentaccounts { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<EmployeeAccounts> employeeaccounts { get; set; }
        public DbSet<FinancialStatements> financialstatements { get; set; }
        public DbSet<Guarantees> guarantees { get; set; }
        public DbSet<PersonalData> personaldata { get; set; }
        public DbSet<PracticalExperiences> practicalexperiences { get; set; }
        public DbSet<Qualifications> qualifications { get; set; }
        public DbSet<StatementOfEmployeeFiles> statementofemployeefiles { get; set; }
        public DbSet<TrainingCourses> trainingcourses { get; set; }





    }
}
