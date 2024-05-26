using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.MaintenanceControl.ViewModels;
using N.G.HRS.Date;
using N.G.HRS.HRSelectList;

namespace N.G.HRS.Areas.MaintenanceControl.Controllers
{
    [Area("MaintenanceControl")]

    public class UplodeFingerPrintFromDeviceVMController : Controller
    {
        private readonly AppDbContext _context;
        public UplodeFingerPrintFromDeviceVMController(AppDbContext context)
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
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( UplodeFingerPrintFromDeviceVM deviceVM)
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
            ViewData["Department"] = new SelectList(department, "Id", "DepartmentName");
            var employee = await _context.employee.ToListAsync();
            ViewData["Employee"] = new SelectList(employee, "Id", "EmployeeName");
            var device= await _context.fingerprintDevices.ToListAsync();
            ViewData["Device"] = new SelectList(device, "Id", "DeviceName");

        }
    }
}
