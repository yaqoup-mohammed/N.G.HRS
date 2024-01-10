using N.G.HRS.Areas.GeneralConfiguration.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.Employees.Models
{
    public class Qualifications
    {
        //يرتبط بجدول (المؤهل) و(التخصص) و(الحامعات) و (الموظفن)
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ الحصول علية")]
        public DateOnly ReceivedDate { get; set; }
        //=================================================
        public List<EducationalQualification> educationalQualificationsList { get; set; }
        public List<Specialties> specialtiesList { get; set; }
        public List<Universities> universitiesList { get; set; }
        //=====================================================
        public ICollection<Employee> employees { get; set; }


    }
}
