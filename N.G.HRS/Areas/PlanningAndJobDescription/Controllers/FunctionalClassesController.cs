using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;
using Microsoft.AspNetCore.Authorization;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Controllers
{
    [Area("PlanningAndJobDescription")]
    public class FunctionalClassesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<FunctionalClass> _functionalClassesRepository;

        public FunctionalClassesController(AppDbContext context, IRepository<FunctionalClass> functionalClassesRepository)
        {
            _context = context;
            _functionalClassesRepository = functionalClassesRepository;
        }

        // GET: PlanningAndJobDescription/FunctionalClasses
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.functionalClasses.Include(f => f.Currency);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PlanningAndJobDescription/FunctionalClasses/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalClass = await _context.functionalClasses
                .Include(f => f.Currency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionalClass == null)
            {
                return NotFound();
            }

            return View(functionalClass);
        }

        // GET: PlanningAndJobDescription/FunctionalClasses/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["CurrencyId"] = new SelectList(_context.Set<Currency>(), "Id", "CurrencyCode");
            return View();
        }

        // POST: PlanningAndJobDescription/FunctionalClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,BasicSalary,Notes,CurrencyId")] FunctionalClass functionalClass)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist=_context.functionalClasses.Any(x => x.Name == functionalClass.Name);
                    if (!exist)
                    {
                        await _functionalClassesRepository.AddAsync(functionalClass);
                        TempData["success"] = "تم الحفظ بنجاح";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "الاسم موجود مسبقا";
                        return View(functionalClass);
                    }
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(functionalClass);
                }

                //return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

            ViewData["CurrencyId"] = new SelectList(_context.Set<Currency>(), "Id", "CurrencyCode", functionalClass.CurrencyId);
            return View(functionalClass);
        }

        // GET: PlanningAndJobDescription/FunctionalClasses/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalClass = await _functionalClassesRepository.GetByIdAsync(id);
            if (functionalClass == null)
            {
                return NotFound();
            }
            ViewData["CurrencyId"] = new SelectList(_context.Set<Currency>(), "Id", "CurrencyCode", functionalClass.CurrencyId);
            return View(functionalClass);
        }

        // POST: PlanningAndJobDescription/FunctionalClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BasicSalary,Notes,CurrencyId")] FunctionalClass functionalClass)
        {
            if (id != functionalClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _functionalClassesRepository.UpdateAsync(functionalClass);
                    TempData["success"] = "تم التعديل بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionalClassExists(functionalClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(functionalClass);
                //return RedirectToAction(nameof(Index));
            }
            ViewData["CurrencyId"] = new SelectList(_context.Set<Currency>(), "Id", "CurrencyCode", functionalClass.CurrencyId);
            return View(functionalClass);
        }

        // GET: PlanningAndJobDescription/FunctionalClasses/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalClass = await _context.functionalClasses
                .Include(f => f.Currency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionalClass == null)
            {
                return NotFound();
            }

            return View(functionalClass);
        }

        // POST: PlanningAndJobDescription/FunctionalClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var functionalClass = await _functionalClassesRepository.GetByIdAsync(id);
            if (functionalClass != null)
            {
                _context.functionalClasses.Remove(functionalClass);
            }

            await _context.SaveChangesAsync();
            TempData ["success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Create));

            //return RedirectToAction(nameof(Index));
        }

        private bool FunctionalClassExists(int id)
        {
            return _context.functionalClasses.Any(e => e.Id == id);
        }
    }
}
