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
    public class ContractTermsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<ContractTerms> _contractTermsRepository;

        public ContractTermsController(AppDbContext context, IRepository<ContractTerms> contractTermsRepository)
        {
            _context = context;
            _contractTermsRepository = contractTermsRepository;
        }

        // GET: GeneralConfiguration/ContractTerms
        [Authorize (Policy = "ViewPolicy")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.contractTerms.Include(c => c.Contracts);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GeneralConfiguration/ContractTerms/Details/5
        [Authorize(Policy = "DetailsPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var contractTerms = await _context.contractTerms
                .Include(c => c.Contracts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contractTerms == null)
            {
                return NotFound();
            }

            return View(contractTerms);
        }

        // GET: GeneralConfiguration/ContractTerms/Create
        [Authorize(Policy = "AddPolicy")]
        public IActionResult Create()
        {
            ViewData["ContractsId"] = new SelectList(_context.contracts, "Id", "Name");
            return View();
        }

        // POST: GeneralConfiguration/ContractTerms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,ModelName,StatementOfConditions,Notes,ContractsId")] ContractTerms contractTerms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contractTerms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractsId"] = new SelectList(_context.contracts, "Id", "Name", contractTerms.ContractsId);
            return View(contractTerms);
        }

        // GET: GeneralConfiguration/ContractTerms/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractTerms = await _context.contractTerms.FindAsync(id);
            if (contractTerms == null)
            {
                return NotFound();
            }
            ViewData["ContractsId"] = new SelectList(_context.contracts, "Id", "Name", contractTerms.ContractsId);
            return View(contractTerms);
        }

        // POST: GeneralConfiguration/ContractTerms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModelName,StatementOfConditions,Notes,ContractsId")] ContractTerms contractTerms)
        {
            if (id != contractTerms.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractTerms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractTermsExists(contractTerms.Id))
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
            ViewData["ContractsId"] = new SelectList(_context.contracts, "Id", "Name", contractTerms.ContractsId);
            return View(contractTerms);
        }

        // GET: GeneralConfiguration/ContractTerms/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractTerms = await _context.contractTerms
                .Include(c => c.Contracts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contractTerms == null)
            {
                return NotFound();
            }

            return View(contractTerms);
        }



        // POST: GeneralConfiguration/ContractTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contractTerms = await _context.contractTerms.FindAsync(id);
            if (contractTerms != null)
            {
                _context.contractTerms.Remove(contractTerms);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractTermsExists(int id)
        {
            return _context.contractTerms.Any(e => e.Id == id);
        }
    }
}
