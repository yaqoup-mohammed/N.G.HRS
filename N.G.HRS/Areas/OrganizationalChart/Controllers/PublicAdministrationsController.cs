using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.OrganizationalChart.Controllers
{
    [Area("OrganizationalChart")]
    public class PublicAdministrationsController : Controller
    {
        private readonly AppDbContext _context;

        public PublicAdministrationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrganizationalChart/PublicAdministrations
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.publicAdministrations.ToListAsync());
        }

        // GET: OrganizationalChart/PublicAdministrations/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicAdministration = await _context.publicAdministrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicAdministration == null)
            {
                return NotFound();
            }

            return View(publicAdministration);
        }

        // GET: OrganizationalChart/PublicAdministrations/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: OrganizationalChart/PublicAdministrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,PublicAdministrationName,Nots")] PublicAdministration publicAdministration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(publicAdministration);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "تمت العملية بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(publicAdministration);
                }
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";
            return View(publicAdministration);
        }

        // GET: OrganizationalChart/PublicAdministrations/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicAdministration = await _context.publicAdministrations.FindAsync(id);
            if (publicAdministration == null)
            {
                return NotFound();
            }
            return View(publicAdministration);
        }

        // POST: OrganizationalChart/PublicAdministrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PublicAdministrationName,Nots")] PublicAdministration publicAdministration)
        {
            if (id != publicAdministration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicAdministration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicAdministrationExists(publicAdministration.Id))
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
            return View(publicAdministration);
        }

        // GET: OrganizationalChart/PublicAdministrations/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicAdministration = await _context.publicAdministrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicAdministration == null)
            {
                return NotFound();
            }

            return View(publicAdministration);
        }

        // POST: OrganizationalChart/PublicAdministrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicAdministration = await _context.publicAdministrations.FindAsync(id);
            if (publicAdministration != null)
            {
                _context.publicAdministrations.Remove(publicAdministration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicAdministrationExists(int id)
        {
            return _context.publicAdministrations.Any(e => e.Id == id);
        }
    }
}
