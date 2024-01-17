using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class WeekendsForFlexibleWorking: Weekends
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, 50)]
        public int NumbersOfHours { get; set;}
        //===============================================
        [ForeignKey("PermanenceModelsId")]
        public int PermanenceModelsId { get; set; }
        public PermanenceModels PermanenceModels { get; set; }

    }
}
