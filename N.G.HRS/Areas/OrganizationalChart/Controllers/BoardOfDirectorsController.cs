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
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.OrganizationalChart.Controllers
{
    [Area("OrganizationalChart")]
    public class BoardOfDirectorsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<BoardOfDirectors> _boardOfDirectorsRepository;

        public BoardOfDirectorsController(AppDbContext context, IRepository<BoardOfDirectors> boardOfDirectorsRepository)
        {
            _context = context;
            _boardOfDirectorsRepository = boardOfDirectorsRepository;
        }

        // GET: OrganizationalChart/BoardOfDirectors
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.boardOfDirectors.Include(b => b.MembershipOfTheBoardOfDirectors);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrganizationalChart/BoardOfDirectors/Details/5
        [Authorize(Policy = "DetailsPolicy")]

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
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
            return View();
        }

        // POST: OrganizationalChart/BoardOfDirectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create( BoardOfDirectors boardOfDirectors)
        {
            await PopulateDropdownListsAsync();

            if (ModelState.IsValid)
            {
                try
                {

                    await _boardOfDirectorsRepository.AddAsync(boardOfDirectors);
                    TempData["Success"] = "تمت العملية بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(boardOfDirectors);
                }
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

            return View(boardOfDirectors);
        }

        // GET: OrganizationalChart/BoardOfDirectors/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            await PopulateDropdownListsAsync();

            if (id == null)
            {
                return NotFound();
            }
            await PopulateDropdownListsAsync();

            var boardOfDirectors = await _boardOfDirectorsRepository.GetByIdAsync(id);
            if (boardOfDirectors == null)
            {
                return NotFound();
            }
            return View(boardOfDirectors);
        }

        // POST: OrganizationalChart/BoardOfDirectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CouncilName,Notes,NameOfMembership,MembershipOfTheBoardOfDirectorsId")] BoardOfDirectors boardOfDirectors)
        {
            if (id != boardOfDirectors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();

                try
                {
                    await PopulateDropdownListsAsync();

                    await _boardOfDirectorsRepository.UpdateAsync(boardOfDirectors);
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
            return View(boardOfDirectors);
        }

        // GET: OrganizationalChart/BoardOfDirectors/Delete/5
        [Authorize(Policy = "DeletePolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boardOfDirectors = await _boardOfDirectorsRepository.GetByIdAsync(id);
            if (boardOfDirectors != null)
            {
                _boardOfDirectorsRepository.DeleteAsync(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardOfDirectorsExists(int id)
        {
            return _context.boardOfDirectors.Any(e => e.Id == id);
        }
        private async Task PopulateDropdownListsAsync()
        {
            var membershipOfTheBoardOfs = await _context.membershipOfTheBoardOfs.ToListAsync();
            ViewData["membershipOfTheBoardOfs"] = new SelectList(membershipOfTheBoardOfs, "Id", "TypeOFMembership");
            //====================================================
        }
    }
}
