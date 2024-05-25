using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;
//using N.G.HRS.Areas.GeneralConfiguration.Models;
namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class GovernoratesController : Controller
    {
        private readonly AppDbContext _context;

        public GovernoratesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GeneralConfiguration/Governorates
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.governorates.Include(g => g.CountryOne);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GeneralConfiguration/Governorates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var governorate = await _context.governorates
                .Include(g => g.CountryOne)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (governorate == null)
            {
                return NotFound();
            }

            return View(governorate);
        }

        // GET: GeneralConfiguration/Governorates/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name");
            return View();
        }

        // POST: GeneralConfiguration/Governorates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes,CountryId")] Governorate governorate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(governorate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name", governorate.CountryId);
            return View(governorate);
        }

        // GET: GeneralConfiguration/Governorates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var governorate = await _context.governorates.FindAsync(id);
            if (governorate == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name", governorate.CountryId);
            return View(governorate);
        }

        // POST: GeneralConfiguration/Governorates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes,CountryId")] Governorate governorate)
        {
            if (id != governorate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(governorate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GovernorateExists(governorate.Id))
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
            ViewData["CountryId"] = new SelectList(_context.country, "Id", "Name", governorate.CountryId);
            return View(governorate);
        }

        // GET: GeneralConfiguration/Governorates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var governorate = await _context.governorates
                .Include(g => g.CountryOne)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (governorate == null)
            {
                return NotFound();
            }

            return View(governorate);
        }

        // POST: GeneralConfiguration/Governorates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var governorate = await _context.governorates.FindAsync(id);
            if (governorate != null)
            {
                _context.governorates.Remove(governorate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> SaveGovernorateData(string governorate, string notes, int countryId)
        {
            try
            {
                // إنشاء كائن دولة لتخزين البيانات المستلمة
                var newGovernorate = new Governorate
                {
                    Name = governorate,
                    Notes = notes,
                    CountryId = countryId
                };

                // إضافة الدولة الجديدة إلى قاعدة البيانات باستخدام Entity Framework Core
                _context.governorates.Add(newGovernorate);
                await _context.SaveChangesAsync();

                // إرجاع رسالة نجاح
                return Ok("تم حفظ البيانات بنجاح!");
            }
            catch (Exception ex)
            {
                // في حال حدوث أي خطأ، يمكنك إرجاع رسالة خطأ أو استثناء للتحكم في السلوك
                return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
            }
        }

        private bool GovernorateExists(int id)
        {
            return _context.governorates.Any(e => e.Id == id);
        }

        //// عملية حفظ البيانات الموجودة في الجدول إلى قاعدة البيانات
        //[HttpPost]
        //public IActionResult SaveTableDataToDatabase( )
        //{
        //    var data = _context.governorates.ToList(); // الحصول على البيانات من الجدول
        //    if (data == null || !data.Any())
        //        return BadRequest("لا توجد بيانات لحفظها في قاعدة البيانات.");

        //    _context.governorates.AddRange(data); // إضافة البيانات إلى قاعدة البيانات
        //    _context.SaveChanges();

        //    return Ok();
        //}

        ////public IActionResult SaveToDatabase([FromBody] List<Dictionary<string, string>> data)
        ////{
        ////    if (data == null || !data.Any())
        ////        return BadRequest("لم يتم استلام البيانات.");

        ////    _context.governorates.AddRange(data.Select(item => new Governorate
        ////    {
        ////        Name = item["Name"],
        ////        Notes = item["Notes"],
        ////        CountryId = int.Parse(item["CountryId"])
        ////    }));

        ////    _context.SaveChanges();

        ////    return Ok();
        ////}

        //// باقي أجزاء الوحدة التحكم مثل الإجراءات الأخرى (Index, Create, Edit, Delete) لم يتم تغييرها.


        ////[HttpPost]
        ////public IActionResult SaveData([FromBody] Governorate data)
        ////{
        ////    // Save data to the database
        ////    if (ModelState.IsValid)
        ////    {
        ////        var governorate = new Governorate
        ////        {
        ////            Name = data.Name,
        ////            Notes = data.Notes,
        ////            CountryId = data.CountryId
        ////        };
        ////        _context.governorates.Add(governorate);
        ////        _context.SaveChanges();

        ////        return Ok();
        ////    }
        ////    else
        ////    {
        ////        return BadRequest(ModelState);
        ////    }
        ////}

    }
}
