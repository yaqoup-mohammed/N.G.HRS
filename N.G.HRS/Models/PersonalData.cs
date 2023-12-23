namespace N.G.HRS.Models
{
    public class PersonalData
    {
        public int Id { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Age { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public DateOnly CardExpiryDate { get; set; }
        public DateTime CardExpiryTime { get; set;}
        //يتم الربط مع جدول (الجنس) وجدول (الجنسية) وجدول( الديانة) و(الموظف)و (الحالة الاجتماعية)
    }
}
