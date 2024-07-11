using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PayRoll.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    public class EmployeeAdvancesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeAdvancesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PayRoll/EmployeeAdvances
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmployeeAdvances.Include(e => e.Currency).Include(e => e.Departments).Include(e => e.Employee).Include(e => e.EmployeeAccount).Include(e => e.Sections);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PayRoll/EmployeeAdvances/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAdvances = await _context.EmployeeAdvances
                .Include(e => e.Currency)
                .Include(e => e.Departments)
                .Include(e => e.Employee)
                .Include(e => e.EmployeeAccount)
                .Include(e => e.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAdvances == null)
            {
                return NotFound();
            }

            return View(employeeAdvances);
        }

        // GET: PayRoll/EmployeeAdvances/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "SubAdministration");
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["EmployeeAccountId"] = new SelectList(_context.EmployeeAccount, "Id", "Id");
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName");
            return View();
        }

        // POST: PayRoll/EmployeeAdvances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,DepartmentId,SectionId,EmployeeAccountId,CurrencyId,Amount,Notes")] EmployeeAdvances employeeAdvances)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeAdvances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeAdvances.CurrencyId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "SubAdministration", employeeAdvances.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAdvances.EmployeeId);
            ViewData["EmployeeAccountId"] = new SelectList(_context.EmployeeAccount, "Id", "Id", employeeAdvances.EmployeeAccountId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", employeeAdvances.SectionId);
            return View(employeeAdvances);
        }

        // GET: PayRoll/EmployeeAdvances/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAdvances = await _context.EmployeeAdvances.FindAsync(id);
            if (employeeAdvances == null)
            {
                return NotFound();
            }
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeAdvances.CurrencyId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "SubAdministration", employeeAdvances.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAdvances.EmployeeId);
            ViewData["EmployeeAccountId"] = new SelectList(_context.EmployeeAccount, "Id", "Id", employeeAdvances.EmployeeAccountId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", employeeAdvances.SectionId);
            return View(employeeAdvances);
        }

        // POST: PayRoll/EmployeeAdvances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]



        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,DepartmentId,SectionId,EmployeeAccountId,CurrencyId,Amount,Notes")] EmployeeAdvances employeeAdvances)
        {
            if (id != employeeAdvances.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeAdvances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeAdvancesExists(employeeAdvances.Id))
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
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "CurrencyCode", employeeAdvances.CurrencyId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "SubAdministration", employeeAdvances.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeeAdvances.EmployeeId);
            ViewData["EmployeeAccountId"] = new SelectList(_context.EmployeeAccount, "Id", "Id", employeeAdvances.EmployeeAccountId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionsName", employeeAdvances.SectionId);
            return View(employeeAdvances);
        }

        // GET: PayRoll/EmployeeAdvances/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAdvances = await _context.EmployeeAdvances
                .Include(e => e.Currency)
                .Include(e => e.Departments)
                .Include(e => e.Employee)
                .Include(e => e.EmployeeAccount)
                .Include(e => e.Sections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeAdvances == null)
            {
                return NotFound();
            }

            return View(employeeAdvances);
        }

        // POST: PayRoll/EmployeeAdvances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeAdvances = await _context.EmployeeAdvances.FindAsync(id);
            if (employeeAdvances != null)
            {
                _context.EmployeeAdvances.Remove(employeeAdvances);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult GetSections(int departmentId)
        {
            var sections = _context.Sections.Include(x => x.Departments).Where(s => s.DepartmentsId == departmentId).Select(x => new { id = x.Id, name = x.SectionsName }).ToList();
            if (sections == null)
            {
                return NotFound();
            }
            return Json(new { sections });
        }
        private bool EmployeeAdvancesExists(int id)
        {
            return _context.EmployeeAdvances.Any(e => e.Id == id);
        }
        public IActionResult getData(int id)
        {
            var empAdvances = _context.EmployeeAccount.Include(x=>x.employee).Include(x=>x.FinanceAccount).Where(x => x.EmployeeId == id).Select(x => new {secId= x.employee.SectionsId,depId= x.employee.DepartmentsId,accId= x.FinanceAccount.Id,accName= x.FinanceAccount.Name }).ToList();
            return Json(new {  empAdvances });
        }
    }
}


