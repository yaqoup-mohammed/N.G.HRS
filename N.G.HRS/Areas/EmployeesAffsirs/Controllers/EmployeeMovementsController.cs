using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.EmployeesAffsirs.Controllers
{
    [Area("EmployeesAffsirs")]
    public class EmployeeMovementsController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeMovementsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EmployeesAffsirs/EmployeeMovements
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmployeeMovements.Include(e => e.Employee).Include(e => e.jopdescription);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EmployeesAffsirs/EmployeeMovements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeMovements = await _context.EmployeeMovements
                .Include(e => e.Employee)
                .Include(e => e.jopdescription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeMovements == null)
            {
                return NotFound();
            }

            return View(employeeMovements);
        }

        // GET: EmployeesAffsirs/EmployeeMovements/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["jopdescriptionId"] = new SelectList(_context.JobDescription, "Id", "Authorities");
            return View();
        }

        // POST: EmployeesAffsirs/EmployeeMovements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,DateDown,Note,EmployeeId,jopdescriptionId,CurrentJop,LastJop")] EmployeeMovements employeeMovements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeMovements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeMovements.EmployeeId);
            ViewData["jopdescriptionId"] = new SelectList(_context.JobDescription, "Id", "Authorities", employeeMovements.jopdescriptionId);
            return View(employeeMovements);
        }

        // GET: EmployeesAffsirs/EmployeeMovements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeMovements = await _context.EmployeeMovements.FindAsync(id);
            if (employeeMovements == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeMovements.EmployeeId);
            ViewData["jopdescriptionId"] = new SelectList(_context.JobDescription, "Id", "Authorities", employeeMovements.jopdescriptionId);
            return View(employeeMovements);
        }

        // POST: EmployeesAffsirs/EmployeeMovements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,DateDown,Note,EmployeeId,jopdescriptionId,CurrentJop,LastJop")] EmployeeMovements employeeMovements)
        {
            if (id != employeeMovements.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeMovements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeMovementsExists(employeeMovements.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeMovements.EmployeeId);
            ViewData["jopdescriptionId"] = new SelectList(_context.JobDescription, "Id", "Authorities", employeeMovements.jopdescriptionId);
            return View(employeeMovements);
        }

        // GET: EmployeesAffsirs/EmployeeMovements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeMovements = await _context.EmployeeMovements
                .Include(e => e.Employee)
                .Include(e => e.jopdescription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeMovements == null)
            {
                return NotFound();
            }

            return View(employeeMovements);
        }

        // POST: EmployeesAffsirs/EmployeeMovements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeMovements = await _context.EmployeeMovements.FindAsync(id);
            if (employeeMovements != null)
            {
                _context.EmployeeMovements.Remove(employeeMovements);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeMovementsExists(int id)
        {
            return _context.EmployeeMovements.Any(e => e.Id == id);
        }
    }
}
