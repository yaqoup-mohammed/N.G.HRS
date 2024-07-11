using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.EmployeesAffsirs.Controllers
{
    [Area("EmployeesAffsirs")]
    public class PermitsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Permits> _Permits;

        public PermitsController(AppDbContext context, IRepository<Permits> PermitsRepository)
        {
            _context = context;
            _Permits = PermitsRepository;
        }

        // GET: EmployeesAffsirs/Permits
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Permits.Include(p => p.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EmployeesAffsirs/Permits/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permits = await _context.Permits
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permits == null)
            {
                return NotFound();
            }

            return View(permits);
        }

        // GET: EmployeesAffsirs/Permits/Create
        [Authorize(Policy = "AdminPolicy")]

        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
                return View();
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var permits = await _Permits.GetByIdAsync(id);
                if (permits == null)
                {
                    return NotFound();
                }
                ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", permits.EmployeeId);
                return View(permits);
            }
        }

        // POST: EmployeesAffsirs/Permits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create(int? id, Permits permits)
        {
            if (id == null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        await _Permits.AddAsync(permits);
                        TempData["Success"] = "تمت الاضافة بنجاح";
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", permits.EmployeeId);
                    TempData["Error"] = "خطئ في البيانات!!";
                return View(permits);
                }
                catch (Exception ex)
                {

                    TempData["Error"] = ex.Message;
                    return View(permits);
                }
            }
            else
            {
                if (id != permits.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _Permits.UpdateAsync(permits);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PermitsExists(permits.Id))
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
                ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", permits.EmployeeId);
                return View(permits);
            }
        }


        // GET: EmployeesAffsirs/Permits/Delete/5
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permits = await _context.Permits
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permits == null)
            {
                return NotFound();
            }

            return View(permits);
        }

        // POST: EmployeesAffsirs/Permits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permits = await _Permits.GetByIdAsync(id);
            if (permits != null)
            {
                await _Permits.DeleteAsync(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermitsExists(int id)
        {
            return _context.Permits.Any(e => e.Id == id);
        }



        public async Task<IActionResult> Print(int id)
        {
            var item = await _context.Permits
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }


    }
}
