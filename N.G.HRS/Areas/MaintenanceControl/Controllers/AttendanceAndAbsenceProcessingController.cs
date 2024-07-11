using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.Date;
namespace N.G.HRS.Areas.MaintenanceControl.Controllers
{
    [Area("MaintenanceControl")]

    public class AttendanceAndAbsenceProcessingController : Controller
    {
        private readonly AppDbContext _context;

        public AttendanceAndAbsenceProcessingController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create(AttendanceAndAbsenceProcessing attendanceAndAbsenceProcessing)
        {
            if (ModelState.IsValid)
            {
                _context.AttendanceAndAbsenceProcessing.Add(attendanceAndAbsenceProcessing);
                await _context.SaveChangesAsync();
                TempData["success"] = "تمت العملية بنجاح";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}