namespace N.G.HRS.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public DateOnly DateOfEmployment { get; set; }
        public DateOnly PlacementDate { get; set; }//تاريخ التثبيت
        public string EmploymentStatus { get; set; }
        public DateOnly RehireDate { get; set; }
        public DateOnly DateOfStoppingWork { get; set; }
        public Boolean UsedFingerprint { get; set; }
        public Boolean SubjectToInsurance { get; set;}//خاضع للتامين 
        public DateOnly DateInsurance { get; set; }
        public string Notes { get; set; }
        //يرتبط مع جدول(الادارة) وجدول (القسم) وجدول (الوصف الوظيفي) وجدول (جهاز البصمة) و علاقة(self)




    }
}
