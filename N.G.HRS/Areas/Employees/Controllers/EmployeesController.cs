using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Employees.ViewModel;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.Employees.Controllers
{
    [Area("Employees")]

    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Sections> _sectionsrepository;
        private readonly IRepository<Departments> _departmentsrepository;
        private readonly IRepository<JobDescription> _jobDescriptionrepository;
        private readonly IRepository<PracticalExperiences> _practicalExperiencesrepository;
        private readonly IRepository<Family> _familyrepository;
        private readonly IRepository<RelativesType> _relativesTyperepository;

        public EmployeesController(AppDbContext context, IRepository<Employee> employeeRepository,
            IRepository<Sections> sectionsrepository,
            IRepository<Departments> departmentsrepository,
            IRepository<JobDescription> jobDescriptionrepository,
            IRepository<PracticalExperiences> practicalExperiencesrepository,
            IRepository<Family> familyrepository,
            IRepository<RelativesType> relativesTyperepository

            )
        {
            this._context = context;
            this._employeeRepository = employeeRepository;
            this._sectionsrepository = sectionsrepository;
            this._departmentsrepository = departmentsrepository;
            this._jobDescriptionrepository = jobDescriptionrepository;
            this._practicalExperiencesrepository = practicalExperiencesrepository;
            this._familyrepository = familyrepository;
            this._relativesTyperepository = relativesTyperepository;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.employee.Include(e => e.Departments).Include(e => e.FingerprintDevices).Include(e => e.JobDescription).Include(e => e.Manager).Include(e => e.Sections).ToListAsync();

            return View(await appDbContext);
        }
        public async Task< IActionResult> AddEmployee()
        {
           
            //-----------------------Sections---------------------------
            var Sections = await _sectionsrepository.GetAllAsync();
            ViewData["Sections"] =  new SelectList(Sections, "Id", "SectionsName");
            //-----------------------Departments---------------------------
            var Departments = await _departmentsrepository.GetAllAsync();
            ViewData["Departments"] =  new SelectList(Departments, "Id", "SubAdministration");
            //-----------------------JobDescription---------------------------
            var JobDescription = await _jobDescriptionrepository.GetAllAsync();
            ViewData["JobDescription"] =  new SelectList(JobDescription, "Id", "JopName");
            //-----------------------Manager---------------------------
            var Manager = await _employeeRepository.GetAllAsync();
            ViewData["Manager"] =  new SelectList(Manager, "Id", "EmployeeName");

            //-----------------------RelativesType---------------------------
            var RelativesType = await _relativesTyperepository.GetAllAsync();
            ViewData["RelativesType"] = new SelectList(RelativesType, "Id", "RelativeName");



            var viewModel = new EmployeeVM();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(EmployeeVM viewModel )
        {
            
            if (viewModel.Employee!=null)
            {
                await _employeeRepository.AddAsync(viewModel.Employee);

                TempData["Succsess"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(AddEmployee));
            }

            else
            {
                TempData["Error"] = "لم تتم الإضافة هناك خطأ";
            }

            //-----------------------Sections---------------------------
            var Sections = await _sectionsrepository.GetAllAsync();
            ViewData["Sections"] = new SelectList(Sections, "Id", "SectionsName");
            //-----------------------Departments---------------------------
            var Departments = await _departmentsrepository.GetAllAsync();
            ViewData["Departments"] = new SelectList(Departments, "Id", "SubAdministration");
            //-----------------------JobDescription---------------------------
            var JobDescription = await _jobDescriptionrepository.GetAllAsync();
            ViewData["JobDescription"] = new SelectList(JobDescription, "Id", "JopName");
            //-----------------------Manager---------------------------
            var Manager = await _employeeRepository.GetAllAsync();
            ViewData["Manager"] = new SelectList(Manager, "Id", "EmployeeName");


            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPracticalExperiencesToEmployee(EmployeeVM viewModel)
        {

            if (viewModel.PracticalExperiences != null)
            {
                //// تحويل تواريخ البداية والنهاية إلى DateTimeOffset
                //DateTimeOffset fromDateOffset = new DateTimeOffset(viewModel.PracticalExperiences.FromDate.Year,
                //  viewModel.PracticalExperiences.FromDate.Month, viewModel.PracticalExperiences.FromDate.Day, 0, 0, 0, TimeSpan.Zero);

                //DateTimeOffset toDateOffset = new DateTimeOffset(viewModel.PracticalExperiences.ToDate.Year,
                //viewModel.PracticalExperiences.ToDate.Month, viewModel.PracticalExperiences.ToDate.Day,0, 0, 0, TimeSpan.Zero);

                //// حساب الفارق بين التواريخ
                //TimeSpan dateDifference = toDateOffset - fromDateOffset;

                //// تنسيق الفارق بشكل قابل للقراءة
                //string durationFormat = $"{dateDifference.Days} يوم، {dateDifference.Hours} ساعة، {dateDifference.Minutes} دقيقة";

                //// تخزين الفارق في خاصية Duration بتنسيق مخصص
                //viewModel.PracticalExperiences.Duration = durationFormat;

                await _practicalExperiencesrepository.AddAsync(viewModel.PracticalExperiences);

                TempData["Succsess"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(AddEmployee));
            }
            else
            {
                TempData["Error"] = "لم تتم الإضافة هناك خطأ";
            }


            //-----------------------Manager---------------------------
            var Manager = await _employeeRepository.GetAllAsync();
            ViewData["Manager"] = new SelectList(Manager, "Id", "EmployeeName");

            return View("AddEmployee", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFamilyToEmployee(EmployeeVM viewModel)
        {

            if (viewModel.Family != null)
            {
                await _familyrepository.AddAsync(viewModel.Family);

                TempData["Succsess"] = "تم الحفظ بنجاح";
                return RedirectToAction(nameof(AddEmployee));
            }
            else
            {
                TempData["Error"] = "لم تتم الإضافة هناك خطأ";
            }


            //-----------------------Manager---------------------------
            var Manager = await _employeeRepository.GetAllAsync();
            ViewData["Manager"] = new SelectList(Manager, "Id", "EmployeeName");
            //-----------------------RelativesType---------------------------
            var RelativesType = await _relativesTyperepository.GetAllAsync();
            ViewData["RelativesType"] = new SelectList(RelativesType, "Id", "RelativeName");


            return View("AddEmployee", viewModel);
        }
    }
}

