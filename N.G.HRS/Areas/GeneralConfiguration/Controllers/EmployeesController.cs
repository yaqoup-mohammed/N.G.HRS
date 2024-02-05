using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/Employees
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.employee.Include(e => e.Departments).Include(e => e.FingerprintDevices).Include(e => e.JobDescription).Include(e => e.Manager).Include(e => e.PracticalExperiences).Include(e => e.Sections).Include(e => e.StatementOfEmployeeFiles).Include(e => e.TrainingCourses);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GeneralConfiguration/Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employee
                .Include(e => e.Departments)
                .Include(e => e.FingerprintDevices)
                .Include(e => e.JobDescription)
                .Include(e => e.Manager)
                .Include(e => e.PracticalExperiences)
                .Include(e => e.Sections)
                .Include(e => e.StatementOfEmployeeFiles)
                .Include(e => e.TrainingCourses)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: GeneralConfiguration/Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration");
            ViewData["FingerprintDevicesId"] = new SelectList(_context.fingerprintDevices, "Id", "ConnectionType");
            ViewData["JobDescriptionId"] = new SelectList(_context.JobDescription, "Id", "Authorities");
            ViewData["ManagerId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["PracticalExperiencesId"] = new SelectList(_context.practicalExperiences, "Id", "Duration");
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName");
            ViewData["StatementOfEmployeeFilesId"] = new SelectList(_context.statementOfEmployeeFiles, "Id", "Notes");
            ViewData["TrainingCoursesId"] = new SelectList(_context.trainingCourses, "Id", "NameCourses");
            return View();
        }

        // POST: GeneralConfiguration/Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeNumber,EmployeeName,DateOfEmployment,PlacementDate,EmploymentStatus,RehireDate,DateOfStoppingWork,UsedFingerprint,SubjectToInsurance,DateInsurance,Notes,DepartmentsId,SectionsId,JobDescriptionId,PracticalExperiencesId,StatementOfEmployeeFilesId,TrainingCoursesId,FingerprintDevicesId,ManagerId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration", employee.DepartmentsId);
            ViewData["FingerprintDevicesId"] = new SelectList(_context.fingerprintDevices, "Id", "ConnectionType", employee.FingerprintDevicesId);
            ViewData["JobDescriptionId"] = new SelectList(_context.JobDescription, "Id", "Authorities", employee.JobDescriptionId);
            ViewData["ManagerId"] = new SelectList(_context.employee, "Id", "EmployeeName", employee.ManagerId);
            ViewData["PracticalExperiencesId"] = new SelectList(_context.practicalExperiences, "Id", "Duration", employee.PracticalExperiencesId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", employee.SectionsId);
            ViewData["StatementOfEmployeeFilesId"] = new SelectList(_context.statementOfEmployeeFiles, "Id", "Notes", employee.StatementOfEmployeeFilesId);
            ViewData["TrainingCoursesId"] = new SelectList(_context.trainingCourses, "Id", "NameCourses", employee.TrainingCoursesId);
            return View(employee);
        }

        // GET: GeneralConfiguration/Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration", employee.DepartmentsId);
            ViewData["FingerprintDevicesId"] = new SelectList(_context.fingerprintDevices, "Id", "ConnectionType", employee.FingerprintDevicesId);
            ViewData["JobDescriptionId"] = new SelectList(_context.JobDescription, "Id", "Authorities", employee.JobDescriptionId);
            ViewData["ManagerId"] = new SelectList(_context.employee, "Id", "EmployeeName", employee.ManagerId);
            ViewData["PracticalExperiencesId"] = new SelectList(_context.practicalExperiences, "Id", "Duration", employee.PracticalExperiencesId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", employee.SectionsId);
            ViewData["StatementOfEmployeeFilesId"] = new SelectList(_context.statementOfEmployeeFiles, "Id", "Notes", employee.StatementOfEmployeeFilesId);
            ViewData["TrainingCoursesId"] = new SelectList(_context.trainingCourses, "Id", "NameCourses", employee.TrainingCoursesId);
            return View(employee);
        }

        // POST: GeneralConfiguration/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeNumber,EmployeeName,DateOfEmployment,PlacementDate,EmploymentStatus,RehireDate,DateOfStoppingWork,UsedFingerprint,SubjectToInsurance,DateInsurance,Notes,DepartmentsId,SectionsId,JobDescriptionId,PracticalExperiencesId,StatementOfEmployeeFilesId,TrainingCoursesId,FingerprintDevicesId,ManagerId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["DepartmentsId"] = new SelectList(_context.Departments, "Id", "SubAdministration", employee.DepartmentsId);
            ViewData["FingerprintDevicesId"] = new SelectList(_context.fingerprintDevices, "Id", "ConnectionType", employee.FingerprintDevicesId);
            ViewData["JobDescriptionId"] = new SelectList(_context.JobDescription, "Id", "Authorities", employee.JobDescriptionId);
            ViewData["ManagerId"] = new SelectList(_context.employee, "Id", "EmployeeName", employee.ManagerId);
            ViewData["PracticalExperiencesId"] = new SelectList(_context.practicalExperiences, "Id", "Duration", employee.PracticalExperiencesId);
            ViewData["SectionsId"] = new SelectList(_context.Sections, "Id", "SectionsName", employee.SectionsId);
            ViewData["StatementOfEmployeeFilesId"] = new SelectList(_context.statementOfEmployeeFiles, "Id", "Notes", employee.StatementOfEmployeeFilesId);
            ViewData["TrainingCoursesId"] = new SelectList(_context.trainingCourses, "Id", "NameCourses", employee.TrainingCoursesId);
            return View(employee);
        }

        // GET: GeneralConfiguration/Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employee
                .Include(e => e.Departments)
                .Include(e => e.FingerprintDevices)
                .Include(e => e.JobDescription)
                .Include(e => e.Manager)
                .Include(e => e.PracticalExperiences)
                .Include(e => e.Sections)
                .Include(e => e.StatementOfEmployeeFiles)
                .Include(e => e.TrainingCourses)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: GeneralConfiguration/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.employee.FindAsync(id);
            if (employee != null)
            {
                _context.employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.employee.Any(e => e.Id == id);
        }
    }
}
