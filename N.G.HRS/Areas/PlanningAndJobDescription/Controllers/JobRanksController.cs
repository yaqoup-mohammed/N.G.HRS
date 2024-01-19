using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Controllers
{
    [Area("PlanningAndJobDescription")]
    public class JobRanksController : Controller
    {
        private readonly AppDbContext _context;

        public JobRanksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PlanningAndJobDescription/JobRanks
        public async Task<IActionResult> Index()
        {
            return View(await _context.jobRanks.ToListAsync());
        }

        // GET: PlanningAndJobDescription/JobRanks/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanningAndJobDescription/JobRanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RankName,Notes")] JobRanks jobRanks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobRanks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobRanks);
        }

        // GET: PlanningAndJobDescription/JobRanks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRanks = await _context.jobRanks.FindAsync(id);
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
                    _context.Update(jobRanks);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(jobRanks);
        }

        // GET: PlanningAndJobDescription/JobRanks/Delete/5
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobRanks = await _context.jobRanks.FindAsync(id);
            if (jobRanks != null)
            {
                _context.jobRanks.Remove(jobRanks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobRanksExists(int id)
        {
            return _context.jobRanks.Any(e => e.Id == id);
        }
    }
}
