//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using N.G.HRS.Areas.GeneralConfiguration.Models;
//using N.G.HRS.Date;
//using N.G.HRS.Repository;

//namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
//{
//    [Area("GeneralConfiguration")]
//    public class SexesController : Controller
//    {
//        private readonly AppDbContext _context;
//        private readonly IRepository<Sex> _sexRepository;

//        public SexesController(AppDbContext context , IRepository<Sex> sexRepository)
//        {
//            _context = context;
//            _sexRepository = sexRepository;
//        }

//        // GET: GeneralConfiguration/Sexes
//        [Authorize (Policy = "ViewPolicy")]
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.sex.ToListAsync());
//        }

//        // GET: GeneralConfiguration/Sexes/Details/5
//        [Authorize(Policy = "DetailsPolicy")]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var sex = await _context.sex
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (sex == null)
//            {
//                return NotFound();
//            }

//            return View(sex);
//        }

//        // GET: GeneralConfiguration/Sexes/Create
//        [Authorize(Policy = "AddPolicy")]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: GeneralConfiguration/Sexes/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Policy = "AddPolicy")]
//        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] Sex sex)
//        {
//            if (ModelState.IsValid)
//            {
//               await _sexRepository.AddAsync(sex);
//                TempData["Success"] = "تمت العملية بنجاح";
//                return RedirectToAction(nameof(Create));

//                //return RedirectToAction(nameof(Index));
//            }
//            return View(sex);
//        }

//        // GET: GeneralConfiguration/Sexes/Edit/5
//        [Authorize(Policy = "EditPolicy")]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var sex = await _sexRepository.GetByIdAsync(id);
//            if (sex == null)
//            {
//                return NotFound();
//            }
//            return View(sex);
//        }

//        // POST: GeneralConfiguration/Sexes/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Policy = "EditPolicy")]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] Sex sex)
//        {
//            if (id != sex.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    await _sexRepository.UpdateAsync(sex);
//                    TempData ["Success"] = "تمت تعديل بنجاح";
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!SexExists(sex.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Create));
//                //return RedirectToAction(nameof(Index));

//            }
//            return View(sex);
//        }

//        // GET: GeneralConfiguration/Sexes/Delete/5
//        [ Authorize(Policy = "DeletePolicy")]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var sex = await _context.sex
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (sex == null)
//            {
//                return NotFound();
//            }

//            return View(sex);
//        }

//        // POST: GeneralConfiguration/Sexes/Delete/5
//        [Authorize(Policy = "DeletePolicy")]
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var sex = await _sexRepository.GetByIdAsync(id);
//            if (sex != null)
//            {
//                _context.sex.Remove(sex);
//            }
//            await _context.SaveChangesAsync();

//            TempData["Success"] = "تم الحذف بنجاح";
//            return RedirectToAction(nameof(Create));

//            //return RedirectToAction(nameof(Index));
//        }

//        private bool SexExists(int id)
//        {
//            return _context.sex.Any(e => e.Id == id);
//        }
//    }
//}

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
    public class SexesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Sex> _sexRepository;

        public SexesController(AppDbContext context, IRepository<Sex> sexRepository)
        {
            _context = context;
            _sexRepository = sexRepository;
        }

        // GET: GeneralConfiguration/Sexes
        [Authorize(Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.sex.ToListAsync());
        }

        // GET: GeneralConfiguration/Sexes/Details/5
        [Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sex = await _context.sex.FirstOrDefaultAsync(m => m.Id == id);
            if (sex == null)
            {
                return NotFound();
            }

            return View(sex);
        }

        // GET: GeneralConfiguration/Sexes/Create
        [Authorize(Policy = "AddPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Sexes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes")] Sex sex)
        {
            if (ModelState.IsValid)
            {
                await _sexRepository.AddAsync(sex);
                TempData["Success"] = "تمت العملية بنجاح";
                return RedirectToAction(nameof(Create));
            }
            return View(sex);
        }

        // GET: GeneralConfiguration/Sexes/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sex = await _sexRepository.GetByIdAsync(id);
            if (sex == null)
            {
                return NotFound();
            }
            return View(sex);
        }

        // POST: GeneralConfiguration/Sexes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] Sex sex)
        {
            if (id != sex.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _sexRepository.UpdateAsync(sex);
                    TempData["Success"] = "تمت تعديل بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SexExists(sex.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Create));
            }
            return View(sex);
        }

        // GET: GeneralConfiguration/Sexes/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sex = await _context.sex.FirstOrDefaultAsync(m => m.Id == id);
            if (sex == null)
            {
                return NotFound();
            }

            return View(sex);
        }

        // POST: GeneralConfiguration/Sexes/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sex = await _sexRepository.GetByIdAsync(id);
            if (sex != null)
            {
                // تحقق إذا كانت القيم المحذوفة هي "ذكر" أو "أنثى"
                if (sex.Name == "ذكر" || sex.Name == "أنثى")
                {
                    TempData["Error"] = "لا يمكن حذف القيم الافتراضية للجنس.";
                    return RedirectToAction(nameof(Index));
                }

                _context.sex.Remove(sex);
                await _context.SaveChangesAsync();

                TempData["Success"] = "تم الحذف بنجاح";
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SexExists(int id)
        {
            return _context.sex.Any(e => e.Id == id);
        }
    }
}
