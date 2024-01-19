using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class AdjustingTime//ضبط الدوام
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? StaffTimeStatues { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime FromTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime ToTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly FromDate {  get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly ToDate { get; set;}
        [Required]
        [StringLength(255)]
        public string? Notes { get; set; }
        //========================================================
        [ForeignKey("PermanenceModelsId")]
        public int PermanenceModelsId { get; set; }
        public PermanenceModels PermanenceModels { get; set; }
        //====
        [ForeignKey("PeriodsId")]
        public int PeriodsId { get; set; }
        public Periods Periods { get; set; }
    }
}
