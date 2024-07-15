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
    public class CompaniesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Company> _CompaniesRepository;

        public CompaniesController(AppDbContext context, IRepository<Company> CompaniesRepository)
        {
            _context = context;
            _CompaniesRepository = CompaniesRepository;
        }

        // GET: OrganizationalChart/Companies
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.company.Include(c => c.BoardOfDirectors);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/Companies/Details/5
        [Authorize(Policy = "DetailsPolice ")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.company
                .Include(c => c.BoardOfDirectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: OrganizationalChart/Companies/Create
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
            return View();
        }

        // POST: OrganizationalChart/Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("Id,Date,CompanyName,LicenseNumber,TypeOfBusinessActivity,ComponyLogo,ComponyAddress,Notes,BoardOfDirectorsId")] Company company)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var number = _context.company.Count();
                    if (number == 1)
                    {
                        TempData["Error"] = "لا يمكن إضافة اكثر من شركة !!";
                        return View(company);
                    }
                    else
                    {
                        await PopulateDropdownListsAsync();

                        await _CompaniesRepository.AddAsync(company);
                        TempData["Success"] = "تمت العملية بنجاح";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(company);
                }
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

            return View(company);
        }

        // GET: OrganizationalChart/Companies/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var company = await _CompaniesRepository.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: OrganizationalChart/Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CompanyName,LicenseNumber,TypeOfBusinessActivity,ComponyLogo,ComponyAddress,Notes,BoardOfDirectorsId")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();

                try
                {
                    await _CompaniesRepository.UpdateAsync(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            return View(company);
        }

        // GET: OrganizationalChart/Companies/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.company
                .Include(c => c.BoardOfDirectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: OrganizationalChart/Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _CompaniesRepository.GetByIdAsync(id);
            if (company != null)
            {
                await _CompaniesRepository.DeleteAsync(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.company.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {
            var boardOfDirectors = await _context.boardOfDirectors.ToListAsync();
            ViewData["boardOfDirectors"] = new SelectList(boardOfDirectors, "Id", "CouncilName");
            //====================================================

        }
    }
}
