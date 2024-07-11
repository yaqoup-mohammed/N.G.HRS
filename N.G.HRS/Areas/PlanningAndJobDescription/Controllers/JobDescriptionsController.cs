using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.PlanningAndJobDescription.Controllers
{
    [Area("PlanningAndJobDescription")]
    public class JobDescriptionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<JobDescription> _jobDescriptionRepository;

        public JobDescriptionsController(AppDbContext context, IRepository<JobDescription> jobDescriptionRepository)
        {
            _context = context;
            _jobDescriptionRepository = jobDescriptionRepository;
        }

        // GET: PlanningAndJobDescription/JobDescriptions
        [Authorize(Policy = "ViewPolicy")]

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.JobDescription.Include(j => j.FunctionalCategories).Include(j => j.FunctionalClass).Include(j => j.JobRanks);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PlanningAndJobDescription/JobDescriptions/Details/5
        [Authorize(Policy = "DetailsPolicy")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobDescription = await _context.JobDescription
                .Include(j => j.FunctionalCategories)
                .Include(j => j.FunctionalClass)
                .Include(j => j.JobRanks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobDescription == null)
            {
                return NotFound();
            }

            return View(jobDescription);
        }

        // GET: PlanningAndJobDescription/JobDescriptions/Create
        [Authorize(Policy = "AddPolicy")]

        public IActionResult Create()
        {
            ViewData["FunctionalCategoriesId"] = new SelectList(_context.functionalCategories, "Id", "CategoriesName");
            ViewData["FunctionalClassId"] = new SelectList(_context.functionalClasses, "Id", "Name");
            ViewData["JobRanksId"] = new SelectList(_context.jobRanks, "Id", "RankName");
            return View();
        }

        // POST: PlanningAndJobDescription/JobDescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AddPolicy")]
        public async Task<IActionResult> Create([Bind("Id,JopName,JobQualifications,Authorities,Responsibilities,Notes,FunctionalCategoriesId,FunctionalClassId,JobRanksId")] JobDescription jobDescription)
        {
            if (ModelState.IsValid)
            {
              await  _jobDescriptionRepository.AddAsync(jobDescription);
                TempData["Success"] = "تمت الإضافة بنجاح";
                return RedirectToAction(nameof(Create));


                //return RedirectToAction(nameof(Index));
            }
            ViewData["FunctionalCategoriesId"] = new SelectList(_context.functionalCategories, "Id", "CategoriesName", jobDescription.FunctionalCategoriesId);
            ViewData["FunctionalClassId"] = new SelectList(_context.functionalClasses, "Id", "Name", jobDescription.FunctionalClassId);
            ViewData["JobRanksId"] = new SelectList(_context.jobRanks, "Id", "RankName", jobDescription.JobRanksId);
            return View(jobDescription);
        }

        // GET: PlanningAndJobDescription/JobDescriptions/Edit/5
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobDescription = await _jobDescriptionRepository.GetByIdAsync(id);
            if (jobDescription == null)
            {
                return NotFound();
            }
            ViewData["FunctionalCategoriesId"] = new SelectList(_context.functionalCategories, "Id", "CategoriesName", jobDescription.FunctionalCategoriesId);
            ViewData["FunctionalClassId"] = new SelectList(_context.functionalClasses, "Id", "Name", jobDescription.FunctionalClassId);
            ViewData["JobRanksId"] = new SelectList(_context.jobRanks, "Id", "RankName", jobDescription.JobRanksId);
            return View(jobDescription);
        }

        // POST: PlanningAndJobDescription/JobDescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JopName,JobQualifications,Authorities,Responsibilities,Notes,FunctionalCategoriesId,FunctionalClassId,JobRanksId")] JobDescription jobDescription)
        {
            if (id != jobDescription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _jobDescriptionRepository.UpdateAsync(jobDescription);
                    TempData["Success"] = "تم التعديل بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobDescriptionExists(jobDescription.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(jobDescription);
                //return RedirectToAction(nameof(Index));
            }
            ViewData["FunctionalCategoriesId"] = new SelectList(_context.functionalCategories, "Id", "CategoriesName", jobDescription.FunctionalCategoriesId);
            ViewData["FunctionalClassId"] = new SelectList(_context.functionalClasses, "Id", "Name", jobDescription.FunctionalClassId);
            ViewData["JobRanksId"] = new SelectList(_context.jobRanks, "Id", "RankName", jobDescription.JobRanksId);
            return View(jobDescription);
        }

        // GET: PlanningAndJobDescription/JobDescriptions/Delete/5
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobDescription = await _context.JobDescription
                .Include(j => j.FunctionalCategories)
                .Include(j => j.FunctionalClass)
                .Include(j => j.JobRanks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobDescription == null)
            {
                return NotFound();
            }

            return View(jobDescription);
        }

        // POST: PlanningAndJobDescription/JobDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeletePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobDescription = await _jobDescriptionRepository.GetByIdAsync(id);
            if (jobDescription != null)
            {
                _context.JobDescription.Remove(jobDescription);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "تم الحذف بنجاح";
            return RedirectToAction(nameof(Create));

            //return RedirectToAction(nameof(Index));
        }

        private bool JobDescriptionExists(int id)
        {
            return _context.JobDescription.Any(e => e.Id == id);
        }
    }
}
