using static System.Runtime.InteropServices.JavaScript.JSType;

namespace N.G.HRS.Models
{
    public class Guarantees//الضمين
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set;}
        public string NameOfTheBusiness { get; set; }//الاسم التجاري
        public string CommercialRegistrationNo { get; set; }//رقم السجل التجاري
        public string ShopAddress { get; set; }
        public string HomeAdress { get; set; }
        public int NumberOfDependents { get; set; }
        public string Notes { get; set; }
        //يرتبط مع جدول(الموظف)وجدول (الضمين)
    }
}
