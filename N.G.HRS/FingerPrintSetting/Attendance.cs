namespace N.G.HRS.FingerPrintSetting
{
    public class Attendance
    {
        public int Id { get; set; }
        public int EnrollNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string State { get; set; }
        public int MachineNo { get; set; }
        public bool IsProcessed { get; set; }

        //public int VerifyMode { get; set; }
        //public int InOutMode { get; set; }
        //public int Year { get; set; }
        //public int Month { get; set; }
        //public int Day { get; set; }
        //public int Hour { get; set; }
        //public int Minute { get; set; }
        //public int Second { get; set; }
        //public int WorkCode { get; set; }
        //public int Reserved { get; set; }
    }
}
