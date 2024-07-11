using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.MaintenanceControl.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.MaintenanceControl.Controllers
{
    [Area("MaintenanceControl")]
    public class EmployeePermissionsController : Controller
    {
        private readonly AppDbContext _context;
        public EmployeePermissionsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: MaintenanceControl/EmployeePermissions
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmployeePermissions.Include(e => e.Employee).Include(e => e.Period).Include(e => e.Permission).Include(e => e.Supervisor);
            return View(await appDbContext.ToListAsync());
        }
        // GET: MaintenanceControl/EmployeePermissions/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employeePermissions = await _context.EmployeePermissions
                .Include(e => e.Employee)
                .Include(e => e.Period)
                .Include(e => e.Permission)
                .Include(e => e.Supervisor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeePermissions == null)
            {
                return NotFound();
            }

            return View(employeePermissions);
        }
        // GET: MaintenanceControl/EmployeePermissions/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["PeriodId"] = new SelectList(_context.periods, "Id", "PeriodsName");
            var permission=_context.permissions.Where(e => e.PermissionStatus==true).ToList();
            ViewData["PermissionId"] = new SelectList(permission, "Id", "PermissionName");
            ViewData["SupervisorId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }
        // POST: MaintenanceControl/EmployeePermissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,SupervisorId,PeriodId,PermissionId,Date,FromDate,ToDate,FromTime,ToTime,Duration,Hours,Minutes,Reason,Note")] EmployeePermissions employeePermissions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeePermissions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeePermissions.EmployeeId);
            ViewData["PeriodId"] = new SelectList(_context.periods, "Id", "PeriodsName", employeePermissions.PeriodId);
            var permission = _context.permissions.Where(e => e.PermissionStatus == true).ToList();
            ViewData["PermissionId"] = new SelectList(permission, "Id", "PermissionName");
            ViewData["SupervisorId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeePermissions.SupervisorId);
            return View(employeePermissions);
        }
        // GET: MaintenanceControl/EmployeePermissions/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employeePermissions = await _context.EmployeePermissions.FindAsync(id);
            if (employeePermissions == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeePermissions.EmployeeId);
            ViewData["PeriodId"] = new SelectList(_context.periods, "Id", "PeriodsName", employeePermissions.PeriodId);
            var permission = _context.permissions.Where(e => e.PermissionStatus == true).ToList();
            ViewData["PermissionId"] = new SelectList(permission, "Id", "PermissionName");
            ViewData["SupervisorId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeePermissions.SupervisorId);
            return View(employeePermissions);
        }
        // POST: MaintenanceControl/EmployeePermissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,SupervisorId,PeriodId,PermissionId,Date,FromDate,ToDate,FromTime,ToTime,Duration,Hours,Minutes,Reason,Note")] EmployeePermissions employeePermissions)
        {
            if (id != employeePermissions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeePermissions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeePermissionsExists(employeePermissions.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeePermissions.EmployeeId);
            ViewData["PeriodId"] = new SelectList(_context.periods, "Id", "PeriodsName", employeePermissions.PeriodId);
            var permission = _context.permissions.Where(e => e.PermissionStatus == true).ToList();
            ViewData["PermissionId"] = new SelectList(permission, "Id", "PermissionName");
            ViewData["SupervisorId"] = new SelectList(_context.employee, "Id", "EmployeeName", employeePermissions.SupervisorId);
            return View(employeePermissions);
        }

        // GET: MaintenanceControl/EmployeePermissions/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePermissions = await _context.EmployeePermissions
                .Include(e => e.Employee)
                .Include(e => e.Period)
                .Include(e => e.Permission)
                .Include(e => e.Supervisor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeePermissions == null)
            {
                return NotFound();
            }

            return View(employeePermissions);
        }

        // POST: MaintenanceControl/EmployeePermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeePermissions = await _context.EmployeePermissions.FindAsync(id);
            if (employeePermissions != null)
            {
                _context.EmployeePermissions.Remove(employeePermissions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeePermissionsExists(int id)
        {
            return _context.EmployeePermissions.Any(e => e.Id == id);
        }

        public IActionResult GetEmployee(int id)
        {
            if(id!=0)
            {
               var employee = _context.employee.Include(x=>x.Manager).Where(x => x.Id == id).Select(x => new {id=x.ManagerId,name=x.Manager.EmployeeName}).FirstOrDefault();
                if (employee != null)
                {
                    return Json(new { employee });
                }
            }
            return NotFound();
        }
        public IActionResult GetPerios(int id)
        {
            if(id!=0)
            {
               var employee = _context.staffTimes.Include(x=>x.Periods).Where(x => x.EmployeeId == id).Select(x => new { id = x.PeriodId, name = x.Periods.PeriodsName, from = x.Periods.FromTime, to = x.Periods.ToTime }).ToList();
                if (employee != null)
                {
                    return Json(new { employee });
                }
            }
            return NotFound();
        }
        public IActionResult Perios(int id)
        {
            if(id!=0)
            {
                var employee = _context.Periods.FirstOrDefault(x => x.Id == id);
                if (employee != null)
                {
                    return Json( employee );
                }
            }
            return NotFound();
        }
        
    }
}
