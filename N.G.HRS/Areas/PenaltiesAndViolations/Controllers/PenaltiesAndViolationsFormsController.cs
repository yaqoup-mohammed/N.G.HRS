using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PenaltiesAndViolations.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.PenaltiesAndViolations.Controllers
{
    [Area("PenaltiesAndViolations")]
    public class PenaltiesAndViolationsFormsController : Controller
    {
        private readonly AppDbContext _context;
        //private readonly IRepository <PenaltiesAndViolationsForms> _penaltiesAndViolationsFormsRepository;

        public PenaltiesAndViolationsFormsController(AppDbContext context, IRepository<PenaltiesAndViolationsForms> penaltiesAndViolationsFormsRepository)
        {
            _context = context;
            //_penaltiesAndViolationsFormsRepository = penaltiesAndViolationsFormsRepository;
        }

        // GET: PenaltiesAndViolations/PenaltiesAndViolationsForms
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.penaltiesAndViolationsForms.Include(p => p.Penalties).Include(p => p.Violations);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PenaltiesAndViolations/PenaltiesAndViolationsForms/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penaltiesAndViolationsForms = await _context.penaltiesAndViolationsForms
                .Include(p => p.Penalties)
                .Include(p => p.Violations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penaltiesAndViolationsForms == null)
            {
                return NotFound();
            }

            return View(penaltiesAndViolationsForms);
        }

        // GET: PenaltiesAndViolations/PenaltiesAndViolationsForms/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["PenaltiesId"] = new SelectList(_context.Penalties, "Id", "PenaltiesName");
            ViewData["ViolationsId"] = new SelectList(_context.Violations, "Id", "ViolationsName");
            return View();
        }

        // POST: PenaltiesAndViolations/PenaltiesAndViolationsForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes,NumberOfTime,ViolationsId,PenaltiesId")] PenaltiesAndViolationsForms penaltiesAndViolationsForms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(penaltiesAndViolationsForms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PenaltiesId"] = new SelectList(_context.Penalties, "Id", "PenaltiesName", penaltiesAndViolationsForms.PenaltiesId);
            ViewData["ViolationsId"] = new SelectList(_context.Violations, "Id", "ViolationsName", penaltiesAndViolationsForms.ViolationsId);
            return View(penaltiesAndViolationsForms);
        }

        // GET: PenaltiesAndViolations/PenaltiesAndViolationsForms/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penaltiesAndViolationsForms = await _context.penaltiesAndViolationsForms.FindAsync(id);
            if (penaltiesAndViolationsForms == null)
            {
                return NotFound();
            }
            ViewData["PenaltiesId"] = new SelectList(_context.Penalties, "Id", "PenaltiesName", penaltiesAndViolationsForms.PenaltiesId);
            ViewData["ViolationsId"] = new SelectList(_context.Violations, "Id", "ViolationsName", penaltiesAndViolationsForms.ViolationsId);
            return View(penaltiesAndViolationsForms);
        }

        // POST: PenaltiesAndViolations/PenaltiesAndViolationsForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes,NumberOfTime,ViolationsId,PenaltiesId")] PenaltiesAndViolationsForms penaltiesAndViolationsForms)
        {
            if (id != penaltiesAndViolationsForms.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penaltiesAndViolationsForms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenaltiesAndViolationsFormsExists(penaltiesAndViolationsForms.Id))
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
            ViewData["PenaltiesId"] = new SelectList(_context.Penalties, "Id", "PenaltiesName", penaltiesAndViolationsForms.PenaltiesId);
            ViewData["ViolationsId"] = new SelectList(_context.Violations, "Id", "ViolationsName", penaltiesAndViolationsForms.ViolationsId);
            return View(penaltiesAndViolationsForms);
        }

        // GET: PenaltiesAndViolations/PenaltiesAndViolationsForms/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penaltiesAndViolationsForms = await _context.penaltiesAndViolationsForms
                .Include(p => p.Penalties)
                .Include(p => p.Violations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penaltiesAndViolationsForms == null)
            {
                return NotFound();
            }

            return View(penaltiesAndViolationsForms);
        }

        // POST: PenaltiesAndViolations/PenaltiesAndViolationsForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var penaltiesAndViolationsForms = await _context.penaltiesAndViolationsForms.FindAsync(id);
            if (penaltiesAndViolationsForms != null)
            {
                _context.penaltiesAndViolationsForms.Remove(penaltiesAndViolationsForms);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenaltiesAndViolationsFormsExists(int id)
        {
            return _context.penaltiesAndViolationsForms.Any(e => e.Id == id);
        }
    }
}
