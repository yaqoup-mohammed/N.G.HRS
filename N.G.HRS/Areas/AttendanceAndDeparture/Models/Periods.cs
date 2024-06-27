using N.G.HRS.Areas.MaintenanceControl.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class Periods
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Display(Name = " اسم الفترة")]
        public string? PeriodsName { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "من الوقت")]
        public DateTime? FromTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "الى الوقت")]
        public DateTime? ToTime { get; set; }
        [Display(Name = "السبت")]
        public bool Saturday { get; set;}
        [Display(Name = "الاحد")]
        public bool SunDay { get; set;}
        [Display(Name = "الاثنين")]
        public bool Monday { get; set;}
        [Display(Name = "الثلاثاء")]
        public bool Tuesday { get; set;}
        [Display(Name = "الاربعاء")]
        public bool Wednesday { get;set;}
        [Display(Name = "الخميس")]
        public bool Thursday { get; set;}
        [Display(Name = "الجمعة")]
        public bool Friday { get; set;}
        [Display(Name = "الساعات")]
        public int? Hours { get; set; }
        [Display(Name = "الدقائق")]
        public int? Muinutes { get; set; }
        //============================================
        [Display(Name = "الدوام")]
        public int? PermanenceModelsId { get; set; }
        [Display(Name = "نوع الدوام")]
        public PermanenceModels? PermanenceModels { get; set; } 
        //===============================================================
        public List<LinkingEmployeesToShiftPeriods>? LinkingEmployeesToShiftPeriodsList { get; set; }
        //===============================================================
        public List<AttendanceRecord>? AttendanceRecordList { get; set; }
        //===============================================================
        public List<EmployeePermissions>? EmployeePermissionsList { get; set; }

        //===============================================================
        public List<StaffVacations>? StaffVacationsList { get; set; }

        public List<Weekends>? WeekendsList { get; set; }
        public List<AdjustingTime>? AdjustingTimeList { get; set; }
        public List<Periods>? PeriodsList { get; set; }
        public List<StaffTime>? StaffTimeList { get; set; }

        //=
        public List<AttendanceAndAbsenceProcessing>? AttendanceAndAbsenceProcessing { get; set; }




    }
}
