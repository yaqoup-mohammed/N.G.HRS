using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.OrganizationalChart.Controllers
{
    [Area("OrganizationalChart")]
    public class BoardOfDirectorsController : Controller
    {
        private readonly AppDbContext _context;

        public BoardOfDirectorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrganizationalChart/BoardOfDirectors
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.boardOfDirectors.Include(b => b.MembershipOfTheBoardOfDirectors);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/BoardOfDirectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardOfDirectors = await _context.boardOfDirectors
                .Include(b => b.MembershipOfTheBoardOfDirectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boardOfDirectors == null)
            {
                return NotFound();
            }

            return View(boardOfDirectors);
        }

        // GET: OrganizationalChart/BoardOfDirectors/Create
        public IActionResult Create()
        {
            ViewData["MembershipOfTheBoardOfDirectorsId"] = new SelectList(_context.membershipOfTheBoardOfs, "Id", "TypeOFMembership");
            return View();
        }

        // POST: OrganizationalChart/BoardOfDirectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,CouncilName,Notes,NameOfMembership,MembershipOfTheBoardOfDirectorsId")] BoardOfDirectors boardOfDirectors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boardOfDirectors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MembershipOfTheBoardOfDirectorsId"] = new SelectList(_context.membershipOfTheBoardOfs, "Id", "TypeOFMembership", boardOfDirectors.MembershipOfTheBoardOfDirectorsId);
            return View(boardOfDirectors);
        }

        // GET: OrganizationalChart/BoardOfDirectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardOfDirectors = await _context.boardOfDirectors.FindAsync(id);
            if (boardOfDirectors == null)
            {
                return NotFound();
            }
            ViewData["MembershipOfTheBoardOfDirectorsId"] = new SelectList(_context.membershipOfTheBoardOfs, "Id", "TypeOFMembership", boardOfDirectors.MembershipOfTheBoardOfDirectorsId);
            return View(boardOfDirectors);
        }

        // POST: OrganizationalChart/BoardOfDirectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CouncilName,Notes,NameOfMembership,MembershipOfTheBoardOfDirectorsId")] BoardOfDirectors boardOfDirectors)
        {
            if (id != boardOfDirectors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boardOfDirectors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardOfDirectorsExists(boardOfDirectors.Id))
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
            ViewData["MembershipOfTheBoardOfDirectorsId"] = new SelectList(_context.membershipOfTheBoardOfs, "Id", "TypeOFMembership", boardOfDirectors.MembershipOfTheBoardOfDirectorsId);
            return View(boardOfDirectors);
        }

        // GET: OrganizationalChart/BoardOfDirectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardOfDirectors = await _context.boardOfDirectors
                .Include(b => b.MembershipOfTheBoardOfDirectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boardOfDirectors == null)
            {
                return NotFound();
            }

            return View(boardOfDirectors);
        }

        // POST: OrganizationalChart/BoardOfDirectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boardOfDirectors = await _context.boardOfDirectors.FindAsync(id);
            if (boardOfDirectors != null)
            {
                _context.boardOfDirectors.Remove(boardOfDirectors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardOfDirectorsExists(int id)
        {
            return _context.boardOfDirectors.Any(e => e.Id == id);
        }
    }
}
