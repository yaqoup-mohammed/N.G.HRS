using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.EmployeesAffsirs.Models;
using N.G.HRS.Date;
using N.G.HRS.HRSelectList;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.EmployeesAffsirs.Controllers
{
    [Area("EmployeesAffsirs")]
    public class EmploymentStatusManagementsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<EmploymentStatusManagement> _EmploymentStatusManagement;

        public EmploymentStatusManagementsController(AppDbContext context, IRepository<EmploymentStatusManagement> EmploymentStatusManagement)
        {
            _context = context;
            _EmploymentStatusManagement = EmploymentStatusManagement;
        }

        // GET: EmployeesAffsirs/EmploymentStatusManagements
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmploymentStatusManagement.Include(e => e.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EmployeesAffsirs/EmploymentStatusManagements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentStatusManagement = await _context.EmploymentStatusManagement
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employmentStatusManagement == null)
            {
                return NotFound();
            }

            return View(employmentStatusManagement);
        }

        // GET: EmployeesAffsirs/EmploymentStatusManagements/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            EmployeeStatus();
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            return View();
        }

        // POST: EmployeesAffsirs/EmploymentStatusManagements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create(EmploymentStatusManagement employmentStatusManagement)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    EmployeeStatus();
                    await _EmploymentStatusManagement.AddAsync(employmentStatusManagement);
                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(Index));
                    var employee = await _context.employee.FindAsync(employmentStatusManagement.EmployeeId);
                    if (employee != null )
                    {
                        if(employmentStatusManagement.EmployeeStatus != null)
                        {
                            if(employmentStatusManagement.EmployeeStatus != employee.EmploymentStatus)
                            {
                                employee.EmploymentStatus = employmentStatusManagement.EmployeeStatus;
                                _context.employee.Update(employee);
                                EmployeeStatus();
                                await _EmploymentStatusManagement.AddAsync(employmentStatusManagement);
                                TempData["Success"] = "تم الحفظ بنجاح";
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                TempData["Error"] = "بيانات الموظف موجود بالفعل يرجى التأكد من الحالة الموظف!!";
                                return View(employmentStatusManagement);
                            }
                        }
                        else
                        {
                            TempData["Error"] = "يرجى أختيار حالة الموظف";
                            return View(employmentStatusManagement);
                        }
                    }
                    else
                    {
                        TempData["Error"] = "يرجى إختيار موظف";
                        return View(employmentStatusManagement);
                    }


                }
                catch(Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(employmentStatusManagement);
                }

            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employmentStatusManagement.EmployeeId);
            return View(employmentStatusManagement);
        }

        // GET: EmployeesAffsirs/EmploymentStatusManagements/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentStatusManagement = await _EmploymentStatusManagement.GetByIdAsync(id);
            if (employmentStatusManagement == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employmentStatusManagement.EmployeeId);
            return View(employmentStatusManagement);
        }

        // POST: EmployeesAffsirs/EmploymentStatusManagements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int id, EmploymentStatusManagement employmentStatusManagement)
        {
            if (id != employmentStatusManagement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _EmploymentStatusManagement.UpdateAsync(employmentStatusManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentStatusManagementExists(employmentStatusManagement.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName", employmentStatusManagement.EmployeeId);
            return View(employmentStatusManagement);
        }

        // GET: EmployeesAffsirs/EmploymentStatusManagements/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentStatusManagement = await _context.EmploymentStatusManagement
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employmentStatusManagement == null)
            {
                return NotFound();
            }

            return View(employmentStatusManagement);
        }

        // POST: EmployeesAffsirs/EmploymentStatusManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employmentStatusManagement = await _EmploymentStatusManagement.GetByIdAsync(id);
            if (employmentStatusManagement != null)
            {
                await _EmploymentStatusManagement.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmploymentStatusManagementExists(int id)
        {
            return _context.EmploymentStatusManagement.Any(e => e.Id == id);
        }
        public IActionResult EmployeeData(int id) { 
            if (id != 0) {
                try
                {
                    var employee = _context.employee.Find(id);
                    if (employee != null)
                    {
                        return Ok(employee);
                    }
                    else
                    {
                        TempData["Message"] = "لم يتم العثور على الموظف المطلوب!";
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return Ok();
                }
            }
            else
            {
                TempData["Message"] = "أختر الموظف";
                return Ok();
            }
        }
        public void EmployeeStatus()
        {
            List<EmployeeStatusList> employeeStatus = new List<EmployeeStatusList>
            {
                new EmployeeStatusList () { id = 1, name = "مثبت" },
                new EmployeeStatusList () { id = 2, name = "متعاقد" },
                new EmployeeStatusList () { id = 3, name = "متدرب" },
                new EmployeeStatusList () { id = 4, name = "مستمر" },
                new EmployeeStatusList () { id = 5, name = "موقف" },
                new EmployeeStatusList () { id = 6, name = "تم إنهاء الخدمة" },
                new EmployeeStatusList () { id = 7, name = "حارس أمن" }
            };
            SelectList listItems = new SelectList(employeeStatus, "id", "name");
            ViewData["Employee"] = listItems;
        }
        
    }
}
