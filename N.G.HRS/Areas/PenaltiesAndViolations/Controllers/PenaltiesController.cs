using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PenaltiesAndViolations.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.PenaltiesAndViolations.Controllers
{
    [Area("PenaltiesAndViolations")]
    public class PenaltiesController : Controller
    {
        private readonly AppDbContext _context;
<<<<<<< HEAD
        private readonly IRepository<Penalties> _Penalties;

        public PenaltiesController(AppDbContext context, IRepository<Penalties> Penalties)
        {
            _context = context;
            _Penalties = Penalties;
=======
        private readonly IRepository<Penalties> _repository;

        public PenaltiesController(AppDbContext context, IRepository<Penalties> repository)
        {
            _context = context;
            _repository = repository;
>>>>>>> 66ea88f1417c4dca0add2df6a92ef28220b85e02
        }

        // GET: PenaltiesAndViolations/Penalties
        public async Task<IActionResult> Index()
        {
<<<<<<< HEAD
            return View(await _Penalties.GetAllAsync());
=======
            return View(await _context.Penalties.ToListAsync());
>>>>>>> 66ea88f1417c4dca0add2df6a92ef28220b85e02
        }

        // GET: PenaltiesAndViolations/Penalties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalties = await _context.Penalties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penalties == null)
            {
                return NotFound();
            }

            return View(penalties);
        }

        // GET: PenaltiesAndViolations/Penalties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PenaltiesAndViolations/Penalties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PenaltiesName,Deduction,DiscountFromWorkingHours,DeductionFromTheDailyWage,DeductionFromSalary,Value,Percent,Notes")] Penalties penalties)
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
                await _Penalties.AddAsync(penalties);
                TempData ["Success"] = "تمت العملية بنجاح";
=======
                await _repository.AddAsync(penalties);
                TempData ["Success"] = "تم الحفظ بنجاح";
>>>>>>> 66ea88f1417c4dca0add2df6a92ef28220b85e02
                return RedirectToAction(nameof(Index));
            }
            return View(penalties);
        }

        // GET: PenaltiesAndViolations/Penalties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

<<<<<<< HEAD
            var penalties = await _Penalties.GetByIdAsync(id);
=======
            var penalties = await _repository.GetByIdAsync(id);
>>>>>>> 66ea88f1417c4dca0add2df6a92ef28220b85e02
            if (penalties == null)
            {
                return NotFound();
            }
            return View(penalties);
        }

        // POST: PenaltiesAndViolations/Penalties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PenaltiesName,Deduction,DiscountFromWorkingHours,DeductionFromTheDailyWage,DeductionFromSalary,Value,Percent,Notes")] Penalties penalties)
        {
            if (id != penalties.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
<<<<<<< HEAD
                    await _Penalties.UpdateAsync(penalties);
=======
                  await  _repository.UpdateAsync(penalties);
                    TempData["Success"] = "تم التعديل بنجاح";
>>>>>>> 66ea88f1417c4dca0add2df6a92ef28220b85e02
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenaltiesExists(penalties.Id))
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
            return View(penalties);
        }

        // GET: PenaltiesAndViolations/Penalties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalties = await _context.Penalties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penalties == null)
            {
                return NotFound();
            }

            return View(penalties);
        }

        // POST: PenaltiesAndViolations/Penalties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
<<<<<<< HEAD
            var penalties = await _Penalties.GetByIdAsync(id);
            if (penalties != null)
            {
                 _context.Remove(penalties);
            }

=======
            var penalties = await _repository.GetByIdAsync(id);
            if (penalties != null)
            {
                _context.Penalties.Remove(penalties);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "تم الحذف بنجاح";
>>>>>>> 66ea88f1417c4dca0add2df6a92ef28220b85e02
            return RedirectToAction(nameof(Index));
        }

        private bool PenaltiesExists(int id)
        {
            return _context.Penalties.Any(e => e.Id == id);
        }
    }
}
