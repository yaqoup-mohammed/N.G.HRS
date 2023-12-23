namespace N.G.HRS.Models
{
    public class AdditionalAccountInformation
    {
        public int ID { get; set; }
        public string NormalParameter { get; set; }
        public string WeekendParameter { get; set; }

        public string OfficialHolidaysParameter { get; set; }

        public string NightPeriodParameter { get; set; }

        public string InDayParameter { get; set; }
        public TimeOnly From { get; set;}
        public TimeOnly To { get; set;}
        public DateOnly Day { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string Notes { get; set; }



    }
}
