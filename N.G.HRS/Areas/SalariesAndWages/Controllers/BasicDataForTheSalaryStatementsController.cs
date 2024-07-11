using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AalariesAndWages.Models;
using N.G.HRS.Date;
using Microsoft.AspNetCore.Authorization;

namespace N.G.HRS.Areas.SalariesAndWages.Controllers
{
    [Area("SalariesAndWages")]
    public class BasicDataForTheSalaryStatementsController : Controller
    {
        private readonly AppDbContext _context;

        public BasicDataForTheSalaryStatementsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SalariesAndWages/BasicDataForTheSalaryStatements
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.basicDataForTheSalaryStatements.ToListAsync());
        }

        // GET: SalariesAndWages/BasicDataForTheSalaryStatements/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicDataForTheSalaryStatement = await _context.basicDataForTheSalaryStatements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basicDataForTheSalaryStatement == null)
            {
                return NotFound();
            }

            return View(basicDataForTheSalaryStatement);
        }

        // GET: SalariesAndWages/BasicDataForTheSalaryStatements/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: SalariesAndWages/BasicDataForTheSalaryStatements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]


        public async Task<IActionResult> Create([Bind("Id,HealthInsuranceIncluded,RetirementInsuranceIncluded,IncludesTheWorkShareInRetirementInsurance,IncludesTaxCalculation,TaxFrom,AllowancesIncluded,IncludesAdditionalData,FromDate,ToDate,Notes,Percentage,PercentageOnEmployee,PercentageOnCompany")] BasicDataForTheSalaryStatement basicDataForTheSalaryStatement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basicDataForTheSalaryStatement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(basicDataForTheSalaryStatement);
        }

        // GET: SalariesAndWages/BasicDataForTheSalaryStatements/Edit/5
        [Authorize(Policy = "EditPolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicDataForTheSalaryStatement = await _context.basicDataForTheSalaryStatements.FindAsync(id);
            if (basicDataForTheSalaryStatement == null)
            {
                return NotFound();
            }
            return View(basicDataForTheSalaryStatement);
        }

        // POST: SalariesAndWages/BasicDataForTheSalaryStatements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HealthInsuranceIncluded,RetirementInsuranceIncluded,IncludesTheWorkShareInRetirementInsurance,IncludesTaxCalculation,TaxFrom,AllowancesIncluded,IncludesAdditionalData,FromDate,ToDate,Notes,Percentage,PercentageOnEmployee,PercentageOnCompany")] BasicDataForTheSalaryStatement basicDataForTheSalaryStatement)
        {
            if (id != basicDataForTheSalaryStatement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basicDataForTheSalaryStatement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasicDataForTheSalaryStatementExists(basicDataForTheSalaryStatement.Id))
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
            return View(basicDataForTheSalaryStatement);
        }

        // GET: SalariesAndWages/BasicDataForTheSalaryStatements/Delete/5
        [Authorize(Policy = "DeletePolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basicDataForTheSalaryStatement = await _context.basicDataForTheSalaryStatements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basicDataForTheSalaryStatement == null)
            {
                return NotFound();
            }

            return View(basicDataForTheSalaryStatement);
        }

        // POST: SalariesAndWages/BasicDataForTheSalaryStatements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basicDataForTheSalaryStatement = await _context.basicDataForTheSalaryStatements.FindAsync(id);
            if (basicDataForTheSalaryStatement != null)
            {
                _context.basicDataForTheSalaryStatements.Remove(basicDataForTheSalaryStatement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasicDataForTheSalaryStatementExists(int id)
        {
            return _context.basicDataForTheSalaryStatements.Any(e => e.Id == id);
        }
    }
}
