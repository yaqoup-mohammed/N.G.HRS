using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.ViolationsAndPenaltiesAffairs.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;


namespace N.G.HRS.Areas.ViolationsAndPenaltiesAffairs.Controllers
{
    [Area("ViolationsAndPenaltiesAffairs")]
    public class EmployeeViolationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<EmployeeViolations> _EmployeeViolationsRepository;

        public EmployeeViolationsController(AppDbContext context, IRepository<EmployeeViolations> EmployeeViolationsRepository)
        {
            _context = context;
            _EmployeeViolationsRepository = EmployeeViolationsRepository;
        }



        // GET: ViolationsAndPenaltiesAffairs/EmployeeViolations
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var employeeViolations = await _context.EmployeeViolations
                .Include(e => e.Employee)
                .Include(e => e.Penalties)
                .Include(e => e.Violations)
                .ToListAsync();
            return View(employeeViolations);
        }

        // GET: ViolationsAndPenaltiesAffairs/EmployeeViolations/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeViolations = await _context.EmployeeViolations
                .Include(e => e.Employee)
                .Include(e => e.Penalties)
                .Include(e => e.Violations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeViolations == null)
            {
                return NotFound();
            }

            return View(employeeViolations);
        }


        // GET: ViolationsAndPenaltiesAffairs/EmployeeViolations/Create
        [Authorize(Policy = "AddPolicy")]


        public async Task<IActionResult> Create(int? id)
        {
            if (id != null)
            {
                var employeeViolations = await _EmployeeViolationsRepository.GetByIdAsync(id);
                if (employeeViolations == null)
                {
                    return NotFound();
                }
                PopulateDropDownLists();
                return View(employeeViolations);
            }
            else
            {
                PopulateDropDownLists();
                return View();
            }



        
        }

        // POST: ViolationsAndPenaltiesAffairs/EmployeeViolations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create(int?id, EmployeeViolations employeeViolations)
        {
            if (id == null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        await _EmployeeViolationsRepository.AddAsync(employeeViolations);
                        TempData["Success"] = "تم الحفظ بنجاح";
                        return RedirectToAction(nameof(Index));

                    }
                    catch(Exception ex)
                    {
                        TempData["SystemError"] = ex.Message;
                        return View(employeeViolations);
                    }
                }

                PopulateDropDownLists();
                TempData["Error"] = "حدث خطأ ما قد تكون البيانات خاطئة تأكد من صحة البيانات ثم  حاول مرة اخرى";
                return View(employeeViolations);
            }
            else
            {
                if (id != employeeViolations.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _EmployeeViolationsRepository.UpdateAsync(employeeViolations);
                        TempData["Success"] = "تم التعديل بنجاح";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeViolationsExists(employeeViolations.Id))
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
                PopulateDropDownLists();
                return View(employeeViolations);
            }
        }

        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeViolations = await _context.EmployeeViolations
                .Include(e => e.Employee)
                .Include(e => e.Penalties)
                .Include(e => e.Violations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeViolations == null)
            {
                return NotFound();
            }

            return View(employeeViolations);
        }

        // POST: ViolationsAndPenaltiesAffairs/EmployeeViolations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]



        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeViolations = await _context.EmployeeViolations.FindAsync(id);
            if (employeeViolations != null)
            {
                _context.EmployeeViolations.Remove(employeeViolations);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Method to check if EmployeeViolation exists
        private bool EmployeeViolationsExists(int id)
        {
            return _context.EmployeeViolations.Any(e => e.Id == id);

        }

        public IActionResult EmployeeViolations(int id)
        {
            var pant = _context.Penalties.Find(id);
            if (pant != null)
            {
                return Ok(pant);
            }
            else
            {
                return NotFound();

            }
        }
        //Method to populate dropdown lists
        private void PopulateDropDownLists()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["ViolationsId"] = new SelectList(_context.Violations, "Id", "ViolationsName");
            ViewData["PenaltiesId"] = new SelectList(_context.Penalties, "Id", "PenaltiesName");
        }
       
    }

}

