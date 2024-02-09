using N.G.HRS.Areas.Employees.Models;

namespace N.G.HRS.Areas.Employees.ViewModel
{
    public class EmployeeVM
    {
        
        public Employee Employee { get; set; } 
        public Family Family { get; set; }
        public FinancialStatements FinancialStatements { get; set; }
        public Guarantees Guarantees { get; set; }
        public PersonalData PersonalData { get; set; }
        public PracticalExperiences PracticalExperiences { get; set; }
        public Qualifications Qualifications { get; set; }
        public StatementOfEmployeeFiles StatementOfEmployeeFiles { get; set; }
        public TrainingCourses TrainingCourses { get; set; }

        public  EmployeeVM()
        {

        }
    }
}
