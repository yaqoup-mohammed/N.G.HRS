using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.ViewModels;
using N.G.HRS.Areas.Employees.ViewModel;
using N.G.HRS.Date;
using N.G.HRS.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Controllers
{
    [Area("AttendanceAndDeparture")]
    public class PermananceAndPeriodsVMController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IRepository<PermanenceModels> _permanenceModelsRepository;
        private readonly IRepository<Periods> _periodsRepository;
        public PermananceAndPeriodsVMController(AppDbContext appDbContext, IRepository<PermanenceModels> permanenceModelsRepository, IRepository<Periods> periodsRepository)
        {
            this._appDbContext = appDbContext;
            this._periodsRepository = periodsRepository;
            this._permanenceModelsRepository = permanenceModelsRepository;

        }
        public async Task< IActionResult> Index()
        {

            await PopulateDropdownListsAsync();
            var periods = await _appDbContext.periods.Include(e => e.PermanenceModels).ToListAsync();
            var permmenance = await _appDbContext.permanenceModels.ToListAsync();

            var permmenanceVM = new PermanenceModelsAndPeriodsVM
            {
                permanenceModelsList = permmenance,
                periodsList = periods,
            };

            return View(permmenanceVM);
        }
        //public async Task<IActionResult> Create()
        //{

        //    return View();
        //}
        public async Task   <IActionResult> Create()
        {
            await PopulateDropdownListsAsync();
            var viewModel = new PermanenceModelsAndPeriodsVM();

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PermanenceModelsAndPeriodsVM PVM)
        {

           if(ModelState.IsValid)
            {
                try
                {
                        await PopulateDropdownListsAsync();

                    
                    if (PVM.permanenceModels != null)
                    {
                        if(PVM.permanenceModels.FromDate >  PVM.permanenceModels.ToDate)
                        {
                            TempData["Error"] = "يجب ان يكون تاريخ الانتهاء اكبر من تاريخ البدء";
                            return View(PVM);
                        }
                        await _permanenceModelsRepository.AddAsync(PVM.permanenceModels);
                        //================================================
                        TempData["Success"] = "تم الحفظ بنجاح";
                        return RedirectToAction(nameof(Create)); // Redirect to the Create action();
                        //return RedirectToAction(nameof(Index));


                    }
                    else
                    {
                        TempData["Error"] = "لم تتم الإضافة، هناك خطأ";

                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it accordingly
                    TempData["Error"] = ex.Message;
                    TempData["Error"] = "حدث خطأ أثناء محاولة إضافة الموظف";
                }
            }

            return View(PVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePeriods(PermanenceModelsAndPeriodsVM data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (data.periods != null )
                    {
                        // Assuming PopulateDropdownListsAsync is implemented elsewhere:
                        await PopulateDropdownListsAsync(); // Call if necessary


                        // Deserialization (remove unnecessary ToString()):
                        //var periodsList = JsonConvert.DeserializeObject<List<Periods>>(data.periods.ToString());
                        //foreach (var period in periodsList)
                        //{

                            await _periodsRepository.AddAsync(data.periods);
                        //}
                        

                        TempData["Success"] = "تم الحفظ بنجاح"; // Success message

                        return RedirectToAction(nameof(Create));
                    }
                    else
                    {
                        TempData["Error"] = "لم تتم الإضافة، لم يتم إرسال بيانات الفترات"; // Specific error message
                    }
                }
                catch (Exception ex)
                { 
                    // Log the exception and provide more informative error message
                    TempData["Error"] = "حدث خطأ أثناء محاولة إضافة الفترات"; // Generic user message

                    TempData["Error"] = ex.Message; // Log for debugging
                }
            }

            return View(data); // Return view with data if validation fails or an error occurs
        }
        private async Task PopulateDropdownListsAsync()
        {

            var periods = await _appDbContext.periods.ToListAsync();
            ViewData["Periods"] = new SelectList(periods, "Id", "PeriodsName");
            var permanance = await _appDbContext.permanenceModels.ToListAsync();
            ViewData["permanance"] = new SelectList(permanance, "Id", "PermanenceName");

        }
    }
}
