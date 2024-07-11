using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;
using Microsoft.AspNetCore.Authorization;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Controllers
{
    [Area("PlanningAndJobDescription")]
    public class FunctionalCategoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<FunctionalCategories> _functionalCategoriesRepository;

        public FunctionalCategoriesController(AppDbContext context , IRepository<FunctionalCategories> functionalCategoriesRepository)
        {
            _context = context;
            _functionalCategoriesRepository = functionalCategoriesRepository;
        }

        // GET: PlanningAndJobDescription/FunctionalCategories
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.functionalCategories.ToListAsync());
        }

        // GET: PlanningAndJobDescription/FunctionalCategories/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalCategories = await _context.functionalCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionalCategories == null)
            {
                return NotFound();
            }

            return View(functionalCategories);
        }

        // GET: PlanningAndJobDescription/FunctionalCategories/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanningAndJobDescription/FunctionalCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,CategoriesName,Notes")] FunctionalCategories functionalCategories)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist =_context.functionalCategories.Any(x => x.CategoriesName == functionalCategories.CategoriesName);
                    if (!exist)
                    {
                        await _functionalCategoriesRepository.AddAsync(functionalCategories);
                        TempData["Success"] = "تمت العملية بنجاح";
                        return RedirectToAction(nameof(Index));
                    }
                    {
                        TempData["Error"] = "هذه الفئة موجودة بالفعل";
                        return View(functionalCategories);
                    }
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(functionalCategories);
                }

                //return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

            return View(functionalCategories);
        }

        // GET: PlanningAndJobDescription/FunctionalCategories/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalCategories = await _functionalCategoriesRepository.GetByIdAsync(id);
            if (functionalCategories == null)
            {
                return NotFound();
            }
            return View(functionalCategories);
        }

        // POST: PlanningAndJobDescription/FunctionalCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoriesName,Notes")] FunctionalCategories functionalCategories)
        {
            if (id != functionalCategories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _functionalCategoriesRepository.UpdateAsync(functionalCategories);
                    TempData["Success"] = "تمت تعديل  بنجاح";


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionalCategoriesExists(functionalCategories.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(functionalCategories);
                //return RedirectToAction(nameof(Index));
            }
            return View(functionalCategories);
        }

        // GET: PlanningAndJobDescription/FunctionalCategories/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalCategories = await _context.functionalCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionalCategories == null)
            {
                return NotFound();
            }

            return View(functionalCategories);
        }

        // POST: PlanningAndJobDescription/FunctionalCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var functionalCategories = await _functionalCategoriesRepository.GetByIdAsync(id);
            if (functionalCategories != null)
            {
                _context.functionalCategories.Remove(functionalCategories);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Create));

            //return RedirectToAction(nameof(Index));
        }

        private bool FunctionalCategoriesExists(int id)
        {
            return _context.functionalCategories.Any(e => e.Id == id);
        }
    }
}
