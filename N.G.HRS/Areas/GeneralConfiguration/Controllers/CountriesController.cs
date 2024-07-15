using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SharePoint.Client;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;
using OfficeOpenXml;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class CountriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Country> _countryRepository;

        public CountriesController(AppDbContext context, IRepository<Country> countryRepository)
        {
            _context = context;
            _countryRepository = countryRepository;
        }

        // GET: GeneralConfiguration/Countries
        [Authorize(Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.country.ToListAsync());
        }

        // GET: GeneralConfiguration/Countries/Details/5
        [Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.country
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: GeneralConfiguration/Countries/Create
        [Authorize(Policy = "AddPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfiguration/Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                await _countryRepository.AddAsync(country);
                TempData["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: GeneralConfiguration/Countries/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }



        //public async Task<IActionResult> ExportToExcelCountry()1
        //{
        //    var countries = await _context.country.ToListAsync(); // استرجاع جميع الدول من قاعدة البيانات

        //    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // تحديد سياق الترخيص (يعتمد على نوع الترخيص الخاص بك)

        //    using (var package = new ExcelPackage())
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("دول");

        //        // عنوان الأعمدة

        //        worksheet.Cells[1, 1].Value = "Id";

        //        worksheet.Cells[1, 2].Value = "الدولة";
        //        worksheet.Cells[1, 3].Value = "الملاحظة";

        //        // ملء البيانات
        //        int row = 2;
        //        foreach (var country in countries)
        //        {
        //            worksheet.Cells[row, 1].Value = country.Id;
        //            worksheet.Cells[row, 2].Value = country.Name;
        //            worksheet.Cells[row, 3].Value = country.Notes;
        //            row++;
        //        }

        //        // تحويل الملف إلى بيانات بايت وإرجاعه كنتيجة تنزيل
        //        var stream = new MemoryStream();
        //        package.SaveAs(stream);
        //        stream.Position = 0;

        //        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        string fileName = "countries.xlsx";

        //        return File(stream, contentType, fileName);
        //    }
        //}
        //public async Task<IActionResult> ExportToPdf()
        //{
        //    var countries = await _context.country.ToListAsync(); // استرجاع جميع الدول من قاعدة البيانات

        // POST: GeneralConfiguration/Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _countryRepository.UpdateAsync(country);
                    TempData["Success"] = "تم التعديل بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(country);
        }

        // GET: GeneralConfiguration/Countries/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.country
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: GeneralConfiguration/Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _countryRepository.GetByIdAsync(id);
            if (country != null)
            {
                _context.country.Remove(country);
            }
            await _context.SaveChangesAsync();
            TempData["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Index));
        }

        // استيراد ملف إكسل إلى قاعدة البيانات
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> ImportCountry(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                ModelState.AddModelError("File", "يرجى تحديد ملف للتحميل.");
                return View("Import");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // تجاوز الصف الأول بسبب العنوان
                    {
                        string countryName = worksheet.Cells[row, 2].Value?.ToString().Trim();
                        string notes = worksheet.Cells[row, 3].Value?.ToString().Trim();

                        if (!string.IsNullOrEmpty(countryName))
                        {
                            var existingCountry = await _context.country.FirstOrDefaultAsync(c => c.Name == countryName);
                            if (existingCountry != null)
                            {
                                existingCountry.Notes = notes;
                                _context.country.Update(existingCountry);
                            }
                            else
                            {
                                var newCountry = new Country { Name = countryName, Notes = notes };
                                _context.country.Add(newCountry);
                            }
                        }
                    }
                    TempData["Success"] = "تم استيراد ملف  بنجاح";
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "ViewPolicy")]
  

        [HttpPost]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> CheckCountryExists(string country)
        {
            var exists = await _context.country.AnyAsync(c => c.Name == country);
            return Json(new { exists });
        }

        [HttpPost]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> SaveCountryData(string country, string notes)
        {
            try
            {
                var existingCountry = await _context.country.FirstOrDefaultAsync(c => c.Name == country);
                if (existingCountry != null)
                {
                    return Json(new { error = true, message = "اسم الدولة موجود بالفعل في قاعدة البيانات." });
                }

                var newCountry = new Country
                {
                    Name = country,
                    Notes = notes
                };

                _context.country.Add(newCountry);
                await _context.SaveChangesAsync();
                TempData["Success"] = "تم الحفظ بنجاح";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
            }
        }

        private bool CountryExists(int id)
        {
            return _context.country.Any(e => e.Id == id);
        }
    }
}





//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//using iTextSharp.text.pdf;
//using iTextSharp.text;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Microsoft.SharePoint.Client;
//using N.G.HRS.Areas.GeneralConfiguration.Models;
//using N.G.HRS.Date;
//using N.G.HRS.Repository;
//using OfficeOpenXml;
//using System.ComponentModel;

//namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
//{
//    [Area("GeneralConfiguration")]
//    public class CountriesController : Controller
//    {
//        private readonly AppDbContext _context;
//        private readonly IRepository<Country> _countryRepository;

//        public CountriesController(AppDbContext context, IRepository<Country> countryRepository)
//        {
//            _context = context;
//            _countryRepository = countryRepository;
//        }

//        // GET: GeneralConfiguration/Countries
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.country.ToListAsync());
//        }

//        // GET: GeneralConfiguration/Countries/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var country = await _context.country
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (country == null)
//            {
//                return NotFound();
//            }

//            return View(country);
//        }

//        // GET: GeneralConfiguration/Countries/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: GeneralConfiguration/Countries/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(Country country)
//        {
//            if (ModelState.IsValid)
//            {



