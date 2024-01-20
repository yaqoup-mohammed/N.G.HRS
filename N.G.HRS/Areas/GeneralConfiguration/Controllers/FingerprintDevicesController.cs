using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class FingerprintDevicesController : Controller
    {
        private readonly AppDbContext _context;

        public FingerprintDevicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/FingerprintDevices
        public async Task<IActionResult> Index()
        {
            return View(await _context.fingerprintDevices.ToListAsync());
        }

        // GET: GeneralConfiguration/FingerprintDevices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fingerprintDevices = await _context.fingerprintDevices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fingerprintDevices == null)
            {
                return NotFound();
            }

            return View(fingerprintDevices);
        }

        // GET: GeneralConfiguration/FingerprintDevices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/FingerprintDevices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DevicesName,DeviceType,DeviceStatus,ConnectionType,DateOfPurchase,VendorName,VendorPhon,VendorAdress,ManufactureCompany,DeviceSpecifications,Notes")] FingerprintDevices fingerprintDevices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fingerprintDevices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fingerprintDevices);
        }

        // GET: GeneralConfiguration/FingerprintDevices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fingerprintDevices = await _context.fingerprintDevices.FindAsync(id);
            if (fingerprintDevices == null)
            {
                return NotFound();
            }
            return View(fingerprintDevices);
        }

        // POST: GeneralConfiguration/FingerprintDevices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DevicesName,DeviceType,DeviceStatus,ConnectionType,DateOfPurchase,VendorName,VendorPhon,VendorAdress,ManufactureCompany,DeviceSpecifications,Notes")] FingerprintDevices fingerprintDevices)
        {
            if (id != fingerprintDevices.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fingerprintDevices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FingerprintDevicesExists(fingerprintDevices.Id))
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
            return View(fingerprintDevices);
        }

        // GET: GeneralConfiguration/FingerprintDevices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fingerprintDevices = await _context.fingerprintDevices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fingerprintDevices == null)
            {
                return NotFound();
            }

            return View(fingerprintDevices);
        }

        // POST: GeneralConfiguration/FingerprintDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fingerprintDevices = await _context.fingerprintDevices.FindAsync(id);
            if (fingerprintDevices != null)
            {
                _context.fingerprintDevices.Remove(fingerprintDevices);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FingerprintDevicesExists(int id)
        {
            return _context.fingerprintDevices.Any(e => e.Id == id);
        }
    }
}
