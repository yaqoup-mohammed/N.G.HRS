using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Employees.ViewModel;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.Areas.MaintenanceControl.ViewModels;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Date;
using N.G.HRS.FingerPrintSetting;
using N.G.HRS.HRSelectList;
using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.V202002;
using SQLitePCL;

namespace N.G.HRS.Areas.MaintenanceControl.Controllers
{
    [Area("MaintenanceControl")]
    public class AttendanceAndAbsenceProcessingVMController : Controller
    {
        private readonly AppDbContext _context;
        public AttendanceAndAbsenceProcessingVMController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            await PopulateDropdownListsAsync();
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AttendanceAndAbsenceProcessingVM attendanceAndAbsenceProcessingVM)
        {
            //var AdditionalExternal= _context.AdditionalExternalOfWork.Where()
            return View();
        }
        private async Task PopulateDropdownListsAsync()
        {
            //-----------------------Sections---------------------------
            var Sections = await _context.Sections.ToListAsync();
            ViewData["Sections"] = new SelectList(Sections, "Id", "SectionsName");
            //-----------------------Departments---------------------------
            var Departments = await _context.Departments.ToListAsync();
            ViewData["Departments"] = new SelectList(Departments, "Id", "SubAdministration");
            //-----------------------JobDescription---------------------------

            //-----------------------Manager---------------------------
            var employee = await _context.employee.ToListAsync();
            ViewData["employee"] = new SelectList(employee, "Id", "EmployeeName");


        }



