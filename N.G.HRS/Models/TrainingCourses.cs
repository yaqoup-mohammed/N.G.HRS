namespace N.G.HRS.Models
{
    public class TrainingCourses
    {
        public int Id { get; set; }
        public string NameCourses { get; set; }
        public string WhereToGetIt { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set;}

    }
}
