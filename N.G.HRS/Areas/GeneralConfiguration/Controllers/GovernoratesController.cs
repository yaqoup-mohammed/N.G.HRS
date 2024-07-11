using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;
using OfficeOpenXml;
//using N.G.HRS.Areas.GeneralConfiguration.Models;
namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class GovernoratesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Governorate> _governorateRepository;


        public GovernoratesController(AppDbContext context, IRepository<Governorate> governorateRepository)
        {
            _context = context;
            _governorateRepository = governorateRepository;
        }

        // GET: GeneralConfiguration/Governorates
        [Authorize(Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.governorates.Include(g => g.CountryOne);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GeneralConfiguration/Governorates/Details/5
        [Authorize(Policy = "DetailsPolicy")]
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
        [Authorize(Policy = "AddPolicy")]
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
        [Authorize(Policy = "AddPolicy")]
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


        //============================استيراد ملف اكسل الى قاعدة البيانات=======
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> ImportGovernorates(IFormFile file)
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

                        string governorateName = worksheet.Cells[row, 2].Value?.ToString().Trim();
                        string notes = worksheet.Cells[row, 3].Value?.ToString().Trim();
                        string countryName = worksheet.Cells[row, 4].Value?.ToString().Trim();

                        if (!string.IsNullOrEmpty(governorateName) && !string.IsNullOrEmpty(countryName))
                        {
                            // البحث عن الدولة باستخدام اسمها
                            var country = await _context.country.FirstOrDefaultAsync(c => c.Name == countryName);
                            if (country == null)
                            {
                                // إذا لم تكن الدولة موجودة، يمكنك إنشاؤها أو التعامل مع هذا السيناريو بما يتناسب مع تطبيقك
                                ModelState.AddModelError("Country", $"الدولة '{countryName}' غير موجودة في قاعدة البيانات.");
                                continue; // الانتقال إلى السجل التالي في حالة عدم وجود الدولة
                            }

                            // البحث عن المحافظة باستخدام اسمها ومعرف الدولة
                            var existingGovernorate = await _context.governorates
                                .FirstOrDefaultAsync(g => g.Name == governorateName && g.CountryId == country.Id);

                            if (existingGovernorate != null)
                            {
                                // إذا كانت المحافظة موجودة بالفعل، قم بتحديث الملاحظات
                                existingGovernorate.Notes = notes;
                                _context.governorates.Update(existingGovernorate); // تحديث المحافظة في قاعدة البيانات
                            }
                            else
                            {
                                // إذا لم تكن المحافظة موجودة، قم بإضافتها إلى قاعدة البيانات مع معرّف الدولة المحدد
                                var newGovernorate = new Governorate
                                {
                                    Name = governorateName,
                                    Notes = notes,
                                    CountryId = country.Id // تعيين معرّف الدولة
                                };
                                _context.governorates.Add(newGovernorate);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }


        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> ExportToExcelGovernorates()
        {
            var governorates = await _context.governorates.Include(g => g.CountryOne).ToListAsync();

            // Create Excel file
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Required for EPPlus
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Governorates");

                // Headers
                worksheet.Cells["A1"].Value = "ID";
                worksheet.Cells["B1"].Value = "المحافظة";
                worksheet.Cells["C1"].Value = "الملاحظة";
                worksheet.Cells["D1"].Value = "الدولة";

                // Data
                int row = 2;
                foreach (var item in governorates)
                {
                    worksheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                    worksheet.Cells[string.Format("B{0}", row)].Value = item.Name;
                    worksheet.Cells[string.Format("C{0}", row)].Value = item.Notes;
                    worksheet.Cells[string.Format("D{0}", row)].Value = item.CountryOne?.Name ?? "N/A";
                    row++;
                }
                  
                

                // Auto fit columns
                worksheet.Cells.AutoFitColumns();

                // Convert to bytes and return as a file download
                var fileBytes = package.GetAsByteArray();
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Governorates.xlsx");
            }
        }
        // GET: GeneralConfiguration/Governorates/Edit/5

        [Authorize(Policy = "EditPolicy")]
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
        [Authorize(Policy = "EditPolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
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
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var governorate = await _governorateRepository.GetByIdAsync(id);
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
                var existingGovernorates = await _context.governorates.FirstOrDefaultAsync(c => c.Name == governorate);
                if (existingGovernorates != null)
                {
                    return Json(new { error = true, message = "اسم المحافظة موجود بالفعل في قاعدة البيانات." });
                }
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
        [HttpPost]
        public async Task<IActionResult> CheckGovernoratesExists(string governorate)
        {
            var exists = await _context.governorates.AnyAsync(c => c.Name == governorate);
            return Json(new { exists });
        }


        //[HttpGet]
        //public IActionResult GetSections(int departmentId)
        //{
        //    var sections = _context.Sections.Include(x => x.Departments).Where(s => s.DepartmentsId == departmentId).Select(x => new { id = x.Id, name = x.SectionsName }).ToList();
        //    if (sections == null)
        //    {
        //        return NotFound();
        //    }
        //    return Json(new { sections });
        //}


        private bool GovernorateExists(int id)
        {
            return _context.governorates.Any(e => e.Id == id);
        }



    }
}
