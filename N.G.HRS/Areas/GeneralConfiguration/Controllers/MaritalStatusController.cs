using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class MaritalStatusController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<MaritalStatus> _maritalStatusRepository;

        public MaritalStatusController(AppDbContext context, IRepository<MaritalStatus> maritalStatusRepository)
        {
            _context = context;
            _maritalStatusRepository = maritalStatusRepository;
        }

        // GET: GeneralConfiguration/MaritalStatus
        [Authorize(Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _maritalStatusRepository.GetAllAsync());
            
        }

        // GET: GeneralConfiguration/MaritalStatus/Details/5
        [Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatus = await _context.maritalStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maritalStatus == null)
            {
                return NotFound();
            }

            return View(maritalStatus);
        }

        // GET: GeneralConfiguration/MaritalStatus/Create
        [Authorize(Policy = "AddPolicy")]
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: GeneralConfiguration/MaritalStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] MaritalStatus maritalStatus)
        {
            if (ModelState.IsValid)
            {
              await  _maritalStatusRepository.AddAsync(maritalStatus);
                TempData    ["Success"] = "تم الحفظ بنجاح";

                return View(maritalStatus);
            }
           
            return View(maritalStatus);
        }

        // GET: GeneralConfiguration/MaritalStatus/Edit/5
        [ Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatus = await _context.maritalStatuses.FindAsync(id);
            if (maritalStatus == null)
            {
                return NotFound();
            }
            return View(maritalStatus);
        }

        // POST: GeneralConfiguration/MaritalStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [   Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] MaritalStatus maritalStatus)
        {
            if (id != maritalStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData ["Success"] = "تم التعديل بنجاح";
                try
                {
                    await _maritalStatusRepository.UpdateAsync(maritalStatus);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaritalStatusExists(maritalStatus.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(maritalStatus);
            }
            return View(maritalStatus);
        }

        // GET: GeneralConfiguration/MaritalStatus/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatus = await _context.maritalStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maritalStatus == null)
            {
                return NotFound();
            }

            return View(maritalStatus);
        }

        // POST: GeneralConfiguration/MaritalStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maritalStatus = await _maritalStatusRepository.GetByIdAsync(id);
            if (maritalStatus != null)
            {
                _context.maritalStatuses.Remove(maritalStatus);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Create));
        }

        private bool MaritalStatusExists(int id)
        {
            return _context.maritalStatuses.Any(e => e.Id == id);
        }
        
    }
}
