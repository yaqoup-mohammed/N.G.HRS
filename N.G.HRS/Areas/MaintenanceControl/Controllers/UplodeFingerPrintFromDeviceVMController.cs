using BioMetrixCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.MaintenanceControl.ViewModels;
using N.G.HRS.Date;
using N.G.HRS.FingerPrintSetting;
using N.G.HRS.HRSelectList;
using System.Reflection.PortableExecutable;
using zkemkeeper;

namespace N.G.HRS.Areas.MaintenanceControl.Controllers
{
    [Area("MaintenanceControl")]

    public class UplodeFingerPrintFromDeviceVMController : Controller
    {
        //private static readonly Action<object, string> _deviceEventCallback;
        //private static readonly ZkemClient _zkemClient = new ZkemClient(_deviceEventCallback);
        //private readonly AppDbContext _context;

        //public UplodeFingerPrintFromDeviceVMController(AppDbContext context)
        //{
        //    _context = context;

        //}

        //static FingerPrintServeces serveces = new FingerPrintServeces(_zkemClient);


        //FingerPrintServeces serveces = new FingerPrintServeces( );
        private readonly AppDbContext _context;
        private bool IsConnected;
        //public ZkemClient objZkeeper;
        ZkemClient objZkeeper = new ZkemClient(RaiseDeviceEvent);

        DeviceManipulator manipulator = new DeviceManipulator();

        private bool isDeviceConnected = false;
        public bool IsDeviceConnected
        {
            get { return isDeviceConnected; }
            set
            {
                isDeviceConnected = value;
                if (isDeviceConnected)
                {
                    TempData["message"] = "تم الاتصال بنجاح";
                }
                else
                {
                    TempData["message"] = "تم قطع الاتصال بنجاح";
                    objZkeeper.Disconnect();

                }
            }
        }
        public UplodeFingerPrintFromDeviceVMController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            await PopulateDropdownListsAsync();
            return View();
        }
        [HttpGet]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();

