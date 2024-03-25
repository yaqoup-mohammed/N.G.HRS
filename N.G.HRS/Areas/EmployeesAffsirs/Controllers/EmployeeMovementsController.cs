using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.EmployeesAffsirs.Controllers
{
    [Area("EmployeesAffsirs")]
    public class EmployeeMovementsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<EmployeeMovements> _employeeMovementsRepository;

        public EmployeeMovementsController(AppDbContext context, IRepository<EmployeeMovements> employeeMovementsRepository)
        {
            _context = context;
            _employeeMovementsRepository = employeeMovementsRepository;

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
            ViewData["jopdescriptionId"] = new SelectList(_context.JobDescription, "Id", "JopName");
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
                

                //تحديث البيانات الجدول     
                var emp = _context.employee.Find(employeeMovements.EmployeeId);
                if (emp != null)
                {
                    emp.JobDescriptionId = employeeMovements.jopdescriptionId;
                    _context.employee.Update(emp);
                }
                //تحديث البيانات الجدول     


                await _employeeMovementsRepository.AddAsync(employeeMovements);
                TempData ["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeMovements.EmployeeId);
            ViewData["jopdescriptionId"] = new SelectList(_context.JobDescription, "Id", "JopName", employeeMovements.jopdescriptionId);
            return View(employeeMovements);
        }

        // GET: EmployeesAffsirs/EmployeeMovements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeMovements = await _employeeMovementsRepository.GetByIdAsync(id);
            if (employeeMovements == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeMovements.EmployeeId);
            ViewData["jopdescriptionId"] = new SelectList(_context.JobDescription, "Id", "JopName", employeeMovements.jopdescriptionId);
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
                    await   _employeeMovementsRepository.UpdateAsync(employeeMovements);
                    TempData ["Success"] = "تم التعديل  بنجاح";
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
            ViewData["jopdescriptionId"] = new SelectList(_context.JobDescription, "Id", "JopName", employeeMovements.jopdescriptionId);
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
            var employeeMovements = await _employeeMovementsRepository.GetByIdAsync(id);
            if (employeeMovements != null)
            {
                _context.EmployeeMovements.Remove(employeeMovements);
            }

            await _context.SaveChangesAsync();
            TempData ["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeMovementsExists(int id)
        {
            return _context.EmployeeMovements.Any(e => e.Id == id);
        }
    }
}
