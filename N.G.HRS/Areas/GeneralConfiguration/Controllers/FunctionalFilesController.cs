using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class FunctionalFilesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<FunctionalFiles> _functionalFilesRepository;

        public FunctionalFilesController(AppDbContext context, IRepository<FunctionalFiles> functionalFilesRepository)
        {
            _context = context;
            _functionalFilesRepository = functionalFilesRepository;

        }

        // GET: GeneralConfiguration/FunctionalFiles
        [Authorize (Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.functionalFiles.ToListAsync());
        }

        // GET: GeneralConfiguration/FunctionalFiles/Details/5
        [Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalFiles = await _context.functionalFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionalFiles == null)
            {
                return NotFound();
            }

            return View(functionalFiles);
        }

        // GET: GeneralConfiguration/FunctionalFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/FunctionalFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,FileName,Notes")] FunctionalFiles functionalFiles)
        {
            if (ModelState.IsValid)
            {
              await  _functionalFilesRepository.AddAsync(functionalFiles);
                TempData[("Success")] = "تمت العملية بنجاح.";
                return RedirectToAction(nameof(Create));

                //return RedirectToAction(nameof(Index));
            }
            return View(functionalFiles);
        }

        // GET: GeneralConfiguration/FunctionalFiles/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalFiles = await _functionalFilesRepository. GetByIdAsync(id);
            if (functionalFiles == null)
            {
                return NotFound();
            }
            return View(functionalFiles);
        }

        // POST: GeneralConfiguration/FunctionalFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FileName,Notes")] FunctionalFiles functionalFiles)
        {
            if (id != functionalFiles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    await _functionalFilesRepository.UpdateAsync(functionalFiles);
                    TempData[("Success")] = "تم تعديل بنجاح.";
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionalFilesExists(functionalFiles.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return View(functionalFiles);
            }
            return View(functionalFiles);
        }

        // GET: GeneralConfiguration/FunctionalFiles/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalFiles = await _context.functionalFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionalFiles == null)
            {
                return NotFound();
            }

            return View(functionalFiles);
        }

        // POST: GeneralConfiguration/FunctionalFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var functionalFiles = await _functionalFilesRepository.GetByIdAsync(id);
            if (functionalFiles != null)
            {
                _context.functionalFiles.Remove(functionalFiles);
            }

            await _context.SaveChangesAsync();
            TempData [("Success")] = "تم الحذف بنجاح.";
            return RedirectToAction(nameof(Create));

            //return RedirectToAction(nameof(Index));
        }

        private bool FunctionalFilesExists(int id)
        {
            return _context.functionalFiles.Any(e => e.Id == id);
        }
    }
}