        //===============================================================================
        //===============================================================================
        //List<AttendanceStatus> attSta = new List<AttendanceStatus>
        //{
        //    new AttendanceStatus add() {}
        //    new AttendanceStatus() {Id = 1 ,Name = "حضور"},
        //    new AttendanceStatus() {Id = 2 ,Name = "غياب"},
        //    new AttendanceStatus() {Id = 3 ,Name = "سماحية انصراف مبكر"},
        //    new AttendanceStatus() {Id = 4 ,Name = "سماحية حضور متأخر"},
        //    new AttendanceStatus() {Id = 5 ,Name = "اذن"},
        //    new AttendanceStatus() {Id = 6 ,Name = "اجازة"},
        //    new AttendanceStatus() {Id = 7 ,Name = "اجازة رسمية"},
        //    new AttendanceStatus() {Id = 8 ,Name = "اجازة اسبوعية"},
        //    new AttendanceStatus() {Id = 9 ,Name = "إضافي معتمد"},
        //    new AttendanceStatus() {Id = 10 ,Name = "إضافي غير معتمد"},
        //    new AttendanceStatus() {Id = 11 ,Name = "انصراف بدون عذر"},
        //    new AttendanceStatus() {Id = 12 ,Name = "تأخير"},
        //    new AttendanceStatus() {Id = 13 ,Name = "غياب نصف يوم"},
        //    new AttendanceStatus() {Id = 14 ,Name = "سماحية حضور وانصراف"},
        //    new AttendanceStatus() {Id = 15 ,Name = "تكليف خارجي "},
        //};
        public async Task<IActionResult> GetAttendanceStatus(DateTime from, DateTime to)
        {
            try
            {
                if (from != null)
                {

                    List<AttendanceAndAbsenceProcessing> attAbsences = new List<AttendanceAndAbsenceProcessing>();

                    var employee = await _context.employee.ToListAsync();
                    foreach (var i in employee)
                    {
                        if (to != null)
                        {
                            DateTime currentDate = from;
                            while (currentDate <= to)
                            {
                                List<AttendanceAndAbsenceProcessing> process = await CalculateAttendance(i, currentDate);
                                if (process == null)
                                {
                                    return Json(0);
                                }
                                foreach (var item in process)
                                {
                                    if (item != null)
                                    {
                                        attAbsences.Add(item);
                                        var check = await _context.AttendanceAndAbsenceProcessing.AnyAsync(x => x.EmployeeId == item.EmployeeId && x.Date == item.Date && x.FromTime == item.FromTime && x.ToTime == item.ToTime && x.AttendanceStatusId == item.AttendanceStatusId);
                                        if (check)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            await _context.AttendanceAndAbsenceProcessing.AddAsync(item);

                                        }
                                    }
                                    else
                                    {
                                        return Json(0);
                                    }
                                }
                                currentDate = currentDate.AddDays(1);
                            }
                        }
                        else
                        {
                            List<AttendanceAndAbsenceProcessing> process = await CalculateAttendance(i, from);
                            if (process == null)
                            {
                                return Json(0);
                            }
                            foreach (var item in process)
                            {
                                if (item != null)
                                {
                                    attAbsences.Add(item);
                                    var check = await _context.AttendanceAndAbsenceProcessing.AnyAsync(x => x.EmployeeId == item.EmployeeId && x.Date == item.Date && x.FromTime == item.FromTime && x.ToTime == item.ToTime && x.AttendanceStatusId == item.AttendanceStatusId);
                                    if (check)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        await _context.AttendanceAndAbsenceProcessing.AddAsync(item);

                                    }
                                }
                                else
                                {
                                    return Json(0);
                                }
                            }
                        }

                    }
                    if (attAbsences == null)
                    {
                        return Json(0);
                    }
                    await _context.SaveChangesAsync();
                    var attAbsences3 = await _context.AttendanceAndAbsenceProcessing.Include(x => x.Employees).Include(x => x.AttendanceStatus).Include(x => x.periods).Include(x => x.PermenenceModel)
                    .Where(x => x.IsProcssessedBefore == false && x.Date >= from && x.Date <= to && x.AttendanceStatusId == 2 || x.AttendanceStatusId == 11 || x.AttendanceStatusId == 13 || x.AttendanceStatusId == 10).ToListAsync();
                    foreach (var item in attAbsences3)
                    {
                        var employeePermissions = await _context.EmployeePermissions.FirstOrDefaultAsync(x => x.Date == item.Date && x.EmployeeId == item.Employees.Id && x.FromTime.TimeOfDay == item.FromTime && x.ToTime.TimeOfDay == item.ToTime);
                        var externalwork = await _context.AdditionalExternalOfWork.FirstOrDefaultAsync(x => x.AssignmentId == 2 && x.Date == item.Date && x.EmployeeId == item.Employees.Id && x.FromTime.TimeOfDay == item.FromTime && x.ToTime.TimeOfDay == item.ToTime);
                        var additinalwork = await _context.AdditionalExternalOfWork.FirstOrDefaultAsync(x => x.AssignmentId == 1 && x.Date == item.Date && x.EmployeeId == item.Employees.Id && x.FromTime.TimeOfDay == item.FromTime && x.ToTime.TimeOfDay == item.ToTime);
                        var staffVacation = await _context.StaffVacations.FirstOrDefaultAsync(x =>  x.EmployeeId == item.Employees.Id && x.FromDate == item.Date );
                        if (employeePermissions != null)
                        {
                            AttendanceAndAbsenceProcessing attendance = new AttendanceAndAbsenceProcessing() { };
                            attendance.EmployeeId = item.Employees.Id;
                            attendance.DepartmentId = item.Employees.DepartmentsId;
                            attendance.SectionId = item.Employees.SectionsId;
                            attendance.AttendanceStatusId = 15;
                            attendance.Date = item.Date;
                            attendance.FromTime = item.FromTime;
                            attendance.ToTime = item.ToTime;
                            attendance.TotalWorkMinutes = item.TotalWorkMinutes;
                            attendance.MinutesOfLate = item.MinutesOfLate;
                            attendance.periodId = item.periodId;
                            attendance.permenenceId = item.permenenceId;
                            attendance.IsProcssessed = false;
                            attendance.IsProcssessedBefore = true;
                            item.IsProcssessedBefore = true;
                            employeePermissions.IsProccessed = true;
                            _context.EmployeePermissions.Update(employeePermissions);
                            _context.AttendanceAndAbsenceProcessing.Add(attendance);
                            _context.AttendanceAndAbsenceProcessing.Update(item);
                        }
                        else if (externalwork != null)
                        {
                            AttendanceAndAbsenceProcessing attendance = new AttendanceAndAbsenceProcessing() { };
                            attendance.EmployeeId = item.Employees.Id;
                            attendance.DepartmentId = item.Employees.DepartmentsId;
                            attendance.SectionId = item.Employees.SectionsId;
                            attendance.AttendanceStatusId = 5;
                            attendance.Date = item.Date;
                            attendance.FromTime = item.FromTime;
                            attendance.ToTime = item.ToTime;
                            attendance.TotalWorkMinutes = item.TotalWorkMinutes;
                            attendance.MinutesOfLate = item.MinutesOfLate;
                            attendance.periodId = item.periodId;
                            attendance.permenenceId = item.permenenceId;
                            attendance.IsProcssessed = false;
                            attendance.IsProcssessedBefore = true;
                            item.IsProcssessedBefore = true;
                            externalwork.IsProccessed = true;
                            _context.AdditionalExternalOfWork.Update(externalwork);
                            _context.AttendanceAndAbsenceProcessing.Add(attendance);
                            _context.AttendanceAndAbsenceProcessing.Update(item);
                        }
                        else if (additinalwork != null && item.AttendanceStatusId == 10)
                        {
                            AttendanceAndAbsenceProcessing attendance = new AttendanceAndAbsenceProcessing() { };
                            attendance.EmployeeId = item.Employees.Id;
                            attendance.DepartmentId = item.Employees.DepartmentsId;
                            attendance.SectionId = item.Employees.SectionsId;
                            attendance.AttendanceStatusId = 9;
                            attendance.Date = item.Date;
                            attendance.FromTime = item.FromTime;
                            attendance.ToTime = item.ToTime;
                            attendance.TotalWorkMinutes = item.TotalWorkMinutes;
                            attendance.MinutesOfLate = item.MinutesOfLate;
                            attendance.periodId = item.periodId;
                            attendance.permenenceId = item.permenenceId;
                            attendance.IsProcssessed = false;
                            attendance.IsProcssessedBefore = true;
                            item.IsProcssessedBefore = true;
                            additinalwork.IsProccessed = true;

                            _context.AdditionalExternalOfWork.Update(additinalwork);
                            _context.AttendanceAndAbsenceProcessing.Add(attendance);
                            _context.AttendanceAndAbsenceProcessing.Update(item);
                        } else if (staffVacation != null )
                        {
                            AttendanceAndAbsenceProcessing attendance = new AttendanceAndAbsenceProcessing() { };
                            attendance.EmployeeId = item.Employees.Id;
                            attendance.DepartmentId = item.Employees.DepartmentsId;
                            attendance.SectionId = item.Employees.SectionsId;
                            attendance.AttendanceStatusId = 7;
                            attendance.Date = item.Date;
                            attendance.FromTime = item.FromTime;
                            attendance.ToTime = item.ToTime;
                            attendance.TotalWorkMinutes = item.TotalWorkMinutes;
                            attendance.MinutesOfLate = item.MinutesOfLate;
                            attendance.periodId = item.periodId;
                            attendance.permenenceId = item.permenenceId;
                            attendance.IsProcssessed = false;
                            attendance.IsProcssessedBefore = true;
                            item.IsProcssessedBefore = true;
                            additinalwork.IsProccessed = true;
                            _context.StaffVacations.Update(staffVacation);
                            _context.AttendanceAndAbsenceProcessing.Add(attendance);
                            _context.AttendanceAndAbsenceProcessing.Update(item);
                        }

                        await _context.SaveChangesAsync();

                    };
                    var attAbsences2 = await _context.AttendanceAndAbsenceProcessing.Include(x => x.Employees).Include(x => x.AttendanceStatus).Include(x => x.periods).Include(x => x.PermenenceModel)
                    .Where(x => x.IsProcssessed == false && x.Date >= from && x.Date <= to).Select(x => new
                    {
                        eNumber = x.Employees.EmployeeNumber,
                        eName = x.Employees.EmployeeName,
                        attendanceStatus = x.AttendanceStatus.Name,
                        permenenceModel = x.PermenenceModel.PermanenceName,
                        periods = x.periods.PeriodsName,
                        from = x.FromTime,
                        to = x.ToTime,
                        date = x.Date,
                        minutes = x.TotalWorkMinutes,
                        minutesLate = x.MinutesOfLate,
                        isProcessed = x.IsProcssessed,
                        isProcessedBefor = x.IsProcssessedBefore
                    }).ToListAsync();
                    if (attAbsences2 == null)
                    {
                        return Json(0);
                    }

                    return Json(attAbsences2);
                }
                //else if (from != null && to == null)
                //{
                //    var attAbsences2 = await _context.AttendanceAndAbsenceProcessing.Include(x => x.Employees).Include(x => x.AttendanceStatus).Include(x => x.periods).Include(x => x.PermenenceModel)
                //                        .Where(x => x.IsProcssessed == false && x.Date >= from && x.Date <= to).Select(x => new
                //                        {
                //                            eNumber = x.Employees.EmployeeNumber,
                //                            eName = x.Employees.EmployeeName,
                //                            attendanceStatus = x.AttendanceStatus.Name,
                //                            permenenceModel = x.PermenenceModel.PermanenceName,
                //                            periods = x.periods.PeriodsName,
                //                            from = x.FromTime,
                //                            to = x.ToTime,
                //                            date = x.Date,
                //                            minutes = x.TotalWorkMinutes,
                //                            minutesLate = x.MinutesOfLate,
                //                            isProcessed = x.IsProcssessed,
                //                            isProcessedBefor = x.IsProcssessedBefore
                //                        }).ToListAsync();

