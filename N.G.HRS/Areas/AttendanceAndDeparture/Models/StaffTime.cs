﻿using System.ComponentModel.DataAnnotations;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class StaffTime
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly WorksFullTimeFromDate { get; set; }
    }
}