namespace N.G.HRS.Models
{
    public class PracticalExperiences
    {
        public int ID { get; set; }
        public string ExperiencesType { get; set; }
        public string PlacToGainExperience { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set;}
        public string Duration { get; set; }
        //يرتبط مع جدول الموظفين


    }
}
