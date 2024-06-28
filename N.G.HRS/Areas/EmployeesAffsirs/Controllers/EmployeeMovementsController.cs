﻿using System;
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
using N.G.HRS.Areas.PlanningAndJobDescription;  


namespace N.G.HRS.Areas.EmployeesAffsirs.Controllers
{
    [Area("EmployeesAffsirs")]
    public class EmployeeMovementsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository < EmployeeMovements> _employeeMovementsRepository;

        

        public EmployeeMovementsController(AppDbContext context, IRepository<EmployeeMovements> employeeMovementsRepository)
        {
            _context = context;
            _employeeMovementsRepository = employeeMovementsRepository;
        }

        // GET: EmployeesAffsirs/EmployeeMovements
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmployeeMovements.Include(e => e.Employee).Include(e => e.jopdescription);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EmployeesAffsirs/EmployeeMovements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeMovements = await _context.EmployeeMovements
                .Include(e => e.Employee)
                .Include(e => e.jopdescription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeMovements == null)
            {
                return NotFound();
            }

            return View(employeeMovements);
        }

        // GET: EmployeesAffsirs/EmployeeMovements/Create
        public async Task<IActionResult> Create(int? id)
        {
            if(id != null)
            {
                var employeeMovements =await _employeeMovementsRepository.GetByIdAsync(id);
                if(employeeMovements == null)
                {
                    return NotFound();

                }     
                PopulateDropDownLists();

                return View( employeeMovements);

            }
            else
            {
                PopulateDropDownLists();
                return View();
            }
        }

        // POST: EmployeesAffsirs/EmployeeMovements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, EmployeeMovements employeeMovements)
        {




            if (id == null)
            {
                if (ModelState.IsValid)
                {
                    
                    try
                    {
                        //تحديث البيانات الجدول     
                        var emp = _context.employee.Find(employeeMovements.EmployeeId);
                        if (emp != null)
                        {
                            emp.JobDescriptionId = (int)employeeMovements.jopdescriptionId;
                            _context.employee.Update(emp);
                        }
                        //        //تحديث البيانات الجدول 
                        await _employeeMovementsRepository.AddAsync(employeeMovements);
                        TempData["Success"] = "تم الحفظ بنجاح";
                        return RedirectToAction(nameof(Index));

                    }
                    catch (Exception ex)
                    {
                        TempData["SystemError"] = ex.Message;
                        return View( employeeMovements);
                    }
                }

                PopulateDropDownLists();
                TempData["Error"] = "حدث خطأ ما قد تكون البيانات خاطئة تأكد من صحة البيانات ثم  حاول مرة اخرى";
                return View( employeeMovements);
            }
            else
            {
                if (id != employeeMovements.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _employeeMovementsRepository.UpdateAsync(employeeMovements);
                        TempData["Success"] = "تم التعديل بنجاح";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeMovementsExists(employeeMovements.Id))
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
                PopulateDropDownLists();
                return View(employeeMovements);
            }
        }
                   
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeMovements = await _context.EmployeeMovements
                .Include(e => e.Employee)
                .Include(e => e.jopdescription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeMovements == null)
            {
                return NotFound();
            }

            return View(employeeMovements);
        }

        // POST: EmployeesAffsirs/EmployeeMovements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeMovements = await _employeeMovementsRepository.GetByIdAsync(id);
            if (employeeMovements != null)
            {
                _context.EmployeeMovements.Remove(employeeMovements);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Create));
        }

        private bool EmployeeMovementsExists(int id)
        {
            return _context.EmployeeMovements.Any(e => e.Id == id);
        }
        private void PopulateDropDownLists()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employee, "Id", "EmployeeName");
            ViewData["jopdescriptionId"] = new SelectList(_context.JobDescription, "Id", "JopName");
        }
        public IActionResult EmployeeMovements(int id)
        {
            var pant = _context.employee.Find(id);
            if (pant != null)
            {
                return Ok(pant);
            }
            else
            {
                return NotFound();

            }
        }
    }
}
