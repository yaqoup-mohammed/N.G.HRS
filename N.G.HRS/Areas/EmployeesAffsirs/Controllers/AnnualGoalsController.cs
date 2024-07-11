using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Areas.ViolationsAndPenaltiesAffairs.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.EmployeesAffsirs.Controllers
{
    [Area("EmployeesAffsirs")]
    public class AnnualGoalsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<AnnualGoals> _annualGoalsRepository;

        public AnnualGoalsController(AppDbContext context, IRepository<AnnualGoals> annualGoalsRepository)
        {
            _context = context;
            _annualGoalsRepository = annualGoalsRepository;
        }

        // GET: EmployeesAffsirs/AnnualGoals
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AnnualGoals.Include(a => a.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EmployeesAffsirs/AnnualGoals/Details/5
        [ Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annualGoals = await _context.AnnualGoals
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annualGoals == null)
            {
                return NotFound();
            }

            return View(annualGoals);
        }

        // GET: EmployeesAffsirs/AnnualGoals/Create
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create(int? id)
        {
            if (id != null)
            {
                var annualGoals = await _annualGoalsRepository.GetByIdAsync(id);
                if (annualGoals == null)
                {
                    return NotFound();
                }
                PopulateDropDownLists();
                return View(annualGoals);
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
        public async Task<IActionResult> Create(int? id, AnnualGoals annualGoals )
        {
            if (id == null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await _annualGoalsRepository.AddAsync( annualGoals);
                        TempData["Success"] = "تم الحفظ بنجاح";
                        return RedirectToAction(nameof(Index));

                    }
                    catch (Exception ex)
                    {
                        TempData["SystemError"] = ex.Message;
                        return View(annualGoals);
                    }
                }

                PopulateDropDownLists();
                TempData["Error"] = "حدث خطأ ما قد تكون البيانات خاطئة تأكد من صحة البيانات ثم  حاول مرة اخرى";
                return View(annualGoals);
            }
            else
            {
                if (id != annualGoals.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _annualGoalsRepository.UpdateAsync(annualGoals);
                        TempData["Success"] = "تم التعديل بنجاح";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AnnualGoalsExists(annualGoals.Id))
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
                return View(annualGoals);
            }
        }




        // GET: EmployeesAffsirs/AnnualGoals/Delete/5
        [ Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annualGoals = await _context.AnnualGoals
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annualGoals == null)
            {
                return NotFound();
            }

            return View(annualGoals);
        }

        // POST: EmployeesAffsirs/AnnualGoals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var annualGoals = await _annualGoalsRepository.GetByIdAsync(id);
            if (annualGoals != null)
            {
                _context.AnnualGoals.Remove(annualGoals);
            }
             
            await _context.SaveChangesAsync();
            TempData ["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Create));
        }

        private bool AnnualGoalsExists(int id)
        {
            return _context.AnnualGoals.Any(e => e.Id == id);
        }

        private void PopulateDropDownLists()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
        }

    }

    
}
