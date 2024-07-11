using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    public class AutomaticallyApprovedAdd_onController : Controller
    {
        private readonly AppDbContext _context;

        public AutomaticallyApprovedAdd_onController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PayRoll/AutomaticallyApprovedAdd_on
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AutomaticallyApprovedAdd_on.Include(a => a.Employee).Include(a => a.Sections);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PayRoll/AutomaticallyApprovedAdd_on/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automaticallyApprovedAdd_on = await _context.AutomaticallyApprovedAdd_on
                .Include(a => a.Employee)
                .Include(a => a.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (automaticallyApprovedAdd_on == null)
            {
                return NotFound();
            }

            return View(automaticallyApprovedAdd_on);
        }

        // GET: PayRoll/AutomaticallyApprovedAdd_on/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName");
            return View();
        }

        // POST: PayRoll/AutomaticallyApprovedAdd_on/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("Id,SectionsId,EmployeeId,Date,FromDate,ToDate,FromTime,ToTime,Hours,Minutes")] AutomaticallyApprovedAdd_on automaticallyApprovedAdd_on)
        {
            if (ModelState.IsValid)
            {
                _context.Add(automaticallyApprovedAdd_on);
                await _context.SaveChangesAsync();
                TempData["success"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", automaticallyApprovedAdd_on.EmployeeId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", automaticallyApprovedAdd_on.SectionsId);
            return View(automaticallyApprovedAdd_on);
        }

        // GET: PayRoll/AutomaticallyApprovedAdd_on/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automaticallyApprovedAdd_on = await _context.AutomaticallyApprovedAdd_on.FindAsync(id);
            if (automaticallyApprovedAdd_on == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", automaticallyApprovedAdd_on.EmployeeId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", automaticallyApprovedAdd_on.SectionsId);
            return View(automaticallyApprovedAdd_on);
        }

        // POST: PayRoll/AutomaticallyApprovedAdd_on/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,SectionsId,EmployeeId,Date,FromDate,ToDate,FromTime,ToTime,Hours,Minutes")] AutomaticallyApprovedAdd_on automaticallyApprovedAdd_on)
        {
            if (id != automaticallyApprovedAdd_on.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(automaticallyApprovedAdd_on);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutomaticallyApprovedAdd_onExists(automaticallyApprovedAdd_on.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", automaticallyApprovedAdd_on.EmployeeId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", automaticallyApprovedAdd_on.SectionsId);
            return View(automaticallyApprovedAdd_on);
        }

        // GET: PayRoll/AutomaticallyApprovedAdd_on/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automaticallyApprovedAdd_on = await _context.AutomaticallyApprovedAdd_on
                .Include(a => a.Employee)
                .Include(a => a.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (automaticallyApprovedAdd_on == null)
            {
                return NotFound();
            }

            return View(automaticallyApprovedAdd_on);
        }

        // POST: PayRoll/AutomaticallyApprovedAdd_on/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var automaticallyApprovedAdd_on = await _context.AutomaticallyApprovedAdd_on.FindAsync(id);
            if (automaticallyApprovedAdd_on != null)
            {
                _context.AutomaticallyApprovedAdd_on.Remove(automaticallyApprovedAdd_on);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutomaticallyApprovedAdd_onExists(int id)
        {
            return _context.AutomaticallyApprovedAdd_on.Any(e => e.Id == id);
        }
    }
}