//                //if(country != null)
//                //{
//                //var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Country>>(country.Data);
//                //foreach (var item in data)
//                //{
//                await _countryRepository.AddAsync(country);
//                //}
//                TempData["Success"] = "تم الحفظ بنجاح";
//                //}
//                //else
//                //{
//                //   TempData["Error"] = "لم تتم الإضافة، لم يتم إرسال بيانات الدول";
//                //}
//                return RedirectToAction(nameof(Index));
//            }
//            return View(country);
//        }
//        //============================استيراد ملف اكسل الى قاعدة البيانات=======
//        public async Task<IActionResult> ImportCountry(IFormFile file)
//        {
//            if (file == null || file.Length <= 0)
//            {
//                ModelState.AddModelError("File", "يرجى تحديد ملف للتحميل.");
//                return View("Import");
//            }

//            using (var stream = new MemoryStream())
//            {
//                await file.CopyToAsync(stream);

//                using (var package = new ExcelPackage(stream))
//                {
//                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
//                    int rowCount = worksheet.Dimension.Rows;

//                    for (int row = 2; row <= rowCount; row++) // تجاوز الصف الأول بسبب العنوان
//                    {

//                        string countryName = worksheet.Cells[row, 2].Value?.ToString().Trim();
//                        string notes = worksheet.Cells[row, 3].Value?.ToString().Trim();

//                        if (!string.IsNullOrEmpty(countryName))
//                        {
//                            // التحقق من عدم تكرار الدولة
//                            var existingCountry = await _context.country.FirstOrDefaultAsync(c => c.Name == countryName);
//                            if (existingCountry != null)
//                            {
//                                // إذا كانت الدولة موجودة بالفعل، قم بتحديث الملاحظات
//                                existingCountry.Notes = notes;
//                                _context.country.Update(existingCountry); // تحديث الدولة في قاعدة البيانات
//                            }
//                            else
//                            {
//                                // إذا لم تكن الدولة موجودة، قم بإضافتها إلى قاعدة البيانات
//                                var newCountry = new Country { Name = countryName, Notes = notes };
//                                _context.country.Add(newCountry);
//                            }
//                        }
//                    }
//                    TempData["Success"] = "تم استيراد ملف  بنجاح";
//                    await _context.SaveChangesAsync();
//                }
//            }

//            return RedirectToAction("Index");
//        }



//        public async Task<IActionResult> ExportToExcelCountry()
//        {
//            var countries = await _context.country.ToListAsync(); // استرجاع جميع الدول من قاعدة البيانات

//            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // تحديد سياق الترخيص (يعتمد على نوع الترخيص الخاص بك)

//            using (var package = new ExcelPackage())
//            {
//                var worksheet = package.Workbook.Worksheets.Add("دول");

//                // عنوان الأعمدة

//                worksheet.Cells[1, 1].Value = "Id";

//                worksheet.Cells[1, 2].Value = "الدولة";
//                worksheet.Cells[1, 3].Value = "الملاحظة";

//                // ملء البيانات
//                int row = 2;
//                foreach (var country in countries)
//                {
//                    worksheet.Cells[row, 1].Value = country.Id;
//                    worksheet.Cells[row, 2].Value = country.Name;
//                    worksheet.Cells[row, 3].Value = country.Notes;
//                    row++;
//                }

//                // تحويل الملف إلى بيانات بايت وإرجاعه كنتيجة تنزيل
//                var stream = new MemoryStream();
//                package.SaveAs(stream);
//                stream.Position = 0;

//                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//                string fileName = "countries.xlsx";

//                return File(stream, contentType, fileName);
//            }
//        }



//        // POST: GeneralConfiguration/Countries/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes")] Country country)
//        {
//            if (id != country.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    await _countryRepository.UpdateAsync(country);
//                    TempData["Success"] = "تم التعديل بنجاح";
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!CountryExists(country.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//                //return View(country);
//            }
//            return View(country);
//        }

//        // GET: GeneralConfiguration/Countries/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var country = await _context.country
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (country == null)
//            {
//                return NotFound();
//            }

//            return View(country);
//        }

//        // POST: GeneralConfiguration/Countries/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var country = await _countryRepository.GetByIdAsync(id);
//            if (country != null)
//            {
//                _context.country.Remove(country);
//            }
//            await _context.SaveChangesAsync();

//            TempData["Success"] = "تم الحذف بنجاح";

//            return RedirectToAction(nameof(Index));
//            //return RedirectToAction(nameof(Create));

//        }



//        [HttpPost]
//        public async Task<IActionResult> CheckCountryExists(string country)
//        {
//            var exists = await _context.country.AnyAsync(c => c.Name == country);
//            return Json(new { exists });
//        }

//        [HttpPost]
//        public async Task<IActionResult> SaveCountryData(string country, string notes)
//        {
//            try
//            {
//                var existingCountry = await _context.country.FirstOrDefaultAsync(c => c.Name == country);
//                if (existingCountry != null)
//                {
//                    return Json(new { error = true, message = "اسم الدولة موجود بالفعل في قاعدة البيانات." });
//                }

//                var newCountry = new Country
//                {
//                    Name = country,
//                    Notes = notes
//                };

//                _context.country.Add(newCountry);
//                await _context.SaveChangesAsync();
//                TempData["Success"] = "تم الحفظ بنجاح";

//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
//            }
//        }



//        private bool CountryExists(int id)
//        {
//            return _context.country.Any(e => e.Id == id);
//        }
//    }
//}
