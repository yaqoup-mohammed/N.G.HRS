using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Controllers
{
    [Area("PlanningAndJobDescription")]
    public class JobRanksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<JobRanks> _jobRanksRepository;
        public JobRanksController(AppDbContext context, IRepository<JobRanks> jobRanksRepository)
        {
            _context = context;
            _jobRanksRepository = jobRanksRepository;
        }



        // GET: PlanningAndJobDescription/JobRanks
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.jobRanks.ToListAsync());
        }

        // GET: PlanningAndJobDescription/JobRanks/Details/5
        [Authorize(Policy = "DetailsPolicy")]


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRanks = await _context.jobRanks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobRanks == null)
            {
                return NotFound();
            }

            return View(jobRanks);
        }

        // GET: PlanningAndJobDescription/JobRanks/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanningAndJobDescription/JobRanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,RankName,Notes")] JobRanks jobRanks)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist=_context.jobRanks.Any(x => x.RankName == jobRanks.RankName);
                    if (!exist)
                    {
                        await _jobRanksRepository.AddAsync(jobRanks);
                        TempData["Success"] = "تمت الإضافة بنجاح";
                        return RedirectToAction(nameof(Create));
                    }
                    else
                    {
                        TempData["Error"] = "هذه الفئة موجودة بالفعل";
                        return View(jobRanks);
                    }
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(jobRanks);
                }

                //return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

            return View(jobRanks);
        }

        // GET: PlanningAndJobDescription/JobRanks/Edit/5
        [Authorize(Policy = "EditPolicy")]


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRanks = await _jobRanksRepository.GetByIdAsync(id);
            if (jobRanks == null)
            {
                return NotFound();
            }
            return View(jobRanks);
        }

        // POST: PlanningAndJobDescription/JobRanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RankName,Notes")] JobRanks jobRanks)
        {
            if (id != jobRanks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _jobRanksRepository.UpdateAsync(jobRanks);
                    TempData["Success"] = "تم التعديل بنجاح";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobRanksExists(jobRanks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(jobRanks);
                //return RedirectToAction(nameof(Index));
            }
            return View(jobRanks);
        }

        // GET: PlanningAndJobDescription/JobRanks/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRanks = await _context.jobRanks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobRanks == null)
            {
                return NotFound();
            }

            return View(jobRanks);
        }

        // POST: PlanningAndJobDescription/JobRanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobRanks = await _jobRanksRepository.GetByIdAsync(id);
            if (jobRanks != null)
            {
                _context.jobRanks.Remove(jobRanks);
            }
            TempData["Success"] = "تم الحذف بنجاح";

            return RedirectToAction(nameof(Create));

            //return RedirectToAction(nameof(Index));
        }

        private bool JobRanksExists(int id)
        {
            return _context.jobRanks.Any(e => e.Id == id);
        }
    }
}
