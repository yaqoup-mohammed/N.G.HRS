using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;
using zkemkeeper;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class FingerprintDevicesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly FingerprintDevices _finger;

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
        public async Task<IActionResult> Create([Bind("Id,DevicesName,DeviceType,DeviceStatus,ConnectionType,DateOfPurchase,VendorName,VendorPhon,VendorAdress,ManufactureCompany,DeviceSpecifications,IpAddress,IsConnected,Notes")] FingerprintDevices fingerprintDevices)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,DevicesName,DeviceType,DeviceStatus,ConnectionType,DateOfPurchase,VendorName,VendorPhon,VendorAdress,ManufactureCompany,DeviceSpecifications,IpAddress,IsConnected,Notes")] FingerprintDevices fingerprintDevices)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Connect(string? ipAddress)
        {
            if (ipAddress != null)
            {
                var isConnected = new FingerprintDevices();
                
                if (isConnected.IsConnected == false)
                {
                    try
                    {
                        TempData["massage"] = "جاري الاتصال....";
                        CZKEM objCZKEM = new CZKEM();
                        if (objCZKEM.Connect_Net(ipAddress, (int)CONSTANTS.PORT))
                        {
                            objCZKEM.SetDeviceTime2(objCZKEM.MachineNumber, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                            var newFingerprintDevices = new FingerprintDevices
                            {
                                IsConnected = true,
                                IpAddress = ipAddress
                            };
                            _context.fingerprintDevices.Update(newFingerprintDevices);
                            await _context.SaveChangesAsync();
                            if (isConnected.IsConnected == true)
                            {
                                TempData["massage"] = "تم الاتصال";
                                TempData["massage"] = "Obtaining attendance data...";
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        TempData["massage"] = ex.Message;

                    }
                }
                else
                {
                    TempData["massage"] = "الجهاز متصل!!";
                }
            }
            else
            {
                TempData["massage"]= "ادخل IP";
            }
            
            return RedirectToAction(nameof(Create));
        }
        public enum CONSTANTS
        {
            PORT = 4370,
        }
        static void Main(string ipAddress)
        {
            Console.WriteLine("Connecting...");
            CZKEM objCZKEM = new CZKEM();
            if (objCZKEM.Connect_Net(ipAddress, (int)CONSTANTS.PORT))
            {
                objCZKEM.SetDeviceTime2(objCZKEM.MachineNumber, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                Console.WriteLine("Connection Successful!");
                Console.WriteLine("Obtaining attendance data...");
            }
            else
            {
                Console.WriteLine("Connection Failed!");
            }

            if (objCZKEM.ReadGeneralLogData(objCZKEM.MachineNumber))
            {
                //ArrayList logs = new ArrayList();
                string log;
                string dwEnrollNumber;
                int dwVerifyMode;
                int dwInOutMode;
                int dwYear;
                int dwMonth;
                int dwDay;
                int dwHour;
                int dwMinute;
                int dwSecond;
                int dwWorkCode = 1;
                int AWorkCode;
                objCZKEM.GetWorkCode(dwWorkCode, out AWorkCode);
                objCZKEM.SaveTheDataToFile(objCZKEM.MachineNumber, "attendance.txt", 1);
                while (true)
                {
                    if (!objCZKEM.SSR_GetGeneralLogData(
                    objCZKEM.MachineNumber,
                    out dwEnrollNumber,
                    out dwVerifyMode,
                    out dwInOutMode,
                    out dwYear,
                    out dwMonth,
                    out dwDay,
                    out dwHour,
                    out dwMinute,
                    out dwSecond,
                    ref AWorkCode
                    ))
                    {
                        break;
                    }
                    log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
                    Console.WriteLine(log);
                    //logs.Add(log);


                }
            }
            //Console.ReadLine();
        }

        static void getAttendanceLogs(CZKEM objCZKEM)
        {
            string log;
            string dwEnrollNumber;
            int dwVerifyMode;
            int dwInOutMode;
            int dwYear;
            int dwMonth;
            int dwDay;
            int dwHour;
            int dwMinute;
            int dwSecond;
            int dwWorkCode = 1;
            int AWorkCode;
            objCZKEM.GetWorkCode(dwWorkCode, out AWorkCode);
            objCZKEM.SaveTheDataToFile(objCZKEM.MachineNumber, "attendance.txt", 1);
            while (true)
            {
                if (!objCZKEM.SSR_GetGeneralLogData(
                objCZKEM.MachineNumber,
                out dwEnrollNumber,
                out dwVerifyMode,
                out dwInOutMode,
                out dwYear,
                out dwMonth,
                out dwDay,
                out dwHour,
                out dwMinute,
                out dwSecond,
                ref AWorkCode
                ))
                {
                    break;
                }
                log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
                Console.WriteLine(log);
            }
        }

        static string time(int Time)
        {
            string stringTime = "";
            if (Time < 10)
            {
                stringTime = "0" + Time.ToString();
            }
            else
            {
                stringTime = Time.ToString();
            }
            return stringTime;
        }

        static string verificationMode(int verifyMode)
        {
            String mode = "";
            switch (verifyMode)
            {
                case 0:
                    mode = "Password";
                    break;
                case 1:
                    mode = "Fingerprint";
                    break;
                case 2:
                    mode = "Card";
                    break;
            }
            return mode;
        }

        static string InorOut(int InOut)
        {
            string InOrOut = "";
            switch (InOut)
            {
                case 0:
                    InOrOut = "IN";
                    break;
                case 1:
                    InOrOut = "OUT";
                    break;
                case 2:
                    InOrOut = "BREAK-OUT";
                    break;
                case 3:
                    InOrOut = "BREAK-IN";
                    break;
                case 4:
                    InOrOut = "OVERTIME-IN";
                    break;
                case 5:
                    InOrOut = "OVERTIME-OUT";
                    break;

            }
            return InOrOut;
        }
        //===============================================
        //===============================================
    }
}
