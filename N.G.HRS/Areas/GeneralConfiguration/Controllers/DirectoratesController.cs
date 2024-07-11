using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Date;
using OfficeOpenXml;

namespace N.G.HRS.Areas.GeneralConfiguration.Controllers
{
    [Area("GeneralConfiguration")]
    public class DirectoratesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DirectoratesController> _logger;

        public DirectoratesController(AppDbContext context, ILogger<DirectoratesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: GeneralConfiguration/Directorates
        [Authorize (policy: "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.directorates.Include(d => d.Governorate);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GeneralConfiguration/Directorates/Details/5
        [Authorize(policy: "ViewPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorate = await _context.directorates
                .Include(d => d.Governorate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directorate == null)
            {
                return NotFound();
            }

            return View(directorate);
        }

        // GET: GeneralConfiguration/Directorates/Create
        [Authorize(policy: "AddPolicy")]
        public IActionResult Create()
        {
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name");
            return View();
        }

        // POST: GeneralConfiguration/Directorates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(policy: "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes,GovernorateId")] Directorate directorate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(directorate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name", directorate.GovernorateId);
            return View(directorate);
        }


        [HttpPost]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> ImportDirectorate(IFormFile file)
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
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        ModelState.AddModelError("File", "الملف لا يحتوي على أوراق عمل.");
                        return View("Import");
                    }

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // تجاوز الصف الأول بسبب العنوان
                    {
                        var directorateName = worksheet.Cells[row, 2].Value?.ToString().Trim();
                        var notes = worksheet.Cells[row, 3].Value?.ToString().Trim();
                        var governorateName = worksheet.Cells[row, 4].Value?.ToString().Trim();

                        if (!string.IsNullOrEmpty(directorateName) && !string.IsNullOrEmpty(governorateName))
                        {
                            var governorate = await _context.governorates.FirstOrDefaultAsync(g => g.Name == governorateName);
                            if (governorate == null)
                            {
                                ModelState.AddModelError("Governorate", $"المحافظة '{governorateName}' غير موجودة في قاعدة البيانات.");
                                continue;
                            }

                            var existingDirectorate = await _context.directorates
                                .FirstOrDefaultAsync(d => d.Name == directorateName && d.GovernorateId == governorate.Id);

                            if (existingDirectorate != null)
                            {
                                existingDirectorate.Notes = notes;
                                _context.directorates.Update(existingDirectorate);
                            }
                            else
                            {
                                var newDirectorate = new Directorate
                                {
                                    Name = directorateName,
                                    Notes = notes,
                                    GovernorateId = governorate.Id
                                };
                                _context.directorates.Add(newDirectorate);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Row", $"الصف {row} يحتوي على بيانات غير مكتملة.");
                        }
                    }

                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "تم استيراد البيانات بنجاح!";
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "addPolicy")]
        public async Task<IActionResult> ExportToExcelDirectorate()
        {
            // Retrieve data from database including Governorate name
            var data = await _context.directorates
                                    .Include(d => d.Governorate) // Assuming there's a navigation property to Governorate
                                    .ToListAsync();

            // Set file name and path
            var fileName = "DirectorateData.xlsx";
            var filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            // Create Excel package using EPPlus
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Check if worksheet already exists, if yes, remove it
                var existingWorksheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "Directorate");
                if (existingWorksheet != null)
                {
                    package.Workbook.Worksheets.Delete(existingWorksheet);
                }

                // Add a new worksheet
                var worksheet = package.Workbook.Worksheets.Add("Directorate");

                // Add headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "المديرية";
                worksheet.Cells[1, 3].Value = "الملاحظة";
                worksheet.Cells[1, 4].Value = "المحافظة";

                // Add data to cells
                int row = 2;
                foreach (var item in data)
                {


                    worksheet.Cells[row, 1].Value = item.Id;

                    worksheet.Cells[row, 2].Value = item.Name;
                    worksheet.Cells[row, 3].Value = item.Notes;
                    worksheet.Cells[row, 4].Value = item.Governorate.Name; // Use Governorate name here
                    row++;
                }

                // Save the package
                package.Save();
            }

            // Download the file
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        // GET: GeneralConfiguration/Directorates/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorate = await _context.directorates.FindAsync(id);
            if (directorate == null)
            {
                return NotFound();
            }
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name", directorate.GovernorateId);
            return View(directorate);
        }

        // POST: GeneralConfiguration/Directorates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Notes,GovernorateId")] Directorate directorate)
        {
            if (id != directorate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(directorate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorateExists(directorate.Id))
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
            ViewData["GovernorateId"] = new SelectList(_context.governorates, "Id", "Name", directorate.GovernorateId);
            return View(directorate);
        }

        // GET: GeneralConfiguration/Directorates/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorate = await _context.directorates
                .Include(d => d.Governorate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directorate == null)
            {
                return NotFound();
            }

            return View(directorate);
        }

        // POST: GeneralConfiguration/Directorates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var directorate = await _context.directorates.FindAsync(id);
            if (directorate != null)
            {
                _context.directorates.Remove(directorate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> SaveDirectorateyData(string directorate, string notes, int governorateId)
        {
            try
            {
                var checkDirectorateyExists = await _context.directorates.FirstOrDefaultAsync(c => c.Name == directorate);
                if (checkDirectorateyExists != null)
                {
                    return Json(new { error = true, message = "اسم المديرية موجود بالفعل في قاعدة البيانات." });
                }
                // إنشاء كائن دولة لتخزين البيانات المستلمة
                var newDirectorate = new Directorate
                {
                    Name = directorate,
                    Notes = notes,
                    GovernorateId = governorateId
                };

                // إضافة الدولة الجديدة إلى قاعدة البيانات باستخدام Entity Framework Core
                _context.directorates.Add(newDirectorate);
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
        public async Task<IActionResult> CheckDirectorateyExists(string directorate)
        {
            var exists = await _context.directorates.AnyAsync(c => c.Name == directorate );
            return Json(new { exists });
        }

        private bool DirectorateExists(int id)
        {
            return _context.directorates.Any(e => e.Id == id);
        }
    }
}
