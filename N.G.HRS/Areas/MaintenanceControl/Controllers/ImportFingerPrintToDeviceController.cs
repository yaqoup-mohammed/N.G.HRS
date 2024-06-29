using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Date;
using N.G.HRS.FingerPrintSetting;

namespace N.G.HRS.Areas.MaintenanceControl.Controllers
{
    [Area("MaintenanceControl")]
    public class ImportFingerPrintToDeviceController : Controller
    {
        //FingerPrintServeces serveces = new FingerPrintServeces();
        private readonly AppDbContext _context;
        public ImportFingerPrintToDeviceController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            await PopulateDropdownListsAsync();

            return View();
        }
        private async Task PopulateDropdownListsAsync()
        {
            //-----------------------Sections---------------------------
            
            var device = await _context.fingerprintDevices.ToListAsync();
            ViewData["Device"] = new SelectList(device, "Id", "DevicesName");

        }
    }
}
