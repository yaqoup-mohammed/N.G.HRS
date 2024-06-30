using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.PayRoll.Models
{
    public class AutomaticallyApprovedAdd_on
    {
        private double _numberOfHours;
        private int _numerOfMinutes;

        //================================================
        public int Id { get; set; }
        [Display(Name = " القسم")]

        public int? SectionsId { get; set; }
        public Sections? Sections { get; set; }
        [Display(Name = " الموظف")]

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        //=========================================
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "من تاريخ   ")]


        [DataType(DataType.Date)]
        public DateOnly FromDate { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الى تاريخ ")]

        [DataType(DataType.Date)]
        public DateOnly ToDate { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "من")]

        [DataType(DataType.Time)]
        public TimeOnly FromTime { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الى")]

        [DataType(DataType.Time)]
        public TimeOnly ToTime { get; set; }
        [Display(Name = "عدد الساعات")]

        public string? Hours    {  get; set;   }
        [Display(Name = "عدد الدقائق")]

        public string? Minutes {  get; set;   }


        public double CalculateHours()
        {
            //if (FromTime > ToTime)
            //{

            //    throw new ArgumentException("وقت البدء بعد وقت النهاية.");

            //}

            DateTime startTime = DateTime.Parse(FromTime.ToString());
            DateTime endTime = DateTime.Parse(ToTime.ToString());
            if (startTime.Hour < 12)
            {
                startTime = startTime.AddHours(12);
            }
            string startTwentyFourHourTime = startTime.ToString("HH");
            if (endTime.Hour < 12)
            {
                endTime = endTime.AddHours(12);
            }
            string endTwentyFourHourTime = endTime.ToString("HH");

            var totalHours = Math.Abs(int.Parse(endTwentyFourHourTime) - int.Parse(startTwentyFourHourTime));
            return totalHours;
        }

        public int CalculateMinutesBetween()
        {
            // Calculate total minutes between times
            var totalTimeSpan = ToTime - FromTime;
            var totalMinutes = totalTimeSpan.TotalMinutes;

            return (int)totalMinutes;
        }

        //private static int GetStandardHours(DateOnly fromDate, DateOnly toDate)
        //{
        //    // Assuming 8 hours per day, adjust as needed
        //    var workingDays = toDate.Day - fromDate.Day + 1;
        //    var standardHours = workingDays * 8;

        //    return standardHours;
        //}


    }
}
