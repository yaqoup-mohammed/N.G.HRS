﻿using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Models
{
    public class LinkingEmployeesToShiftPeriods
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfStartWork { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfEndWork { get; set;}
        //===================================================
        //=
        [ForeignKey("DepartmentsId")]
        public int DepartmentsId { get; set; }
        public Departments Departments { get; set; }
        //=
        [ForeignKey("SectionsId")]
        public int SectionsId { get; set; }
        public Sections Sections { get; set; }
        //=
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        //=
        [ForeignKey("PermanenceModelsId")]
        public int PermanenceModelsId { get; set; }
        public PermanenceModels PermanenceModels { get; set; }
        //=
        [ForeignKey("PeriodsId")]
        public int PeriodsId { get; set; }
        public Periods Periods { get; set; }
        //=
    }
}
