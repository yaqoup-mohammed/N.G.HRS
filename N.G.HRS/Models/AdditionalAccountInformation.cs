using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Models
{
    public class AdditionalAccountInformation
    {
        public int ID { get; set; }

        public DateOnly Day { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateOnly FromDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateOnly ToDate { get; set; }

    }
}
