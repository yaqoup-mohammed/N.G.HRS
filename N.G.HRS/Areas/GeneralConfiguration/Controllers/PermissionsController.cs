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
    public class PermissionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Permissions> _permissionsRepository;

        public PermissionsController(AppDbContext context , IRepository<Permissions> permissionsRepository)
        {
            _context = context;
            _permissionsRepository = permissionsRepository;
        }

        // GET: GeneralConfiguration/Permissions
        [Authorize(Policy = "ViewPolicy")]


        public async Task<IActionResult> Index()
        {
            return View(await _context.permissions.ToListAsync());

        }

        // GET: GeneralConfiguration/Permissions/Details/5
        [Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissions = await _context.permissions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permissions == null)
            {
                return NotFound();
            }

            return View(permissions);
        }

        // GET: GeneralConfiguration/Permissions/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Permissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,PermissionName,PermissionStatus,PermissionsDuration,RepeatPermissionDuringTheMonth,Paid,Notes")] Permissions permissions )
        {
            if (ModelState.IsValid)
            {
               await _permissionsRepository.AddAsync(permissions);
                TempData["Success"] = "تم الحفظ بنجاح";
                await _context.SaveChangesAsync();
                //return View(permissions);

                return RedirectToAction ( nameof(Index));
            }
            

            return View(permissions);
        }
            string massage = "تم الحفظ بنجاح تام";

        // GET: GeneralConfiguration/Permissions/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissions = await _permissionsRepository.GetByIdAsync(id);
            if (permissions == null)
            {
                return NotFound();
            }
            return View(permissions);
        }

        // POST: GeneralConfiguration/Permissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PermissionName,PermissionStatus,PermissionsDuration,RepeatPermissionDuringTheMonth,Paid,Notes")] Permissions permissions)
        {
            if (id != permissions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     await _permissionsRepository.UpdateAsync(permissions);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "تم التعديل بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionsExists(permissions.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(permissions);

                //return RedirectToAction(nameof(Index));
            }
            return View(permissions);
        }

        // GET: GeneralConfiguration/Permissions/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissions = await _context.permissions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permissions == null)
            {
                return NotFound();
            }

            return View(permissions);
        }

        // POST: GeneralConfiguration/Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permissions = await _permissionsRepository.GetByIdAsync(id);
            if (permissions != null)
            {
                _context.permissions.Remove(permissions);
            }

            await _context.SaveChangesAsync();
            TempData ["Success"] = "تم الحذف بنجاح";


            return RedirectToAction(nameof(Create) );
        }

        private bool PermissionsExists(int id)
        {
            return _context.permissions.Any(e => e.Id == id);
        }
    }
}
