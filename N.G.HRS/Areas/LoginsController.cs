//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using N.G.HRS.Areas.RegisterAndLogin.Models;
//using N.G.HRS.Date;

//namespace N.G.HRS.Areas
//{
//    [Area("LoginsController.cs")]
//    public class LoginsController : Controller
//    {
//        private readonly AppDbContext _context;

//        public LoginsController(AppDbContext context)
//        {
//            _context = context;
//        }

//        GET: LoginsController.cs/Logins
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.Login.ToListAsync());
//        }

//        GET: LoginsController.cs/Logins/Details/5
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

//        GET: LoginsController.cs/Logins/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        POST: LoginsController.cs/Logins/Create
//        To protect from overposting attacks, enable the specific properties you want to bind to.
//        For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

//       [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Email,Password")] Login login)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(login);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(login);
//        }

//        GET: LoginsController.cs/Logins/Edit/5
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

//        POST: LoginsController.cs/Logins/Edit/5
//         To protect from overposting attacks, enable the specific properties you want to bind to.
//         For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password")] Login login)
//        {
//            if (id != login.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
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

//        GET: LoginsController.cs/Logins/Delete/5
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

//        POST: LoginsController.cs/Logins/Delete/5
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

//        private bool LoginExists(int id)
//        {
//            return _context.Login.Any(e => e.Id == id);
//        }
//    }
//}
