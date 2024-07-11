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
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Departments> _departmentsrepository;

        public DepartmentsController(AppDbContext context, IRepository<Departments> departmentsrepository)
        {
            _context = context;
            _departmentsrepository = departmentsrepository;
        }

        // GET: OrganizationalChart/Departments
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Departments.Include(d => d.Sectors);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/Departments/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .Include(d => d.Sectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // GET: OrganizationalChart/Departments/Create
        [Authorize(Policy = "AddPolicy")]

        public async Task< IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
            return View();
        }

        // POST: OrganizationalChart/Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("Id,SubAdministration,Notes,SectorsId")] Departments departments)
        {
            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();

                try
                {
                    await _departmentsrepository.AddAsync(departments);
                    TempData["Success"] = "تمت العملية بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(departments);
                }
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

            return View(departments);
        }

        // GET: OrganizationalChart/Departments/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var departments = await _departmentsrepository.GetByIdAsync(id);
            if (departments == null)
            {
                return NotFound();
            }
            return View(departments);
        }

        // POST: OrganizationalChart/Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,SubAdministration,Notes,SectorsId")] Departments departments)
        {
            if (id != departments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();

                try
                {
                    await _departmentsrepository.UpdateAsync(departments);
                    TempData["Success"] = "تمت العملية بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(departments.Id))
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
            return View(departments);
        }

        // GET: OrganizationalChart/Departments/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .Include(d => d.Sectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // POST: OrganizationalChart/Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departments = await _departmentsrepository.GetByIdAsync(id);
            if (departments != null)
            {
                await _departmentsrepository.DeleteAsync(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentsExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {
            var sectors = await _context.sectors.ToListAsync();
            ViewData["sectors"] = new SelectList(sectors, "Id", "SectorsName");
            //====================================================

        }
    }
}