                //    if (attAbsences2 == null)
                //    {
                //        return Json(0);
                //    }
                //    return Json(attAbsences2);

                //}
                else
                {

                    return Json(0);
                }

            }
            catch (Exception ex)
            {

                return Json(new { error = ex.Message });
            }

        }
        //===============================================================================
        //===============================================================================
        public async Task<List<AttendanceAndAbsenceProcessing>> CalculateAttendance(Employee employee, DateTime workDate)
        {

            var date = new DateOnly(workDate.Year, workDate.Month, workDate.Day);
            var records = await _context.MachineInfo.Where(x => x.IndRegID == int.Parse(employee.EmployeeNumber) && x.DateOnlyRecord == date).ToListAsync();
            var sT = _context.staffTimes.Include(x => x.PermanenceModels).Include(x => x.Periods).FirstOrDefault(x => x.EmployeeId == employee.Id);
            List<AttendanceAndAbsenceProcessing> attAbsences = new List<AttendanceAndAbsenceProcessing>();
            if (sT != null)
            {
                var period = _context.Periods.Where(x => x.Id == sT.PeriodId).FirstOrDefault();
                var workStartTime = new TimeSpan(period.FromTime.Value.Hour, period.FromTime.Value.Minute, 0);
                var workEndTime = new TimeSpan(period.ToTime.Value.Hour, period.ToTime.Value.Minute, 0);// Assuming ToTime is a DateTime property and you want to extract the TimeOfDay as a TimeSpan // Assuming ToTime is a DateTime property and you want to extract the TimeOfDay as a TimeSpan
                var holiday = CheckHoliday(employee.Id, workDate);
                var officialVacations = CheckOfficialVacations(date);
                var isWeekend = IsWeekend(workDate, sT);
                // Logic for retrieving work end time (consider holidays, shifts, etc.)
                if (records.Count == 0)
                {
                    if (isWeekend)
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 8,
                            Date = workDate.Date,
                            FromTime = workStartTime,
                            ToTime = workEndTime,
                            TotalWorkMinutes = period.Muinutes,
                            MinutesOfLate = 0,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                        return attAbsences;
                    }
                    else if (holiday)
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 6,
                            Date = workDate.Date,
                            FromTime = workStartTime,
                            ToTime = workEndTime,
                            TotalWorkMinutes = period.Muinutes,
                            MinutesOfLate = 0,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                        return attAbsences;
                    }
                    else if (officialVacations)
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 7,
                            Date = workDate.Date,
                            FromTime = workStartTime,
                            ToTime = workEndTime,
                            TotalWorkMinutes = period.Muinutes,
                            MinutesOfLate = 0,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                        return attAbsences;
                    }
                    else
                    {
                        // Handle no fingerprint scenario (e.g., absent)
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 2,
                            Date = workDate.Date,
                            FromTime = workStartTime,
                            ToTime = workEndTime,
                            TotalWorkMinutes = 0,
                            MinutesOfLate = period.Muinutes,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                        return attAbsences;
                    }
                }
                else if (records.Count == 1)
                {
                    var onlyRecords = records.OrderBy(r => r.DateOnlyRecord).FirstOrDefault();
                    var onlyRecord = new TimeSpan(onlyRecords.TimeOnlyRecord.Value.Hour, onlyRecords.TimeOnlyRecord.Value.Minute, 0);
                    var late = CalculateLateMinutes(onlyRecords, workStartTime);
                    var early = CalculateEarlyDepartureMinutes(onlyRecords, workEndTime);
                    if (isWeekend)
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 10,
                            Date = workDate.Date,
                            FromTime = onlyRecord,
                            ToTime = workEndTime,
                            TotalWorkMinutes = period.Muinutes / 2,
                            MinutesOfLate = period.Muinutes / 2,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                        return attAbsences;
                    }
                    else if (holiday)
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 10,
                            Date = workDate.Date,
                            FromTime = onlyRecord,
                            ToTime = workEndTime,
                            TotalWorkMinutes = period.Muinutes / 2,
                            MinutesOfLate = period.Muinutes / 2,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);

                        return attAbsences;
                    }
                    else if (officialVacations)
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 10,
                            Date = workDate.Date,
                            FromTime = onlyRecord,
                            ToTime = workEndTime,
                            TotalWorkMinutes = period.Muinutes / 2,
                            MinutesOfLate = period.Muinutes / 2,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                        return attAbsences;
                    }
                    else
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 13,
                            Date = workDate.Date,
                            FromTime = onlyRecord,
                            ToTime = workEndTime,
                            TotalWorkMinutes = period.Muinutes / 2,
                            MinutesOfLate = period.Muinutes / 2,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                    }
                    return attAbsences;
                }
                else
                {

                    var ftFingerprint = records.OrderBy(r => r.DateOnlyRecord).OrderBy(x => x.TimeOnlyRecord).FirstOrDefault();
                    var firstFingerprint = new TimeSpan(ftFingerprint.TimeOnlyRecord.Value.Hour, ftFingerprint.TimeOnlyRecord.Value.Minute, 0); ;

                    var lFingerprint = records.OrderBy(r => r.DateOnlyRecord).OrderBy(x => x.TimeOnlyRecord).LastOrDefault();
                    var lastFingerprint = new TimeSpan(lFingerprint.TimeOnlyRecord.Value.Hour, lFingerprint.TimeOnlyRecord.Value.Minute, 0); ;

                    var allowedLateMinutes = sT.PermanenceModels.AllowanceForLateAttendance;
                    var earlyDepartureMinutes = sT.PermanenceModels.EarlyDeparturePermission;


                    var workDuration = CalculateWorkDuration(ftFingerprint, lFingerprint);
                    var wFFFP = CalculateWorkDuration2(workStartTime, lFingerprint);
                    var fTET = CalculateWorkDuration3(ftFingerprint, workEndTime);
                    var lateMinutes = CalculateLateMinutes(ftFingerprint, workStartTime);
                    var isLate = IsEmployeeLate(lateMinutes, allowedLateMinutes);
                    var earlyDeparture = CalculateEarlyDepartureMinutes(lFingerprint, workEndTime);
                    var isleftEarly = IsEmployeeLeftEarly(earlyDeparture, earlyDepartureMinutes);
                    var isAllowance = sT.PermanenceModels.AddAttendanceAndDeparturePermission;
                    var workLate = CalculateAdditionalTimeInMinutes(lastFingerprint, workEndTime);

                    if (isWeekend)
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 10,
                            Date = workDate.Date,
                            FromTime = firstFingerprint,
                            ToTime = lastFingerprint,
                            TotalWorkMinutes = workDuration,
                            MinutesOfLate = 0,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                        return attAbsences;
                    }

                    else if (holiday)
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 10,
                            Date = workDate.Date,
                            FromTime = firstFingerprint,
                            ToTime = lastFingerprint,
                            TotalWorkMinutes = workDuration,
                            MinutesOfLate = 0,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                        return attAbsences;
                    }
                    else if (officialVacations)
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 10,
                            Date = workDate.Date,
                            FromTime = firstFingerprint,
                            ToTime = lastFingerprint,
                            TotalWorkMinutes = workDuration,
                            MinutesOfLate = 0,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                        return attAbsences;
                    }
                    else if (firstFingerprint == workStartTime && lastFingerprint == workEndTime)
                    {
                        AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = employee.DepartmentsId,
                            SectionId = employee.SectionsId,
                            AttendanceStatusId = 1,
                            Date = workDate.Date,
                            FromTime = workStartTime,
                            ToTime = workEndTime,
                            TotalWorkMinutes = period.Muinutes,
                            MinutesOfLate = 0,
                            periodId = sT.PeriodId,
                            permenenceId = sT.PermanenceModelsId,
                            IsProcssessed = false,
                            IsProcssessedBefore = false
                        };
                        attAbsences.Add(attAbsence);
                        return attAbsences;
                    }
                    else if (isAllowance)
                    {
                        var isBetweenStartAndAllowanceTime = IsWithinStartAndAllowanceTime(firstFingerprint, workStartTime, sT.PermanenceModels.AllowanceForLateAttendance);
                        var isBetweenEndtAndAllowanceTime = IsWithinEndAndAllowanceTime(lastFingerprint, workEndTime, sT.PermanenceModels.EarlyDeparturePermission);
                        if (isBetweenStartAndAllowanceTime == 1 && isBetweenEndtAndAllowanceTime == 4)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 1,
                                Date = workDate.Date,
                                FromTime = workEndTime,
                                ToTime = workEndTime,
                                TotalWorkMinutes = period.Muinutes,
                                MinutesOfLate = 0,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            if (workLate >= 10)
                            {
                                AttendanceAndAbsenceProcessing attAbsence2 = new AttendanceAndAbsenceProcessing()
                                {
                                    EmployeeId = employee.Id,
                                    DepartmentId = employee.DepartmentsId,
                                    SectionId = employee.SectionsId,
                                    AttendanceStatusId = 10,
                                    Date = workDate.Date,
                                    FromTime = workEndTime,
                                    ToTime = lastFingerprint,
                                    TotalWorkMinutes = (int)workLate,
                                    MinutesOfLate = 0,
                                    periodId = sT.PeriodId,
                                    permenenceId = sT.PermanenceModelsId,
                                    IsProcssessed = false,
                                    IsProcssessedBefore = false
                                };
                                attAbsences.Add(attAbsence2);
                            }
                            return attAbsences;
                        }
                        else if (isBetweenStartAndAllowanceTime == 1 && isBetweenEndtAndAllowanceTime == 5)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 3,
                                Date = workDate.Date,
                                FromTime = lastFingerprint,
                                ToTime = workEndTime,
                                TotalWorkMinutes = period.Muinutes,
                                MinutesOfLate = earlyDeparture,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            return attAbsences;
                        }
                        else if (isBetweenStartAndAllowanceTime == 1 && isBetweenEndtAndAllowanceTime == 6)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 11,
                                Date = workDate.Date,
                                FromTime = lastFingerprint,
                                ToTime = workEndTime,
                                TotalWorkMinutes = wFFFP,
                                MinutesOfLate = earlyDeparture,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            return attAbsences;

                        }
                        else if (isBetweenStartAndAllowanceTime == 2 && isBetweenEndtAndAllowanceTime == 4)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 4,
                                Date = workDate.Date,
                                FromTime = workStartTime,
                                ToTime = firstFingerprint,
                                TotalWorkMinutes = period.Muinutes,
                                MinutesOfLate = lateMinutes,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            if (lateMinutes < workLate)
                            {
                                if (workLate >= 10)
                                {
                                    AttendanceAndAbsenceProcessing attAbsence2 = new AttendanceAndAbsenceProcessing()
                                    {
                                        EmployeeId = employee.Id,
                                        DepartmentId = employee.DepartmentsId,
                                        SectionId = employee.SectionsId,
                                        AttendanceStatusId = 10,
                                        Date = workDate.Date,
                                        FromTime = workEndTime,
                                        ToTime = lastFingerprint,
                                        TotalWorkMinutes = (int)workLate,
                                        MinutesOfLate = 0,
                                        periodId = sT.PeriodId,
                                        permenenceId = sT.PermanenceModelsId,
                                        IsProcssessed = false,
                                        IsProcssessedBefore = false
                                    };
                                    attAbsences.Add(attAbsence2);
                                }
                            }
                            return attAbsences;
                        }
                        else if (isBetweenStartAndAllowanceTime == 2 && isBetweenEndtAndAllowanceTime == 5)
                        {
                            var m = lateMinutes + earlyDeparture;
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 14,
                                Date = workDate.Date,
                                FromTime = firstFingerprint,
                                ToTime = lastFingerprint,
                                TotalWorkMinutes = period.Muinutes,
                                MinutesOfLate = m,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            return attAbsences;
                        }
                        else if (isBetweenStartAndAllowanceTime == 2 && isBetweenEndtAndAllowanceTime == 6)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 11,
                                Date = workDate.Date,
                                FromTime = lastFingerprint,
                                ToTime = workEndTime,
                                TotalWorkMinutes = wFFFP,
                                MinutesOfLate = earlyDeparture,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            return attAbsences;
                        }
                        else if (isBetweenStartAndAllowanceTime == 3 && isBetweenEndtAndAllowanceTime == 4)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 12,
                                Date = workDate.Date,
                                FromTime = firstFingerprint,
                                ToTime = workEndTime,
                                TotalWorkMinutes = fTET,
                                MinutesOfLate = earlyDeparture,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            if (lateMinutes < workLate)
                            {

                                if (workLate >= 10)
                                {
                                    AttendanceAndAbsenceProcessing attAbsence2 = new AttendanceAndAbsenceProcessing()
                                    {
                                        EmployeeId = employee.Id,
                                        DepartmentId = employee.DepartmentsId,
                                        SectionId = employee.SectionsId,
                                        AttendanceStatusId = 10,
                                        Date = workDate.Date,
                                        FromTime = workEndTime,
                                        ToTime = lastFingerprint,
                                        TotalWorkMinutes = (int)workLate,
                                        MinutesOfLate = 0,
                                        periodId = sT.PeriodId,
                                        permenenceId = sT.PermanenceModelsId,
                                        IsProcssessed = false,
                                        IsProcssessedBefore = false
                                    };
                                    attAbsences.Add(attAbsence2);

                                }
                            }
                            return attAbsences;
                        }
                        else if (isBetweenStartAndAllowanceTime == 3 && isBetweenEndtAndAllowanceTime == 5)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 12,
                                Date = workDate.Date,
                                FromTime = workStartTime,
                                ToTime = firstFingerprint,
                                TotalWorkMinutes = fTET,
                                MinutesOfLate = lateMinutes,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            return attAbsences;
                        }
                        else if (isBetweenStartAndAllowanceTime == 3 && isBetweenEndtAndAllowanceTime == 6)
                        {
                            var v = lateMinutes + earlyDeparture;
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,

                                AttendanceStatusId = 13,
                                Date = workDate.Date,
                                FromTime = firstFingerprint,
                                ToTime = lastFingerprint,
                                TotalWorkMinutes = workDuration,
                                MinutesOfLate = v,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            return attAbsences;
                        }
                    }
                    else
                    {
                        if (workStartTime < firstFingerprint && workEndTime == lastFingerprint)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 12,
                                Date = workDate.Date,
                                FromTime = workStartTime,
                                ToTime = firstFingerprint,
                                TotalWorkMinutes = workDuration,
                                MinutesOfLate = lateMinutes,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            return attAbsences;
                        }
                        else if (workStartTime == firstFingerprint && workEndTime < lastFingerprint)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 1,
                                Date = workDate.Date,
                                FromTime = workStartTime,
                                ToTime = workEndTime,
                                TotalWorkMinutes = period.Muinutes,
                                MinutesOfLate = 0,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            if (workLate > 10)
                            {
                                AttendanceAndAbsenceProcessing attAbsence2 = new AttendanceAndAbsenceProcessing()
                                {
                                    EmployeeId = employee.Id,
                                    DepartmentId = employee.DepartmentsId,
                                    SectionId = employee.SectionsId,
                                    AttendanceStatusId = 10,
                                    Date = workDate.Date,
                                    FromTime = workEndTime,
                                    ToTime = lastFingerprint,
                                    TotalWorkMinutes = (int)workLate,
                                    MinutesOfLate = 0,
                                    periodId = sT.PeriodId,
                                    permenenceId = sT.PermanenceModelsId,
                                    IsProcssessed = false,
                                    IsProcssessedBefore = false
                                };
                                attAbsences.Add(attAbsence2);
                            }
                            return attAbsences;
                        }
                        else if (workStartTime < firstFingerprint && workEndTime < lastFingerprint)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 12,
                                Date = workDate.Date,
                                FromTime = workStartTime,
                                ToTime = firstFingerprint,
                                TotalWorkMinutes = fTET,
                                MinutesOfLate = lateMinutes,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            if (lateMinutes < workLate)
                            {
                                if (workLate >= 10)
                                {

                                    AttendanceAndAbsenceProcessing attAbsence2 = new AttendanceAndAbsenceProcessing()
                                    {
                                        EmployeeId = employee.Id,
                                        DepartmentId = employee.DepartmentsId,
                                        SectionId = employee.SectionsId,
                                        AttendanceStatusId = 10,
                                        Date = workDate.Date,
                                        FromTime = workEndTime,
                                        ToTime = lastFingerprint,
                                        TotalWorkMinutes = (int)workLate,
                                        MinutesOfLate = 0,
                                        periodId = sT.PeriodId,
                                        permenenceId = sT.PermanenceModelsId,
                                        IsProcssessed = false,
                                        IsProcssessedBefore = false
                                    };
                                    attAbsences.Add(attAbsence2);
                                }
                            }
                            return attAbsences;

                        }
                        else if (workStartTime == firstFingerprint && workEndTime > lastFingerprint)
                        {
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 11,
                                Date = workDate.Date,
                                FromTime = workStartTime,
                                ToTime = lastFingerprint,
                                TotalWorkMinutes = workDuration,
                                MinutesOfLate = earlyDeparture,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            return attAbsences;
                        }
                        else if (workStartTime < firstFingerprint && workEndTime > lastFingerprint)
                        {
                            var m = earlyDeparture + lateMinutes;
                            AttendanceAndAbsenceProcessing attAbsence = new AttendanceAndAbsenceProcessing()
                            {
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentsId,
                                SectionId = employee.SectionsId,
                                AttendanceStatusId = 13,
                                Date = workDate.Date,
                                FromTime = firstFingerprint,
                                ToTime = lastFingerprint,
                                TotalWorkMinutes = workDuration,
                                MinutesOfLate = m,
                                periodId = sT.PeriodId,
                                permenenceId = sT.PermanenceModelsId,
                                IsProcssessed = false,
                                IsProcssessedBefore = false
                            };
                            attAbsences.Add(attAbsence);
                            return attAbsences;
                        }
                    }



                    //return new AttendanceSummary
                    //{
                    //    WorkDuration = workDuration,
                    //    LateMinutes = lateMinutes,
                    //    IsLate = isLate,
                    //    EarlyDepartureMinutes = earlyDepartureMinutes,
                    //    LeftEarly = leftEarly
                    //};
                }
            }
            return null;


        }


        private int CalculateWorkDuration(MachineInfo first, MachineInfo last)
        {
            if (first == null || last == null)
            {
                return 0; // Handle incomplete records
            }
            var firstTime = new TimeSpan(first.TimeOnlyRecord.Value.Hour, first.TimeOnlyRecord.Value.Minute, 0);
            var lastTime = new TimeSpan(last.TimeOnlyRecord.Value.Hour, last.TimeOnlyRecord.Value.Minute, 0);

            TimeSpan workDuration = lastTime.Subtract(firstTime);
            return (int)workDuration.TotalMinutes;
        }
        private int CalculateWorkDuration2(TimeSpan? first, MachineInfo last)
        {
            if (first == null || last == null)
            {
                return 0; // Handle incomplete records
            }
            var lastTime = new TimeSpan(last.TimeOnlyRecord.Value.Hour, last.TimeOnlyRecord.Value.Minute, 0);

            TimeSpan workDuration = lastTime.Subtract(first.Value);
            return (int)workDuration.TotalMinutes;
        }
        private int CalculateWorkDuration3(MachineInfo first, TimeSpan? last)
        {
            if (first == null || last == null)
            {
                return 0; // Handle incomplete records
            }
            var firstTime = new TimeSpan(first.TimeOnlyRecord.Value.Hour, first.TimeOnlyRecord.Value.Minute, 0);

            TimeSpan workDuration = last.Value - firstTime;
            return (int)workDuration.TotalMinutes;
        }
        private int CalculateLateMinutes(MachineInfo first, TimeSpan? workStartTime)
        {
            if (first == null)
            {
                return 0; // Handle no first fingerprint
            }

            // Your logic using workStartTime here
            var firstTime = new TimeSpan(first.TimeOnlyRecord.Value.Hour, first.TimeOnlyRecord.Value.Minute, 0);
            TimeSpan actualWorkStartTime = workStartTime ?? TimeSpan.Zero; // If workStartTime is null, use TimeSpan.Zero
            var lateTime = firstTime - actualWorkStartTime;
            return lateTime.Ticks > 0 ? (int)lateTime.TotalMinutes : 0;
        }

        private bool IsEmployeeLate(int lateMinutes, int? allowanceForLateAttendance)
        {
            return lateMinutes > allowanceForLateAttendance;
        }

        private int CalculateEarlyDepartureMinutes(MachineInfo last, TimeSpan? workEndTime)
        {
            if (last == null)
            {
                return 0; // Handle no last fingerprint
            }
            var lastTime = new TimeSpan(last.TimeOnlyRecord.Value.Hour, last.TimeOnlyRecord.Value.Minute, 0);

            var earlyDepartureTime = workEndTime - lastTime;
            return earlyDepartureTime?.Ticks > 0 ? (int)earlyDepartureTime.Value.TotalMinutes : 0;
        }

        private bool IsEmployeeLeftEarly(int earlyDepartureMinutes, int? earlyDeparturePermission)
        {
            return earlyDepartureMinutes > earlyDeparturePermission;
        }

        // Implement logic for retrieving work start time based on work date and specific requirements (holidays, shifts)
        private DateTime GetWorkStartTime(DateTime workDate)
        {
            // Implement logic based on your specific rules and data access
            return new DateTime(workDate.Year, workDate.Month, workDate.Day, workDate.Hour, workDate.Minute, workDate.Second); // Sample 9:00 AM
        }
        private DateTime GetWorkEndTime(DateTime workDate)
        {
            // Implement logic based on your specific rules and data access
            return new DateTime(workDate.Year, workDate.Month, workDate.Day, workDate.Hour, workDate.Minute, workDate.Second); // Sample 9:00 AM
        }
        private double CalculateAdditionalTimeInMinutes(TimeSpan fingerprintTime, TimeSpan? workEndTime)
        {
            if (workEndTime.HasValue)
            {
                TimeSpan additionalTime = fingerprintTime - workEndTime.Value;
                return additionalTime.TotalMinutes;
            }
            return 0; // If workEndTime is null, return zero additional time
        }
        private int IsWithinStartAndAllowanceTime(TimeSpan fingerprintTime, TimeSpan? workStartTime, int? allowanceForLateAttendance)
        {
            TimeSpan allowanceTime = TimeSpan.FromMinutes(allowanceForLateAttendance ?? 0);
            if (fingerprintTime < workStartTime)
            {
                return 1; // Before work start time
            }

            var allowedLateTime = (workStartTime ?? TimeSpan.Zero).Add(allowanceTime);
            if (fingerprintTime <= allowedLateTime)
            {
                return 2; // Between work start and allowance time
            }
            return 3; // Between work start and allowance time
        }
        private int IsWithinEndAndAllowanceTime(TimeSpan fingerprintTime, TimeSpan? workEndTime, int? allowanceForEarlyDeparture)
        {
            TimeSpan allowanceTime = TimeSpan.FromMinutes(allowanceForEarlyDeparture ?? 0);

            if (fingerprintTime > workEndTime)
            {
                return 4; // After work end time
            }

            var allowedEarlyDepartureTime = (workEndTime ?? TimeSpan.Zero).Subtract(allowanceTime);
            if (fingerprintTime >= allowedEarlyDepartureTime)
            {
                return 5; // Between work end and allowance time
            }
            return 6; // not Between work end and allowance time
        }
        private bool IsWeekend(DateTime date, StaffTime staffTime)
        {
            var sat = System.DayOfWeek.Saturday;
            var sun = System.DayOfWeek.Sunday;
            var mon = System.DayOfWeek.Monday;
            var tues = System.DayOfWeek.Tuesday;
            var wedn = System.DayOfWeek.Wednesday;
            var thur = System.DayOfWeek.Thursday;
            var fri = System.DayOfWeek.Friday;


            if (staffTime.Periods.Saturday == false)
            {
                if (date.DayOfWeek == sat)
                {
                    return true;
                }
                
            }
            if (staffTime.Periods.SunDay == false)
            {
                if (date.DayOfWeek == sun)
                {
                    return true;
                }
               
            }
            if (staffTime.Periods.Monday == false)
            {
                if (date.DayOfWeek == mon)
                {
                    return true;
                }
                
            }
            if (staffTime.Periods.Tuesday == false)
            {
                if (date.DayOfWeek == tues)
                {
                    return true;
                }
               
            }
            if (staffTime.Periods.Wednesday == false)
            {
                if (date.DayOfWeek == wedn)
                {
                    return true;
                }
             
            }
            if (staffTime.Periods.Thursday == false)
            {
                if (date.DayOfWeek == thur)
                {
                    return true;
                }
                
            }
            if (staffTime.Periods.Friday == false)
            {
                if (date.DayOfWeek == fri)
                {
                    return true;
                }
                
            }
            return false;
            // Check if the day is a weekend (Saturday or Sunday) or if the day is not checked in the staffTime record
            //return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || !staffTime.DayChecked;
        }
        public bool CheckHoliday(int employee, DateTime date)
        {
            var holiday = _context.StaffVacations.FirstOrDefault(x => x.EmployeeId == employee);
            if (holiday == null)
            {
                return false;
            }
            else
            {
                if (holiday.IsConnected == false)
                {
                    if (holiday.FromDate == date)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (holiday.FromDate <= date && holiday.ToDate >= date)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public bool CheckOfficialVacations(DateOnly date)
        {
            // Assuming holiday.FromDate is the start date of the holiday and holiday.ToDate is the end date of the holiday
            // Check if the given date falls within the holiday period
            if (_context.officialVacations.Any(holiday => holiday.FromDate <= date && holiday.ToDate >= date))
            {
                return true; // Employee has a holiday on the given date
            }

            return false; // Employee does not have a holiday on the given date
        }


    }


}