            return View();
        }
        [HttpPost]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create(UplodeFingerPrintFromDeviceVM deviceVM)
        {
            await PopulateDropdownListsAsync();

            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public async Task<IActionResult> Index(UplodeFingerPrintFromDeviceVM deviceVM)
        //{
        //    await PopulateDropdownListsAsync();
        //    return View();
        //}

        private async Task PopulateDropdownListsAsync()
        {
            //-----------------------Sections---------------------------
            var Sections = await _context.Sections.ToListAsync();
            ViewData["Sections"] = new SelectList(Sections, "Id", "SectionsName");
            var department = await _context.Departments.ToListAsync();
            ViewData["Department"] = new SelectList(department, "Id", "SubAdministration");
            var employee = await _context.employee.ToListAsync();
            ViewData["Employee"] = new SelectList(employee, "Id", "EmployeeName");
            var device = await _context.fingerprintDevices.ToListAsync();
            ViewData["Device"] = new SelectList(device, "Id", "DevicesName");

        }

        //====================================================================
        public IActionResult GetEmployeeData(int id)
        {
            var employee = _context.employee.Include(x => x.Sections).Include(x => x.Departments).Where(x => x.Id == id).Select(x => new { ids = x.SectionsId, idsname = x.Sections.SectionsName, idd = x.DepartmentsId, iddname = x.Departments.SubAdministration }).FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }
            return Json(employee);
        }
        ////====================================================================
        private static void RaiseDeviceEvent(object sender, string actionType)
        {
            switch (actionType)
            {
                case UniversalStatic.acx_Disconnect:
                    {
                        //Raise Disconnected Event
                        throw new NotImplementedException(" الجهاز غير  متصل 😴");
                    }

                default:
                    break;
            }
        }        ////====================================================================





        //انتبة لا تمسحها!!!
        public IActionResult ClearLog(int deviceId)
        {
            var machinrNo = _context.fingerprintDevices.Where(x => x.Id == deviceId)
                                                                 .Select(x => new { x.DevicesNumber, x.IpAddress })
                                                                 .FirstOrDefault();

            if (machinrNo.DevicesNumber == 0) return NotFound();
            bool isConnected = objZkeeper.Connect_Net(machinrNo.IpAddress, 4370);

            if (isConnected)
            {
                objZkeeper.ClearGLog(machinrNo.DevicesNumber);
                return Json(1);
            }
            else
            {
                return Json(2);
            }

        }
        public IActionResult GetAllUserId(int deviceId)
        {
            // Connect to the device
            var machinrNo = _context.fingerprintDevices.Where(x => x.Id == deviceId)
                                                      .Select(x => new { x.DevicesNumber, x.IpAddress })
                                                      .FirstOrDefault();
            if (machinrNo.DevicesNumber == 0) return NotFound();
            bool isConnected = objZkeeper.Connect_Net(machinrNo.IpAddress, 4370);
            IsConnected = isConnected;
            if (isConnected)
            {
                ICollection<UserIDInfo> lstFingerPrintTemplates = manipulator.GetAllUserID(objZkeeper, machinrNo.DevicesNumber);
                if (lstFingerPrintTemplates != null && lstFingerPrintTemplates.Count > 0)
                {

                    return Json(lstFingerPrintTemplates);
                }
                else
                {
                    return Json(0);
                }
            }
            else
            {
                return Json(0);
            }
        }
        public async Task<IActionResult> GetAllUserInfo(int deviceId)
        {
            try
            {
                if (deviceId == 0) return Json(0);
                else
                {


                    // Connect to the device
                    var machinrNo = _context.fingerprintDevices.Where(x => x.Id == deviceId)
                                                              .Select(x => new { x.DevicesNumber, x.IpAddress })
                                                              .FirstOrDefault();
                    if (machinrNo.DevicesNumber == 0) return NotFound();
                    bool isConnected = objZkeeper.Connect_Net(machinrNo.IpAddress, 4370);
                    IsConnected = isConnected;
                    if (isConnected)
                    {
                        ICollection<UserInfo> lstFingerPrintTemplates = manipulator.GetAllUserInfo(objZkeeper, machinrNo.DevicesNumber);
                        ICollection<MachineInfo> lstMachineInfo = manipulator.GetLogData(objZkeeper, machinrNo.DevicesNumber);
                        var tolerancePeriod = TimeSpan.FromMinutes(5);
                        var processedAttendanceData = lstMachineInfo.Where(record => record.IndRegID != 0)
                            .GroupBy(record => new { record.IndRegID, record.DateOnlyRecord, record.TimeOnlyRecord })
                            .SelectMany(group =>
                            {
                                var sortedRecords = group.OrderBy(record => record.IndRegID).ThenBy(record => record.DateOnlyRecord).ThenBy(record => record.TimeOnlyRecord).ToList();

                                // تجاهل السجلات المتكررة في غضون 5 دقائق
                                for (int i = 1; i < sortedRecords.Count; i++)
                                {
                                    if (sortedRecords[i].TimeOnlyRecord - sortedRecords[i - 1].TimeOnlyRecord <= tolerancePeriod || sortedRecords[i].TimeOnlyRecord == sortedRecords[i - 1].TimeOnlyRecord)
                                    {
                                        sortedRecords.RemoveAt(i);
                                    }
                                }
                                return sortedRecords;
                            });
                        foreach (MachineInfo machine in processedAttendanceData)
                        {
                            if (machine.IndRegID != 0)
                            {
                                var employee = _context.employee
                                    .Where(x => x.EmployeeNumber == machine.IndRegID).Include(x => x.Departments).Include(x => x.Sections)
                                    .Select(x => new { emp = x.EmployeeName, dep = x.DepartmentsId, sec = x.SectionsId }).FirstOrDefault();
                                if (employee != null)
                                {
                                    DateTime dateTime = DateTime.Parse(machine.DateTimeRecord);
                                    MachineInfo attLog = new MachineInfo();
                                    attLog.IndRegID = machine.IndRegID;
                                    attLog.EmployeeName = employee.emp;
                                    attLog.DepartmentId = employee.dep;
                                    attLog.SectionId = employee.sec;
                                    attLog.DateTimeRecord = machine.DateTimeRecord;
                                    attLog.TimeOnlyRecord = new DateTime(2000, 01, 01, dateTime.Hour, dateTime.Minute, dateTime.Second);
                                    attLog.DateOnlyRecord = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
                                    attLog.State = "تحميل من جهاز البصمة";
                                    attLog.MachineNumber = machinrNo.DevicesNumber;
                               
                                    bool m = _context.MachineInfo.Any(x => x.IndRegID == machine.IndRegID && x.DateTimeRecord == machine.DateTimeRecord);
                                    if (m)
                                    {

                                        continue;
                                    }
                                    else
                                    {
                                        _context.MachineInfo.Add(attLog);
                                    }

                                }
                            }
                        }
                        await _context.SaveChangesAsync();

                        var lstFingerPrintTemplates2 = await _context.MachineInfo.Include(x => x.Section).Include(x => x.Department).Where(x => x.IsProcessed == false).OrderBy(x => x.IndRegID).ThenBy(x => x.DateOnlyRecord).ThenBy(x => x.TimeOnlyRecord)
                            .Select(x => new
                            {
                                eNumber = x.IndRegID,
                                eName = x.EmployeeName,
                                eDepartment = x.Department.SubAdministration,
                                eSection = x.Section.SectionsName,
                                eTime = x.TimeOnlyRecord,
                                eDate = x.DateOnlyRecord,
                                eState = x.State,
                                machine = x.MachineNumber
                            }).ToListAsync();
                        if (lstFingerPrintTemplates2 != null && lstFingerPrintTemplates2.Count > 0)
                        {
                            return Json(lstFingerPrintTemplates2);
                        }
                        else
                        {
                            return Json(1);
                        }
                    }

                }
                return Json("حدث خطئ اثناء العملية ! ...");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }


    }
}