using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.MaintenanceControl.Controllers
{
    [Area("MaintenanceControl")]
    public class AdditionalUnsupportedEmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public AdditionalUnsupportedEmployeesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: AdditionalUnsupportedEmployeesController
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AdditionalUnsupportedEmployees.Include(e => e.Employee);

            return View( await appDbContext.ToListAsync());
        }

        // GET: AdditionalUnsupportedEmployeesController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: AdditionalUnsupportedEmployeesController/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");

            return View();
        }

        // POST: AdditionalUnsupportedEmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdditionalUnsupportedEmployees Unsupported)
        {

            try
            {
                ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", Unsupported.EmployeeId);

                if (ModelState.IsValid)
                {
                    _context.Add(Unsupported);
                    _context.SaveChanges();
                    TempData["success"] = "تمت العملية بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                return View(Unsupported);
            }
            catch
            {
                return View();
            }
        }

        // GET: AdditionalUnsupportedEmployeesController/Edit/5
        //public IActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AdditionalUnsupportedEmployeesController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AdditionalUnsupportedEmployeesController/Delete/5
        //public IActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AdditionalUnsupportedEmployeesController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
