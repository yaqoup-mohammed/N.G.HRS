using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.linkingEmployeesToShiftPeriods.Include(l => l.Departments).Include(l => l.Employee).Include(l => l.Periods).Include(l => l.PermanenceModels).Include(l => l.Sections);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Details/5
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
        public IActionResult Create()
        {
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id");
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName");
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName");
            return View();
        }

        // POST: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( LinkingEmployeesToShiftPeriods link)
        {
            if (ModelState.IsValid)
            {
                if(link.DateOfStartWork > link.DateOfEndWork)
                {
                    ViewData["error"] = "تاريخ النهاية يجب ان يكون اكبر من تاريخ الانتهاء!!";
                    return View(link);
                }
                    _context.Add(link);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                

            }
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration", link.DepartmentsId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", link.EmployeeId);
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id", link.PeriodsId);
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", link.PermanenceModelsId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", link.SectionsId);
            return View(link);
        }

        // GET: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Edit/5
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
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration", linkingEmployeesToShiftPeriods.DepartmentsId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", linkingEmployeesToShiftPeriods.EmployeeId);
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id", linkingEmployeesToShiftPeriods.PeriodsId);
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", linkingEmployeesToShiftPeriods.PermanenceModelsId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", linkingEmployeesToShiftPeriods.SectionsId);
            return View(linkingEmployeesToShiftPeriods);
        }

        // POST: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration", linkingEmployeesToShiftPeriods.DepartmentsId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", linkingEmployeesToShiftPeriods.EmployeeId);
            ViewData["PeriodsId"] = new SelectList(_context.periods, "Id", "Id", linkingEmployeesToShiftPeriods.PeriodsId);
            ViewData["PermanenceModelsId"] = new SelectList(_context.permanenceModels, "Id", "PermanenceName", linkingEmployeesToShiftPeriods.PermanenceModelsId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", linkingEmployeesToShiftPeriods.SectionsId);
            return View(linkingEmployeesToShiftPeriods);
        }

        // GET: AttendanceAndDeparture/LinkingEmployeesToShiftPeriods/Delete/5
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
    }
}
