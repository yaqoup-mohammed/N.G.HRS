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
    public class SectorsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Sectors> _sectorsRepository;

        public SectorsController(AppDbContext context, IRepository<Sectors> sectorsRepository)
        {
            _context = context;
            _sectorsRepository = sectorsRepository;
        }

        // GET: OrganizationalChart/Sectors
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.sectors.Include(s => s.Branches);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/Sectors/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectors = await _context.sectors
                .Include(s => s.Branches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectors == null)
            {
                return NotFound();
            }

            return View(sectors);
        }

        // GET: OrganizationalChart/Sectors/Create
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
            return View();
        }

        // POST: OrganizationalChart/Sectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("Id,SectorsName,Notes,BranchesId")] Sectors sectors)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await PopulateDropdownListsAsync();

                    await _sectorsRepository.AddAsync(sectors);
                    TempData["Success"] = "تمت العملية بنجاح!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(sectors);
                }
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";
            return View(sectors);
        }

        // GET: OrganizationalChart/Sectors/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var sectors = await _sectorsRepository.GetByIdAsync(id);
            if (sectors == null)
            {
                return NotFound();
            }
            return View(sectors);
        }

        // POST: OrganizationalChart/Sectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,SectorsName,Notes,BranchesId")] Sectors sectors)
        {
            if (id != sectors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();

                try
                {
                    await _sectorsRepository.UpdateAsync(sectors);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectorsExists(sectors.Id))
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
            return View(sectors);
        }

        // GET: OrganizationalChart/Sectors/Delete/5
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectors = await _context.sectors
                .Include(s => s.Branches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectors == null)
            {
                return NotFound();
            }

            return View(sectors);
        }

        // POST: OrganizationalChart/Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sectors = await _sectorsRepository.GetByIdAsync(id);
            if (sectors != null)
            {
                await _sectorsRepository.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SectorsExists(int id)
        {
            return _context.sectors.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {
            var branches = await _context.branches.ToListAsync();
            ViewData["branches"] = new SelectList(branches, "Id", "BranchesName");
            //====================================================
        }
    }
}
