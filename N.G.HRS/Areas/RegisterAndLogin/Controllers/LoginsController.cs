//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using N.G.HRS.Areas.RegisterAndLogin.Models;
//using N.G.HRS.Date;

//namespace N.G.HRS.Areas.RegisterAndLogin.Controllers
//{
//    [Area("RegisterAndLogin")]
//    public class LoginsController : Controller
//    {
//        private readonly AppDbContext _context;

//        public LoginsController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: RegisterAndLogin/Logins
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.Login.ToListAsync());
//        }

//        // GET: RegisterAndLogin/Logins/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var login = await _context.Login
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (login == null)
//            {
//                return NotFound();
//            }

//            return View(login);
//        }

//        // GET: RegisterAndLogin/Logins/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: RegisterAndLogin/Logins/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Create([Bind("Id,Email,Password,JobNumber")] Login login)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        var accountExists = await _context.Account
//        //            .AnyAsync(a => a.Email == login.Email && a.EmployeeId == login.JobNumber && a.Password == login.Password);

//        //        if (!accountExists)
//        //        {
//        //            ModelState.AddModelError(string.Empty, "البريد الإلكتروني أو رقم الموظف أو كلمة المرور غير موجود.");
//        //            return View(login);
//        //        }

//        //        _context.Add(login);
//        //        await _context.SaveChangesAsync();
//        //        return RedirectToAction(nameof(Index));
//        //    }
//        //    return View(login);
//        //}
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Email,Password,JobNumber")] Login login)
//        {
//            if (ModelState.IsValid)
//            {
//                var accountExists = await _context.Account
//                    .AnyAsync(a => a.Email == login.Email && a.EmployeeId == login.JobNumber && a.Password == login.Password);

//                if (!accountExists)
//                {
//                    ModelState.AddModelError(string.Empty, "البريد الإلكتروني أو رقم الموظف أو كلمة المرور غير موجود.");

//                    // إذا كان الرقم الوظيفي موجود، عرض رسالة خاصة بالرقم الوظيفي
//                    if (await _context.Account.AnyAsync(a => a.EmployeeId == login.JobNumber))
//                    {
//                        ModelState.AddModelError(string.Empty, "هذا الرقم الوظيفي موجود.");
//                    }

//                    return View(login);
//                }

//                _context.Add(login);
//                await _context.SaveChangesAsync();
//                //return Redirect("https://localhost:7125/");
//                // إعادة التوجيه إلى الصفحة المطلوبة بعد النجاح
//                //return Redirect("https://localhost:7125/RegisterAndLogin/Logins/Create");

//                return RedirectToAction(nameof(Index));
//            }
//            return View(login);
//        }

//        // GET: RegisterAndLogin/Logins/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var login = await _context.Login.FindAsync(id);
//            if (login == null)
//            {
//                return NotFound();
//            }
//            return View(login);
//        }

//        // POST: RegisterAndLogin/Logins/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,JobNumber")] Login login)
//        {
//            if (id != login.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                var accountExists = await _context.Account
//                    .AnyAsync(a => a.Email == login.Email && a.EmployeeId == login.JobNumber);

//                if (!accountExists)
//                {
//                    ModelState.AddModelError(string.Empty, "البريد الإلكتروني أو رقم الموظف غير موجود.");
//                    return View(login);
//                }

//                try
//                {
//                    _context.Update(login);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!LoginExists(login.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(login);
//        }
//        // GET: RegisterAndLogin/Logins/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var login = await _context.Login
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (login == null)
//            {
//                return NotFound();
//            }

//            return View(login);
//        }

//        // POST: RegisterAndLogin/Logins/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var login = await _context.Login.FindAsync(id);
//            if (login != null)
//            {
//                _context.Login.Remove(login);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//        [HttpPost]
//        public async Task<IActionResult> VerifyAccount(string email, string employeeId, string password, string field)
//        {
//            bool accountExists = false;

//            switch (field)
//            {
//                case "Email":
//                    accountExists = await _context.Account.AnyAsync(a => a.Email == email);
//                    break;
//                case "Password":
//                    accountExists = await _context.Account.AnyAsync(a => a.Email == email && a.Password == password);
//                    break;
//                case "EmployeeId":
//                    accountExists = await _context.Account.AnyAsync(a => a.Email == email && a.EmployeeId == employeeId);
//                    break;
//            }

//            return Json(accountExists);
//        }


//        private bool LoginExists(int id)
//        {
//            return _context.Login.Any(e => e.Id == id);
//        }
//    }
//}
