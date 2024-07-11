using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.Finance.Controllers
{
    [Area("Finance")]
    public class FinanceAccountTypesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<FinanceAccountType> _repositoryRepository;

        public FinanceAccountTypesController(AppDbContext context, IRepository<FinanceAccountType> repositoryRepository)
        {
            _context = context;
            _repositoryRepository = repositoryRepository;
        }

        // GET: Finance/FinanceAccountTypes
        [Authorize(Policy = "ViewPolicy")]


        public async Task<IActionResult> Index()
        {
            return View(await _context.FinanceAccountType.ToListAsync());
        }

        // GET: Finance/FinanceAccountTypes/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financeAccountType = await _context.FinanceAccountType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financeAccountType == null)
            {
                return NotFound();
            }

            return View(financeAccountType);
        }

        // GET: Finance/FinanceAccountTypes/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Finance/FinanceAccountTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]

        public async Task<IActionResult> Create([Bind("Id,Name,Type,AccountNumber,Description")] FinanceAccountType financeAccountType)
        {
            if (ModelState.IsValid)
            {
               await _repositoryRepository.AddAsync(financeAccountType);
                TempData["Success"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(Index));
            }
            return View(financeAccountType);
        }

        // GET: Finance/FinanceAccountTypes/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financeAccountType = await _repositoryRepository.GetByIdAsync(id);
            if (financeAccountType == null)
            {
                return NotFound();
            }
            return View(financeAccountType);
        }

        // POST: Finance/FinanceAccountTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,AccountNumber,Description")] FinanceAccountType financeAccountType)
        {
            if (id != financeAccountType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financeAccountType);
                    TempData["Success"] = "تم التعديل بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinanceAccountTypeExists(financeAccountType.Id))
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
            return View(financeAccountType);
        }

        // GET: Finance/FinanceAccountTypes/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financeAccountType = await _context.FinanceAccountType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financeAccountType == null)
            {
                return NotFound();
            }

            return View(financeAccountType);
        }

        // POST: Finance/FinanceAccountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financeAccountType = await _repositoryRepository.GetByIdAsync(id);
            if (financeAccountType != null)
            {
                _context.FinanceAccountType.Remove(financeAccountType);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Index));
        }

        private bool FinanceAccountTypeExists(int id)
        {
            return _context.FinanceAccountType.Any(e => e.Id == id);
        }
    }
}
