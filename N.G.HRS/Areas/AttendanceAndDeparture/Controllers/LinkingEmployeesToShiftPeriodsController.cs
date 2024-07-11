using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Controllers
{
    [Area("AttendanceAndDeparture")]
    public class LinkingEmployeesToShiftPeriodsController : Controller
    {
        private readonly AppDbContext _context;

        public LinkingEmployeesToShiftPeriodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.linkingEmployeesToShiftPeriods.Include(l => l.Departments).Include(l => l.Employee).Include(l => l.Periods).Include(l => l.PermanenceModels).Include(l => l.Sections);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkingEmployeesToShiftPeriods = await _context.linkingEmployeesToShiftPeriods
                .Include(l => l.Departments)
                .Include(l => l.Employee)
                .Include(l => l.Periods)
                .Include(l => l.PermanenceModels)
                .Include(l => l.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linkingEmployeesToShiftPeriods == null)
            {
                return NotFound();
            }

            return View(linkingEmployeesToShiftPeriods);
        }

        // GET: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Create
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();

            return View();
        }

        // POST: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create( LinkingEmployeesToShiftPeriods link)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (link.DateOfStartWork > link.DateOfEndWork)
                    {
                        TempData["Error"] = "تاريخ النهاية يجب ان يكون اكبر من تاريخ الانتهاء!!";
                        return View(link);
                    }
                    _context.Add(link);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return View(link);
                }



            }
            await PopulateDropdownListsAsync();

            return View(link);
        }

        // GET: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkingEmployeesToShiftPeriods = await _context.linkingEmployeesToShiftPeriods.FindAsync(id);
            if (linkingEmployeesToShiftPeriods == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            return View(linkingEmployeesToShiftPeriods);
        }

        // POST: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,DateOfStartWork,DateOfEndWork,DepartmentsId,SectionsId,EmployeeId,PermanenceModelsId,PeriodsId")] LinkingEmployeesToShiftPeriods linkingEmployeesToShiftPeriods)
        {
            if (id != linkingEmployeesToShiftPeriods.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await PopulateDropdownListsAsync();

                    _context.Update(linkingEmployeesToShiftPeriods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkingEmployeesToShiftPeriodsExists(linkingEmployeesToShiftPeriods.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(linkingEmployeesToShiftPeriods);
        }

        // GET: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Delete/5
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkingEmployeesToShiftPeriods = await _context.linkingEmployeesToShiftPeriods
                .Include(l => l.Departments)
                .Include(l => l.Employee)
                .Include(l => l.Periods)
                .Include(l => l.PermanenceModels)
                .Include(l => l.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linkingEmployeesToShiftPeriods == null)
            {
                return NotFound();
            }

            return View(linkingEmployeesToShiftPeriods);
        }

        // POST: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var linkingEmployeesToShiftPeriods = await _context.linkingEmployeesToShiftPeriods.FindAsync(id);
            if (linkingEmployeesToShiftPeriods != null)
            {
                _context.linkingEmployeesToShiftPeriods.Remove(linkingEmployeesToShiftPeriods);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinkingEmployeesToShiftPeriodsExists(int id)
        {
            return _context.linkingEmployeesToShiftPeriods.Any(e => e.Id == id);
        }

        private async Task PopulateDropdownListsAsync()
        {
            var department = await _context.Departments.ToListAsync();
            ViewData["DepartmentsId"] = new SelectList(department, "Id", "SubAdministration");
            //====================================================
            var section = await _context.Sections.ToListAsync();
            ViewData["SectionsId"] = new SelectList(section, "Id", "SectionsName");
            //=========================================================
            var employee = await _context.employee.ToListAsync();
            ViewData["EmployeeId"] = new SelectList(employee, "Id", "EmployeeName");
            //============================================================
            var permanance = await _context.permanenceModels.ToListAsync();
            ViewData["PermanenceModelsId"] = new SelectList(permanance, "Id", "PermanenceName");
            //============================================================
            var period = await _context.periods.ToListAsync();
            ViewData["PeriodsId"] = new SelectList(period, "Id", "PeriodsName");


        }
    }
}
