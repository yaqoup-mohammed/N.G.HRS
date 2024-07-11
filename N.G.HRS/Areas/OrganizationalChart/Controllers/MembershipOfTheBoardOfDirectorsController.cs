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
    public class MembershipOfTheBoardOfDirectorsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<MembershipOfTheBoardOfDirectors> _membershipOfTheBoardOfDirectorsRepository;


        public MembershipOfTheBoardOfDirectorsController(AppDbContext context, IRepository<MembershipOfTheBoardOfDirectors> membershipOfTheBoardOfDirectorsRepository)
        {
            _context = context;
            _membershipOfTheBoardOfDirectorsRepository = membershipOfTheBoardOfDirectorsRepository;
        }


        // GET: OrganizationalChart/MembershipOfTheBoardOfDirectors
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.membershipOfTheBoardOfs.ToListAsync());
        }

        // GET: OrganizationalChart/MembershipOfTheBoardOfDirectors/Details/5
        [Authorize(Policy = "DetailsPolicy")]

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
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: OrganizationalChart/MembershipOfTheBoardOfDirectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,TypeOFMembership,Notes")] MembershipOfTheBoardOfDirectors membershipOfTheBoardOfDirectors)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _membershipOfTheBoardOfDirectorsRepository.AddAsync(membershipOfTheBoardOfDirectors);
                   TempData["Success"] = "تمت العملية بنجاح";
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    TempData["SystemError"] = ex.Message;
                    return View(membershipOfTheBoardOfDirectors);
                }
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

            return View(membershipOfTheBoardOfDirectors);
        }

        // GET: OrganizationalChart/MembershipOfTheBoardOfDirectors/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipOfTheBoardOfDirectors =  await _membershipOfTheBoardOfDirectorsRepository.GetByIdAsync(id);
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
        [Authorize(Policy = "EditPolicy")]
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
                    await _membershipOfTheBoardOfDirectorsRepository.UpdateAsync(membershipOfTheBoardOfDirectors);
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
        [Authorize(Policy = "DeletePolicy")]

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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipOfTheBoardOfDirectors = await _membershipOfTheBoardOfDirectorsRepository.GetByIdAsync(id);
            if (membershipOfTheBoardOfDirectors != null)
            {
                await _membershipOfTheBoardOfDirectorsRepository.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MembershipOfTheBoardOfDirectorsExists(int id)
        {
            return _context.membershipOfTheBoardOfs.Any(e => e.Id == id);
        }
 
    }
}
