using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.EmployeesAffsirs.Controllers
{
    [Area("EmployeesAffsirs")]
    public class AdministrativePromotionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<AdministrativePromotions> _administrativePromotionsRepository;

        public AdministrativePromotionsController(AppDbContext context, IRepository<AdministrativePromotions> administrativePromotionsRepository)
        {
            _context = context;
            _administrativePromotionsRepository = administrativePromotionsRepository;
        }

        // GET: EmployeesAffsirs/AdministrativePromotions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AdministrativePromotions.Include(a => a.Departments).Include(a => a.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EmployeesAffsirs/AdministrativePromotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrativePromotions = await _context.AdministrativePromotions
                .Include(a => a.Departments)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrativePromotions == null)
            {
                return NotFound();
            }

            return View(administrativePromotions);
        }

        // GET: EmployeesAffsirs/AdministrativePromotions/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
            return View();
        }

        // POST: EmployeesAffsirs/AdministrativePromotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,EmployeeId,DepartmentsId,FromDate,ToDate,Notes")] AdministrativePromotions administrativePromotions)
        {
            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();
                var empl = _context.employee.Find(administrativePromotions.EmployeeId);
                empl.DepartmentsId = administrativePromotions.DepartmentsId;
                //_context.employee.Update(_employee);
                var dep = _context.Departments.Find(administrativePromotions.DepartmentsId);
                if (empl !=null)
                {
                    _context.employee.Update(empl);
                }
                TempData["Success"] = "تم الحفظ بنجاح";
                TempData["Done"] = "سيتم نقل الموظف " + empl.EmployeeName + " الي إدارة " + dep.SubAdministration + " بتاريخ " + administrativePromotions.FromDate.ToString("dd/MM/yyyy"); ;
                await _administrativePromotionsRepository.AddAsync(administrativePromotions);
                return RedirectToAction(nameof(Index));
            }
            return View(administrativePromotions);
        }

        // GET: EmployeesAffsirs/AdministrativePromotions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var administrativePromotions = await _administrativePromotionsRepository.GetByIdAsync(id);
            if (administrativePromotions == null)
            {
                return NotFound();
            }
            return View(administrativePromotions);
        }

        // POST: EmployeesAffsirs/AdministrativePromotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,EmployeeId,DepartmentsId,FromDate,ToDate,Notes")] AdministrativePromotions administrativePromotions)
        {
            if (id != administrativePromotions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();
                try
                {
                    await _administrativePromotionsRepository.UpdateAsync(administrativePromotions);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrativePromotionsExists(administrativePromotions.Id))
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
            return View(administrativePromotions);
        }

        // GET: EmployeesAffsirs/AdministrativePromotions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrativePromotions = await _context.AdministrativePromotions
                .Include(a => a.Departments)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrativePromotions == null)
            {
                return NotFound();
            }

            return View(administrativePromotions);
        }

        // POST: EmployeesAffsirs/AdministrativePromotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrativePromotions = await _administrativePromotionsRepository.GetByIdAsync(id);
            if (administrativePromotions != null)
            {
                _context.AdministrativePromotions.Remove(administrativePromotions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministrativePromotionsExists(int id)
        {
            return _context.AdministrativePromotions.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {

 
            var employee = await _context.employee.ToListAsync();
            ViewData["employee"] = new SelectList(employee, "Id", "EmployeeName");
            //===============================================================================================
            var Departments = await _context.Departments.ToListAsync();
            ViewData["Departments"] = new SelectList(Departments, "Id", "SubAdministration");
        }
        public IActionResult LoadDepartments(int? id)
        {
            if (id !=0)
                    {
                var employee = _context.employee.Where(e => e.DepartmentsId == id).ToList();
                return Ok(employee);

            }
            else
            {
                var employee = _context.employee.ToList();
                return Ok(employee);
            }

        }
        public IActionResult LoadEmployees()
        {

                var employee = _context.employee.ToList();
                return Ok(employee);
            

        }
            
    }
}
