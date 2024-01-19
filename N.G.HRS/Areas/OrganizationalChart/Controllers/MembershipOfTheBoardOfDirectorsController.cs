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
    public class MembershipOfTheBoardOfDirectorsController : Controller
    {
        private readonly AppDbContext _context;

        public MembershipOfTheBoardOfDirectorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrganizationalChart/MembershipOfTheBoardOfDirectors
        public async Task<IActionResult> Index()
        {
            return View(await _context.membershipOfTheBoardOfs.ToListAsync());
        }

        // GET: OrganizationalChart/MembershipOfTheBoardOfDirectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipOfTheBoardOfDirectors = await _context.membershipOfTheBoardOfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipOfTheBoardOfDirectors == null)
            {
                return NotFound();
            }

            return View(membershipOfTheBoardOfDirectors);
        }

        // GET: OrganizationalChart/MembershipOfTheBoardOfDirectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrganizationalChart/MembershipOfTheBoardOfDirectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeOFMembership,Notes")] MembershipOfTheBoardOfDirectors membershipOfTheBoardOfDirectors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershipOfTheBoardOfDirectors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membershipOfTheBoardOfDirectors);
        }

        // GET: OrganizationalChart/MembershipOfTheBoardOfDirectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipOfTheBoardOfDirectors = await _context.membershipOfTheBoardOfs.FindAsync(id);
            if (membershipOfTheBoardOfDirectors == null)
            {
                return NotFound();
            }
            return View(membershipOfTheBoardOfDirectors);
        }

        // POST: OrganizationalChart/MembershipOfTheBoardOfDirectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOFMembership,Notes")] MembershipOfTheBoardOfDirectors membershipOfTheBoardOfDirectors)
        {
            if (id != membershipOfTheBoardOfDirectors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipOfTheBoardOfDirectors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipOfTheBoardOfDirectorsExists(membershipOfTheBoardOfDirectors.Id))
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
            return View(membershipOfTheBoardOfDirectors);
        }

        // GET: OrganizationalChart/MembershipOfTheBoardOfDirectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipOfTheBoardOfDirectors = await _context.membershipOfTheBoardOfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipOfTheBoardOfDirectors == null)
            {
                return NotFound();
            }

            return View(membershipOfTheBoardOfDirectors);
        }

        // POST: OrganizationalChart/MembershipOfTheBoardOfDirectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipOfTheBoardOfDirectors = await _context.membershipOfTheBoardOfs.FindAsync(id);
            if (membershipOfTheBoardOfDirectors != null)
            {
                _context.membershipOfTheBoardOfs.Remove(membershipOfTheBoardOfDirectors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipOfTheBoardOfDirectorsExists(int id)
        {
            return _context.membershipOfTheBoardOfs.Any(e => e.Id == id);
        }
    }
}
