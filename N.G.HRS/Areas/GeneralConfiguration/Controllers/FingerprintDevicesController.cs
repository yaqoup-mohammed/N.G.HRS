using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;
using N.G.HRS.FingerPrintSetting;
using System.Net;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class FingerprintDevicesController : Controller
    {
        private readonly AppDbContext _context;
        public ZkemClient objZkeeper;
        private bool isDeviceConnected = false;
        public bool IsDeviceConnected
        {
            get { return isDeviceConnected; }
            set
            {
                isDeviceConnected = value;
                if (isDeviceConnected)
                {
                    TempData["message"] = "تم الاتصال بنجاح";
                }
                else
                {
                    TempData["message"] = "تم قطع الاتصال بنجاح";
                    objZkeeper.Disconnect();

                }
            }
        }
        //private readonly ZkemClient _ZK;
        //public FingerprintDevicesController(AppDbContext context)
        //{
        //    _context = context;
        //    _ZK = zkClient;
        //}
        public FingerprintDevicesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: GeneralConfiguration/FingerprintDevices
        [Authorize(policy: "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.fingerprintDevices.ToListAsync());
        }
        // GET: GeneralConfiguration/FingerprintDevices/Details/5
        [Authorize(policy: "DetailsPolicy")]
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
        [ Authorize(policy: "AddPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/FingerprintDevices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(policy: "AddPolicy")]
        public async Task<IActionResult> Create( FingerprintDevices fingerprintDevices)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(fingerprintDevices);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(fingerprintDevices);
            }
            catch (Exception ex)
            {
                TempData["SystemError"] = ex.Message;
                return View(fingerprintDevices);
            }
        }

        // GET: GeneralConfiguration/FingerprintDevices/Edit/5
        [Authorize(policy: "EditPolicy")]
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
        public async Task<IActionResult> Edit(int id, FingerprintDevices fingerprintDevices)
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
        [Authorize(policy: "DeletePolicy")]
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
        [Authorize(policy: "DeletePolicy")]
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

        public IActionResult Ping(string ip)
        {
            //1== ping is successful
            //2== ping is unsuccessful
            //3== ping is unvalid ip
            //4== ip is null
            if (string.IsNullOrEmpty(ip))
            {
                return Json(4);
            }
            var addrString = ip.Trim();
            var value = 0;
            if (UniversalStatic.ValidateIP(ip))
            {
                if (UniversalStatic.PingTheDevice(ip))
                {
                    value = 1;
                    return Json(value);
                }
                else
                {
                    value = 2;
                    return Json(value);
                }
            }
            else
            {
                value = 3;
                return Json(value);
            }
        }

        //=============================================================
        private void RaiseDeviceEvent(object sender, string actionType)
        {
            switch (actionType)
            {
                case UniversalStatic.acx_Disconnect:
                    {
                        //Raise Disconnected Event
                        TempData["message"] = " الجهاز غير  متصل 😴";
                        break;
                    }

                default:
                    break;
            }

        }
        public IActionResult Connect(string ip)
        {
            //1== ping is successful
            //2== ping is unsuccessful
            //3== ping is unvalid ip
            //4== ip is null
            if (string.IsNullOrEmpty(ip))
            {
                return Json(4);
            }
            var ipAddress = ip.Trim();
            objZkeeper = new ZkemClient(RaiseDeviceEvent);
            IsDeviceConnected = objZkeeper.Connect_Net(ipAddress, 4370);

            if (IsDeviceConnected)
            {
                //return Json(IsDeviceConnected);
                return Json(1);
            }
            //return Json(IsDeviceConnected);
            return Json(2);
        }
    }
}