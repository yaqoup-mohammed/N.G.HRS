using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.MaintenanceControl.Models;
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

        [Required(ErrorMessage = "أسم الدوام مطلوب!!")]
        [Display(Name = "أسم الدوام")]
        [StringLength(100, ErrorMessage = "أسم الدوام يجب الا يتجاوز 100 حرف !!")]
        public string PermanenceName { get; set; }

        [Required(ErrorMessage = "تاريخ البدأ مطلوب!!")]
        [Display(Name = " من تاريخ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly FromDate { get; set; }

        [Required(ErrorMessage = "تاريخ الانتهاء مطلوب!!")]
        [Display(Name = "الى تاريخ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly ToDate { get; set; }
        [Display(Name = " دوام مرن")]
        public bool FlexibleWorkingHours { get; set; }
        [Display(Name = " دوام بين يومين")]
        public bool WorkBetweenTwoShifts { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "من الساعة")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? FromTime { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "الى الساعة")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]

        public DateTime? ToTime { get; set; }

        [Range(0, 24, ErrorMessage = "عدد ساعات الدوام يجب الا يتجاوز 24")]
        [Display(Name = "عدد الساعات")]
        public double? HoursOfWorks { get; set; }

        public bool AddAttendanceAndDeparturePermission { get; set; }
        [Range(0, 60, ErrorMessage = "عدد دقائق الحضور المتأخر يجب الا يتجاوز 60 دقيقة")]
        public int? AllowanceForLateAttendance { get; set; }
        [Range(0, 60, ErrorMessage = "عدد دقائق الانصراف المبكر يجب الا يتجاوز 60 دقيقة")]
        public int? EarlyDeparturePermission { get; set; }

        [StringLength(255, ErrorMessage = "عدد الاحرف يجب الا يتجاوز 255")]
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }
        //==========================================
        public List<StaffTime>? StaffTimesList { get; set; }
        //=
        public List<LinkingEmployeesToShiftPeriods>? LinkingEmployeesToShiftPeriodsList { get; set; }
        //=
        public List<Weekends>? WeekendsList { get; set; }
        //=
        public List<AdjustingTime>? AdjustingTimeList { get; set; }
        //=
        public List<AttendanceAndAbsenceProcessing>? AttendanceAndAbsenceProcessing { get; set; }


    }
}