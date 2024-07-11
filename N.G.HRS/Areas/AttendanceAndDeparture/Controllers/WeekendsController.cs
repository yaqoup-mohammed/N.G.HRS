using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Controllers
{
    [Area("AttendanceAndDeparture")]
    public class WeekendsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Periods _periods;
        private readonly IRepository<Weekends> _weekendsRepository;



        public WeekendsController(AppDbContext context, IRepository<Weekends> weekendsRepository, Periods periods)
        {
            _context = context;
            _weekendsRepository = weekendsRepository;
            _periods = periods;

        }

        // GET: AttendanceAndDeparture/Weekends
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.weekends.Include(w => w.Periods).Include(w => w.PermanenceModels);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AttendanceAndDeparture/Weekends/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weekends = await _context.weekends
                .Include(w => w.Periods)
                .Include(w => w.PermanenceModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weekends == null)
            {
                return NotFound();
            }

            return View(weekends);
        }

        // GET: AttendanceAndDeparture/Weekends/Create
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();

            return View();
        }

        // POST: AttendanceAndDeparture/Weekends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,SaturDay,SunDay,MonDay,Tuesday,Wednesday,Thursday,Friday,PermanenceModelsId,PeriodsId,")] Weekends weekends)
        {
           
            await PopulateDropdownListsAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    var periodid = _context.periods.Find(weekends.PeriodsId);

                    var saturday = periodid.Saturday;
                    var sunday = periodid.SunDay;
                    var monday = periodid.Monday;
                    var tuesday = periodid.Tuesday;
                    var wednesday = periodid.Wednesday;
                    var thursday = periodid.Thursday;
                    var friday = periodid.Friday;
                        
                    if (saturday == true && weekends.SaturDay == true)
                    {
                        TempData["Error"] = " السبت هو يوم دوام في الفترة المحددة" + "لايمكنك نحديد يوم دوام كأجازة ";
                        return View(weekends);
                    }
                    if (sunday == true && weekends.SunDay == true)
                    {
                        TempData["Error"] = " الأحد هو يوم دوام في الفترة المحددة " + "لايمكنك نحديد يوم دوام كأجازة ";
                        return View(weekends);

                    }
                    if (monday == true && weekends.MonDay == true)
                    {
                        TempData["Error"] = " الأثنين هو يوم دوام في الفترة المحددة " + " لايمكنك نحديد يوم دوام كأجازة";
                        return View(weekends);

                    }
                    if (tuesday == true && weekends.Tuesday == true)
                    {
                        TempData["Error"] = "الثلاثاء هو يوم دوام في الفترة المحددة " + "لايمكنك نحديد يوم دوام كأجازة ";
                        return View(weekends);

                    }
                    if (wednesday == true && weekends.Wednesday == true)
                    {
                        TempData["Error"] = " الأربعاء هو يوم دوام في الفترة المحددة " + " لايمكنك نحديد يوم دوام كأجازة";
                        return View(weekends);

                    }
                    if (thursday == true && weekends.Thursday == true)
                    {
                        TempData["Error"] = " الخميس هو يوم دوام في الفترة المحددة " + "لايمكنك نحديد يوم دوام كأجازة  ";
                        return View(weekends);

                    }
                    else if (friday == true && weekends.Friday == true)
                    {
                        TempData["Error"] = " الجمعة هو يوم دوام في الفترة المحددة" + " لايمكنك نحديد يوم دوام كأجازة";
                        return View(weekends);

                    }
                    else
                    {


                        _weekendsRepository.AddAsync(weekends);
                        TempData["Success"] = "تم الحفظ بنجاح";
                        return RedirectToAction(nameof(Index));
                    }

                }
                catch (Exception ex)
                {
                    TempData["Error"] = "حدث خطأ ما " + ex.Message;
                    return View(weekends);
                }
            }

            return View(weekends);
        }

        // GET: AttendanceAndDeparture/Weekends/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weekends = await _weekendsRepository.GetByIdAsync(id);
            if (weekends == null)
            {
                return NotFound();
            }

            return View(weekends);
        }

        // POST: AttendanceAndDeparture/Weekends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SaturDay,SunDay,MonDay,Tuesday,Wednesday,Thursday,Friday,PermanenceModelsId,PeriodsId")] Weekends weekends)
        {
            await PopulateDropdownListsAsync();

            if (id != weekends.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  await  _weekendsRepository.UpdateAsync(weekends);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeekendsExists(weekends.Id))
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
            return View(weekends);
        }

        // GET: AttendanceAndDeparture/Weekends/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weekends = await _weekendsRepository.DeleteAsync(id);
            if (weekends == null)
            {
                return NotFound();
            }

            return View(weekends);
        }

        // POST: AttendanceAndDeparture/Weekends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id != null)
            {
                var weekends = await _weekendsRepository.DeleteAsync(id);
                TempData["Success"] = "تم الحذف بنجاح";
            }
            else
                        {
                TempData["Error"] = "حدث خطأ ما";

            }
            

            return RedirectToAction(nameof(Index));
        }

        private bool WeekendsExists(int id)
        {
            return _context.weekends.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {

            
            var periods = await _context.periods.ToListAsync();
            ViewData["Periods"] = new SelectList(periods, "Id", "PeriodsName");
            var permanance = await _context.permanenceModels.ToListAsync();
            ViewData["permanance"] = new SelectList(permanance, "Id", "PermanenceName");

        }

    }
}
