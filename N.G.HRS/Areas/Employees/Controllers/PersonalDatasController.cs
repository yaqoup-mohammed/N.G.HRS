using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.Employees.Controllers
{
    [Area("Employees")]
    public class PersonalDatasController : Controller
    {
        private readonly AppDbContext _context;

        public PersonalDatasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employees/PersonalDatas
        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.personalDatas.Include(p => p.MaritalStatus).Include(p => p.Nationality).Include(p => p.Religion).Include(p => p.Sex).Include(p => p.employee).Include(p => p.guarantees).ToListAsync();
            return View( appDbContext);
        }

        // GET: Employees/PersonalDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalData = await _context.personalDatas
                .Include(p => p.MaritalStatus)
                .Include(p => p.Nationality)
                .Include(p => p.Religion)
                .Include(p => p.Sex)
                .Include(p => p.employee)
                .Include(p => p.guarantees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalData == null)
            {
                return NotFound();
            }

            return View(personalData);
        }

        // GET: Employees/PersonalDatas/Create
        public IActionResult Create()
        {
            ViewData["MaritalStatusId"] = new SelectList(_context.maritalStatuses, "Id", "Name");
            ViewData["NationalityId"] = new SelectList(_context.nationality, "Id", "NationalityName");
            ViewData["ReligionId"] = new SelectList(_context.religion, "Id", "Name");
            ViewData["SexId"] = new SelectList(_context.sex, "Id", "Name");
            ViewData["Id"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["Id"] = new SelectList(_context.guarantees, "Id", "HomeAdress");
            return View();
        }

        // POST: Employees/PersonalDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateOfBirth,Age,HomePhone,Email,PhoneNumber,Address,Notes,CardType,CardNumber,ReleaseDate,CardExpiryDate,EmployeeId,GuaranteesId,SexId,NationalityId,ReligionId,MaritalStatusId")] PersonalData personalData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaritalStatusId"] = new SelectList(_context.maritalStatuses, "Id", "Name", personalData.MaritalStatusId);
            ViewData["NationalityId"] = new SelectList(_context.nationality, "Id", "NationalityName", personalData.NationalityId);
            ViewData["ReligionId"] = new SelectList(_context.religion, "Id", "Name", personalData.ReligionId);
            ViewData["SexId"] = new SelectList(_context.sex, "Id", "Name", personalData.SexId);
            ViewData["Id"] = new SelectList(_context.employee, "Id", "EmployeeName", personalData.Id);
            ViewData["Id"] = new SelectList(_context.guarantees, "Id", "HomeAdress", personalData.Id);
            return View(personalData);
        }

        // GET: Employees/PersonalDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalData = await _context.personalDatas.FindAsync(id);
            if (personalData == null)
            {
                return NotFound();
            }
            ViewData["MaritalStatusId"] = new SelectList(_context.maritalStatuses, "Id", "Name", personalData.MaritalStatusId);
            ViewData["NationalityId"] = new SelectList(_context.nationality, "Id", "NationalityName", personalData.NationalityId);
            ViewData["ReligionId"] = new SelectList(_context.religion, "Id", "Name", personalData.ReligionId);
            ViewData["SexId"] = new SelectList(_context.sex, "Id", "Name", personalData.SexId);
            ViewData["Id"] = new SelectList(_context.employee, "Id", "EmployeeName", personalData.Id);
            ViewData["Id"] = new SelectList(_context.guarantees, "Id", "HomeAdress", personalData.Id);
            return View(personalData);
        }

        // POST: Employees/PersonalDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateOfBirth,Age,HomePhone,Email,PhoneNumber,Address,Notes,CardType,CardNumber,ReleaseDate,CardExpiryDate,EmployeeId,GuaranteesId,SexId,NationalityId,ReligionId,MaritalStatusId")] PersonalData personalData)
        {
            if (id != personalData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalDataExists(personalData.Id))
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
            ViewData["MaritalStatusId"] = new SelectList(_context.maritalStatuses, "Id", "Name", personalData.MaritalStatusId);
            ViewData["NationalityId"] = new SelectList(_context.nationality, "Id", "NationalityName", personalData.NationalityId);
            ViewData["ReligionId"] = new SelectList(_context.religion, "Id", "Name", personalData.ReligionId);
            ViewData["SexId"] = new SelectList(_context.sex, "Id", "Name", personalData.SexId);
            ViewData["Id"] = new SelectList(_context.employee, "Id", "EmployeeName", personalData.Id);
            ViewData["Id"] = new SelectList(_context.guarantees, "Id", "HomeAdress", personalData.Id);
            return View(personalData);
        }

        // GET: Employees/PersonalDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalData = await _context.personalDatas
                .Include(p => p.MaritalStatus)
                .Include(p => p.Nationality)
                .Include(p => p.Religion)
                .Include(p => p.Sex)
                .Include(p => p.employee)
                .Include(p => p.guarantees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalData == null)
            {
                return NotFound();
            }

            return View(personalData);
        }

        // POST: Employees/PersonalDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalData = await _context.personalDatas.FindAsync(id);
            if (personalData != null)
            {
                _context.personalDatas.Remove(personalData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalDataExists(int id)
        {
            return _context.personalDatas.Any(e => e.Id == id);
        }
    }
}
