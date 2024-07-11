using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.OrganizationalChart.Controllers
{
    [Area("OrganizationalChart")]
    public class SectionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Sections> _SectionsRepository;

        public SectionsController(AppDbContext context, IRepository<Sections> SectionsRepository)
        {
            _context = context;
            _SectionsRepository = SectionsRepository;
        }

        // GET: OrganizationalChart/Sections
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Sections.Include(s => s.Departments);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/Sections/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sections = await _context.Sections
                .Include(s => s.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sections == null)
            {
                return NotFound();
            }

            return View(sections);
        }

        // GET: OrganizationalChart/Sections/Create
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
                return View();
        }

        // POST: OrganizationalChart/Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,SectionsName,Notes,DepartmentsId")] Sections sections)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await PopulateDropdownListsAsync();
                    await _SectionsRepository.AddAsync(sections);
                    TempData["Success"] = "تمت العملية بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(sections);
                }

            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";
            return View(sections);
        }

        // GET: OrganizationalChart/Sections/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var sections = await _SectionsRepository.GetByIdAsync(id);
            if (sections == null)
            {
                return NotFound();
            }
            return View(sections);
        }

        // POST: OrganizationalChart/Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SectionsName,Notes,DepartmentsId")] Sections sections)
        {
            if (id != sections.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await PopulateDropdownListsAsync();
                    TempData["Success"] = "تمت العملية بنجاح";
                    await _SectionsRepository.UpdateAsync(sections);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionsExists(sections.Id))
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
            return View(sections);
        }

        // GET: OrganizationalChart/Sections/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sections = await _context.Sections
                .Include(s => s.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sections == null)
            {
                return NotFound();
            }

            return View(sections);
        }

        // POST: OrganizationalChart/Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sections = await _SectionsRepository.GetByIdAsync(id);
            if (sections != null)
            {
                await _SectionsRepository.DeleteAsync(id);
                TempData["Success"] = "تمت العملية بنجاح";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SectionsExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {
            var Departments = await _context.Departments.ToListAsync();
            ViewData["Departments"] = new SelectList(Departments, "Id", "SubAdministration");
            //====================================================
        }
    }
}
