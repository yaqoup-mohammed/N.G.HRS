using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AdministrativePromotions.Include(a => a.Departments).Include(a => a.Employee);
            return View(await appDbContext.ToListAsync());
        }


        // GET: EmployeesAffsirs/AdministrativePromotions/Details/5
        [Authorize(Policy = "DetailsPolicy")]

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
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create(int? id)
        {
            if(id != null)
            {
                await PopulateDropdownListsAsync();

                var administrativePromotions = await _administrativePromotionsRepository.GetByIdAsync(id);
                if (administrativePromotions == null)
                {
                    return NotFound();
                }
                return View(administrativePromotions);
            }
            else
            {
                await PopulateDropdownListsAsync();
                return View();
            }


        }

        // POST: EmployeesAffsirs/AdministrativePromotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create(int? id, AdministrativePromotions administrativePromotions)
        {
            if(id== null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await PopulateDropdownListsAsync();
                        var empl = _context.employee.Find(administrativePromotions.EmployeeId);
                        //empl.DepartmentsId = administrativePromotions.DepartmentsId;
                        //_context.employee.Update(_employee);
                        var dep = _context.Departments.Find(administrativePromotions.DepartmentsId);
                        if (empl != null && dep != null)
                        {
                            if (empl.DepartmentsId == administrativePromotions.DepartmentsId)
                            {
                                TempData["Error"] = "يجب تحديث بيانات الموظف " + empl.EmployeeName + " لأنه لم يتم تحديثه بعد";
                                return View(administrativePromotions);
                            }   
                            else
                            {
                                while(administrativePromotions.FromDate == DateTime.Now) {
                                    empl.DepartmentsId = administrativePromotions.DepartmentsId;
                                    TempData["Success"] = "تم تحديث بيانات الموظف " + empl.EmployeeName + " بنجاح";
                                    _context.employee.Update(empl);
                                    _context.SaveChanges();
                                    break;
                                }


                            }

                        }
                        TempData["Success"] = "تم الحفظ بنجاح" + "سيتم نقل الموظف " + empl.EmployeeName + " الي إدارة " + dep.SubAdministration + " بتاريخ " + administrativePromotions.FromDate.ToString("dd/MM/yyyy"); ;
                        await _administrativePromotionsRepository.AddAsync(administrativePromotions);
                        return RedirectToAction(nameof(Index));
                    }
                    catch(Exception ex)
                    {
                        TempData["SystemError"] = ex.Message;
                        return View(administrativePromotions);
                    }
                }
                return View(administrativePromotions);
            }
            else
            {
                if (id != administrativePromotions.Id)
                {
                    TempData["Error"] = "هذا الموظف ليس موجود !!";
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
        }


        // GET: EmployeesAffsirs/AdministrativePromotions/Delete/5
        [ Authorize(Policy = "DeletePolicy")]
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

            return PartialView("_Delete",administrativePromotions);
        }

        // POST: EmployeesAffsirs/AdministrativePromotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ Authorize(Policy = "DeletePolicy")]

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

        public IActionResult LoudData(int id)
        {
            if (id != 0)
            {
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
    

    }
}
