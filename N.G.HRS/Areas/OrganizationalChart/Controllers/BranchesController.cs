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
    public class BranchesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Branches> _branchesRepository;

        public BranchesController(AppDbContext context, IRepository<Branches> branchesRepository)
        {
            _context = context;
            _branchesRepository = branchesRepository;
        }

        // GET: OrganizationalChart/Branches
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.branches.Include(b => b.Company).Include(b => b.Country).Include(b => b.Directorate).Include(b => b.Governorate);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/Branches/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branches = await _context.branches
                .Include(b => b.Company)
                .Include(b => b.Country)
                .Include(b => b.Directorate)
                .Include(b => b.Governorate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branches == null)
            {
                return NotFound();
            }

            return View(branches);
        }

        // GET: OrganizationalChart/Branches/Create
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
            return View();
        }

        // POST: OrganizationalChart/Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,BranchesName,BranchesAdress,BranchesPhone,BranchesEmail,Notes,CompanyId,CountryId,GovernorateId,DirectorateId")] Branches branches)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await PopulateDropdownListsAsync();
                    await _branchesRepository.AddAsync(branches);
                    TempData["Success"] = "تمت العملية بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(branches);
                }
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

            return View(branches);
        }

        // GET: OrganizationalChart/Branches/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();
            var branches = await _branchesRepository.GetByIdAsync(id);
            if (branches == null)
            {
                return NotFound();
            }

            return View(branches);
        }

        // POST: OrganizationalChart/Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchesName,BranchesAdress,BranchesPhone,BranchesEmail,Notes,CompanyId,CountryId,GovernorateId,DirectorateId")] Branches branches)
        {
            if (id != branches.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();
                try
                {
                    await _branchesRepository.UpdateAsync(branches);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchesExists(branches.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

                return RedirectToAction(nameof(Index));
            }

            return View(branches);
        }

        // GET: OrganizationalChart/Branches/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branches = await _context.branches
                .Include(b => b.Company)
                .Include(b => b.Country)
                .Include(b => b.Directorate)
                .Include(b => b.Governorate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branches == null)
            {
                return NotFound();
            }

            return View(branches);
        }

        // POST: OrganizationalChart/Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branches = await _branchesRepository.GetByIdAsync(id);
            if (branches != null)
            {
               await _branchesRepository.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BranchesExists(int id)
        {
            return _context.branches.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {
            var country = await _context.country.ToListAsync();
            ViewData["country"] = new SelectList(country, "Id", "Name");
            //====================================================
            var directorates = await _context.directorates.ToListAsync();
            ViewData["directorates"] = new SelectList(directorates, "Id", "Name");
            //====================================================
            var governorates = await _context.governorates.ToListAsync();
            ViewData["governorates"] = new SelectList(governorates, "Id", "Name");
            //====================================================
            var company = await _context.company.ToListAsync();
            ViewData["company"] = new SelectList(company, "Id", "CompanyName");
            //====================================================
        }
    }
}
