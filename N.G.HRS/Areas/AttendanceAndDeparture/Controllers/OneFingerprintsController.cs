    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Controllers
{
    [Area("AttendanceAndDeparture")]
    public class OneFingerprintsController : Controller
    {
        private readonly AppDbContext _context;

        public OneFingerprintsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceAndDeparture/OneFingerprints
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.oneFingerprints.Include(o => o.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceAndDeparture/OneFingerprints/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oneFingerprint = await _context.oneFingerprints
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oneFingerprint == null)
            {
                return NotFound();
            }

            return View(oneFingerprint);
        }

        // GET: AttendanceAndDeparture/OneFingerprints/Create
        [Authorize(Policy = "AddPolicy")]

        public async Task< IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
            return View();
        }

        // POST: AttendanceAndDeparture/OneFingerprints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Date,OneDayFingerprint,FromDate,ToDate,Notes,EmployeeId")] OneFingerprint one)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await PopulateDropdownListsAsync();

                    if (one.OneDayFingerprint)
                    {
                        if (one.FromDate > one.ToDate)
                        {
                            TempData["Error"] = "تاريخ البدء يجب ان يكون اقل من تاريخ الانتهاء";
                            return View(one);
                        }
                        _context.AddAsync(one);
                        TempData["Success"] = "تمت العملية بنجاح";
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    else
                    {
                        TempData["Error"] = "يجب تأكيد زر البصمة الواحدة";
                        return View(one);

                    }

                }
                catch(Exception ex)
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ  "+"\n" + ex.Message;
                    return View(one);
                }

            }
            return View(one);
        }

        // GET: AttendanceAndDeparture/OneFingerprints/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var oneFingerprint = await _context.oneFingerprints.FindAsync(id);
            if (oneFingerprint == null)
            {
                return NotFound();
            }
            return View(oneFingerprint);
        }

        // POST: AttendanceAndDeparture/OneFingerprints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,OneDayFingerprint,FromDate,ToDate,Notes,EmployeeId")] OneFingerprint oneFingerprint)
        {
            if (id != oneFingerprint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await PopulateDropdownListsAsync();

                    _context.Update(oneFingerprint);
                    ViewData["success"] = "تمت العملية بنجاح";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OneFingerprintExists(oneFingerprint.Id))
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
            return View(oneFingerprint);
        }

        // GET: AttendanceAndDeparture/OneFingerprints/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oneFingerprint = await _context.oneFingerprints
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oneFingerprint == null)
            {
                return NotFound();
            }

            return View(oneFingerprint);
        }

        // POST: AttendanceAndDeparture/OneFingerprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oneFingerprint = await _context.oneFingerprints.FindAsync(id);
            if (oneFingerprint != null)
            {
                _context.oneFingerprints.Remove(oneFingerprint);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OneFingerprintExists(int id)
        {
            return _context.oneFingerprints.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {
            var employee = await _context.employee.ToListAsync();
            ViewData["EmployeeId"] = new SelectList(employee, "Id", "EmployeeName");


        }
    }
}
