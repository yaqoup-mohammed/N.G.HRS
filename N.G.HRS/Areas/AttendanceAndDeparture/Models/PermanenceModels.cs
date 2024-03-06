using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class PermanenceModels//نماذج الدوام
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string PermanenceName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly FromDate { 
            get { 
            if(FromDate>ToDate)
                {
                    throw new Exception("يجب ان يكون تاريخ البدء اقل من تاريخ الانتهاء");
                }
            else if(FromDate < DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new Exception("يجب ان لا يكون تاريخ البدء اصغر من التاريخ الحالي");
                }
                    return FromDate;
            } set { } }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly ToDate { 
            get { 
            if(FromDate > ToDate)
                {
                    throw new Exception("يجب ان يكون تاريخ الانتهاء اكبر من تاريخ البدء");
                }
                return ToDate;

            } set { } }
        public bool FlexibleWorkingHours { get; set; }
        public bool WorkBetweenTwoDays { get; set; }
        //public bool ShiftTime { get; set; }
        //public bool Working24Hours { get; set; }
        [DataType(DataType.Time)]
        public DateTime FromTime { 
            get {
            if(FromTime > ToTime)
                {
                    throw new Exception("يجب ان يكون وقت البدء اقل من وقت النهاية");
                }
            else if (FromTime.TimeOfDay < DateTime.Now.TimeOfDay)
                {
                    throw new Exception("يجب ان لا يكون وقت البدء اصغر من وقت الحالي");
                }
                    return FromTime;
            } set { } }
        [DataType(DataType.Time)]
        public DateTime ToTime { 
            get {
            if(FromTime > ToTime)
                {
                    throw new Exception("يجب ان يكون وقت النهاية اكبر من وقت البدء");
                }
            return ToTime;
            } set { } }
        [Range(0, 24)]
        public double? HoursOfWorks { 
            get {
                try
                {
                    if (FlexibleWorkingHours)
                    {
                        return CalculateHourOfWork();
                    }
                    else if(WorkBetweenTwoDays) 
                    {
                    return CalculateHourOfWorkBetweenDays();
                    }
                    else 
                    {
                        return CalculateHourOfWork();
                    }
                }
                catch
                {
                    throw new Exception("يجب ان تكون تاريخ البدء اقل من تاريخ الانتهاء");
                }
            } set { } }
        public bool AddAttendanceAndDeparturePermission { get; set; }
        public double? AllowanceForLateAttendance { get; set; }//سماحية حظور متأخر
        public double? EarlyDeparturePermission { get; set; }//سماحية مغادرة مبكرة
        [StringLength(255)]
        public string? Notes {  get; set; }
        //==========================================
        public List<StaffTime>? StaffTimesList { get; set; }
        //=
        public List<LinkingEmployeesToShiftPeriods>? LinkingEmployeesToShiftPeriodsList { get; set; }
        //=
        public List<Weekends>? WeekendsList { get; set; }
        //=
        public List<WeekendsForFlexibleWorking>? WeekendsForFlexibleWorkingList { get; set; }
        public List<AdjustingTime>? AdjustingTimeList { get; set; }
        //=

        //public  double CalculateHourOfWorkBetweenDays()
        //{
        //    DateOnly nextDay = FromDate.AddDays(1);
        //    DateTime firstDate = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, FromTime.Hour, FromTime.Minute, FromTime.Second);
        //    DateTime secondDate = new DateTime(nextDay.Year,nextDay.Month,nextDay.Day, ToTime.Hour, ToTime.Minute, ToTime.Second);

        //    TimeSpan timeSpan = secondDate - firstDate;

        //    double totalHours = timeSpan.Days * 24 + timeSpan.Hours;

        //    return totalHours;
        //}
        public  double CalculateHourOfWorkBetweenDays()
        {
            DateOnly nextDay = FromDate.AddDays(1);
            DateTime firstDate = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, FromTime.Hour, FromTime.Minute, FromTime.Second);
            DateTime secondDate = new DateTime(nextDay.Year,nextDay.Month,nextDay.Day, ToTime.Hour, ToTime.Minute, ToTime.Second);

            if(AddAttendanceAndDeparturePermission)
            {
                TimeSpan withPermission = secondDate.AddMinutes(-EarlyDeparturePermission??0) - firstDate.AddMinutes(AllowanceForLateAttendance??0);

                return withPermission.TotalHours;
            }
            TimeSpan timeSpan = secondDate - firstDate;

            double totalHours = timeSpan.Hours;

            return totalHours;
        }
        public double CalculateHourOfWork()
        {
            DateTime firstTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, FromTime.Hour, FromTime.Minute, FromTime.Second);
            DateTime secondTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, ToTime.Hour, ToTime.Minute, ToTime.Second);
            if (AddAttendanceAndDeparturePermission)
            {
                TimeSpan withPermission = secondTime.AddMinutes(-EarlyDeparturePermission ?? 0) - firstTime.AddMinutes(AllowanceForLateAttendance ?? 0);

                return withPermission.TotalHours;
            }
            TimeSpan timeSpan = secondTime - firstTime;

            int totalHours = timeSpan.Hours;

            return totalHours;
        }


    }
}
