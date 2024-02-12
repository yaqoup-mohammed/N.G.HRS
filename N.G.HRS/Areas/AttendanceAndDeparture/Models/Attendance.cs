namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public int dwVerifyMode { get; set; }
        public int dwInOutMode { get; set; }
        int dwYear;
        int dwMonth;
        int dwDay;
        int dwHour;
        int dwMinute;
        int dwSecond;
        public DateTime? Date
        {
            get
            {
                if (dwYear != null && dwMonth != null && dwDay != null && dwHour != null && dwMinute != null && dwSecond != null)
                {
                    return new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond);
                }
                else { return null; }
            }
            set { }
        }

    }
}
