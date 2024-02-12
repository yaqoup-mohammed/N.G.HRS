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
using NuGet.Protocol.Core.Types;
using System.Drawing;

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
        private readonly IRepository<PersonalData> _personalDatarepository;
        private readonly IRepository<Guarantees> _guaranteesrepository;
        private readonly IRepository<Sex> _sexrepository;
        private readonly IRepository<Religion> _religionrepository;
        private readonly IRepository<Nationality> _nationalityrepository;
        private readonly IRepository<MaritalStatus> _maritalStatusrepository;

        public EmployeesController(AppDbContext context, IRepository<Employee> employeeRepository,
            IRepository<Sections> sectionsrepository,
            IRepository<Departments> departmentsrepository,
            IRepository<JobDescription> jobDescriptionrepository,
            IRepository<PracticalExperiences> practicalExperiencesrepository,
            IRepository<Family> familyrepository,
            IRepository<RelativesType> relativesTyperepository,
            IRepository<PersonalData> PersonalDatarepository,
            IRepository<Guarantees> Guaranteesrepository,
            IRepository<Sex> Sexrepository,
            IRepository<Religion> Religionrepository,
            IRepository<Nationality> Nationalityrepository,
            IRepository<MaritalStatus> MaritalStatusrepository

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
            this._personalDatarepository = PersonalDatarepository;

            this._guaranteesrepository = Guaranteesrepository;
            this._sexrepository = Sexrepository;
            this._religionrepository = Religionrepository;
            this._nationalityrepository = Nationalityrepository;
            this._maritalStatusrepository = MaritalStatusrepository;
        }

        public async Task<IActionResult> Index()
        {
            // Populate ViewData for dropdown lists
            await PopulateDropdownListsAsync();

            var employees  =await _context.employee.Include(e => e.Departments)
                .Include(e => e.FingerprintDevices).Include(e => e.JobDescription)
                .Include(e => e.Manager).Include(e => e.Sections).ToListAsync();
            //-------------------------------------------------------
            var practicalExperiences = await _context.practicalExperiences
                .Include(p => p.Employee).ToListAsync();
            //-------------------------------------------------------
            var family = await _context.Family.Include(f => f.Employees)
                .Include(f => f.RelativesType).ToListAsync();
            //-------------------------------------------------------
            var personalData = await _context.personalDatas.Include(p => p.MaritalStatus)
                .Include(p => p.Nationality).Include(p => p.Religion).Include(p => p.Sex)
                .Include(p => p.employee).Include(p => p.guarantees).ToListAsync();
            //-------------------------------------------------------

            if (personalData == null|| family == null || practicalExperiences == null || employees == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = new EmployeeVM
                {
                    EmployeeList = employees,
                    PracticalExperiencesList = practicalExperiences,
                    FamilyList = family,
                    PersonalDataList = personalData,

                };
                return View(viewModel);
            }
  
        }
        // Example with English naming convention
        public async Task<IActionResult> AddOrEditEmployee(int? id)
        {
            await PopulateDropdownListsAsync();
            var viewModel = new EmployeeVM
            {
                Employee = await _employeeRepository.GetByIdAsync(id),
            };

            if (id == null)
            {
                return View(viewModel);
            }
            else
            {
                var employeeViewModel = new EmployeeVM
                {
                    PersonalData = await _personalDatarepository.GetByIdAsync(id),
                    Employee = await _employeeRepository.GetByIdAsync(id),
                    Family = await _familyrepository.GetByIdAsync(id),
                    PracticalExperiences = await _practicalExperiencesrepository.GetByIdAsync(id)
                };
              return View(employeeViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditEmployee(int? id, EmployeeVM viewModel)
        {
            await PopulateDropdownListsAsync();

            if (viewModel != null)
            {
                if (id != null)
                {
                    if (id != viewModel.Employee.Id)
                    {
                        return NotFound();
                    }

                    var existingEntity = await _employeeRepository.GetByIdAsync(id);
                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    // Now you can safely attach the updated entity
                    await _employeeRepository.UpdateAsync(viewModel.Employee);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _employeeRepository.AddAsync(viewModel.Employee);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddOrEditEmployee));
                }
            }
            else
            {
                TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
            }

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditPracticalExperiencesToEmployee(int? id  ,EmployeeVM viewModel)
        {
            // Populate ViewData for dropdown lists
            await PopulateDropdownListsAsync();

            if (viewModel != null)
            {
                if (id != null)
                {
                    if (id != viewModel.PracticalExperiences.Id)
                    {
                        return NotFound();
                    }

                    var existingEntity = await _practicalExperiencesrepository.GetByIdAsync(id);
                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    // Now you can safely attach the updated entity
                    await _practicalExperiencesrepository.UpdateAsync(viewModel.PracticalExperiences);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _practicalExperiencesrepository.AddAsync(viewModel.PracticalExperiences);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddOrEditEmployee));
                }
            }
            else
            {
                TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
            }

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditFamilyToEmployee(int? id,EmployeeVM viewModel)
        {
            // Populate ViewData for dropdown lists
            await PopulateDropdownListsAsync();

            if (viewModel != null)
            {
                if (id != null)
                {
                    if (id != viewModel.Family.Id)
                    {
                        return NotFound();
                    }

                    var existingEntity = await _familyrepository.GetByIdAsync(id);
                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    // Now you can safely attach the updated entity
                    await _familyrepository.UpdateAsync(viewModel.Family);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _familyrepository.AddAsync(viewModel.Family);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddOrEditEmployee));
                }
            }
            else
            {
                TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditPersonalDataToEmployee(int? id, EmployeeVM viewModel)
        {
            // Populate ViewData for dropdown lists
            await PopulateDropdownListsAsync();

            if (viewModel != null)
            {
                if (id != null)
                {
                    if (id != viewModel.PersonalData.Id)
                    {
                        return NotFound();
                    }

                    var existingEntity = await _personalDatarepository.GetByIdAsync(id);
                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).State = EntityState.Detached;
                    }

                    // Now you can safely attach the updated entity
                    await _personalDatarepository.UpdateAsync(viewModel.PersonalData);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _personalDatarepository.AddAsync(viewModel.PersonalData);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddOrEditEmployee));
                }
            }
            else
            {
                TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
            }

            return View(viewModel);
        }
        private async Task PopulateDropdownListsAsync()
        {
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

            //-----------------------RelativesType---------------------------
            var RelativesType = await _relativesTyperepository.GetAllAsync();
            ViewData["RelativesType"] = new SelectList(RelativesType, "Id", "RelativeName");

            //----------------------------------------------------------------
            //-----------------------MaritalStatus---------------------------
            var MaritalStatus = await _maritalStatusrepository.GetAllAsync();
            ViewData["MaritalStatus"] = new SelectList(MaritalStatus, "Id", "Name");
            //-----------------------Nationality---------------------------
            var Nationality = await _nationalityrepository.GetAllAsync();
            ViewData["Nationality"] = new SelectList(Nationality, "Id", "NationalityName");
            //-----------------------Religion---------------------------
            var Religion = await _religionrepository.GetAllAsync();
            ViewData["Religion"] = new SelectList(Religion, "Id", "Name");
            //-----------------------Sex---------------------------
            var Sex = await _sexrepository.GetAllAsync();
            ViewData["Sex"] = new SelectList(Sex, "Id", "Name");
            //===============================================================================================
            //---------------GuaranteesOne-------- One to One الجداول الذي العلاقة بينها--------------------------
            // استرجاع كل الضمانات
            var GuaranteesOne = await _guaranteesrepository.GetAllAsync();
            // تحقق من عدم وجودها في جدول PersonalData
            var filteredGuarantees = GuaranteesOne.Where(g => !_context.personalDatas.Any(pd => pd.GuaranteesId == g.Id));
            // إذا كانت الضمانات غير موجودة في جدول PersonalData، قم بإضافتها إلى SelectList
            ViewData["GuaranteesOne"] = new SelectList(filteredGuarantees, "Id", "Name");
            //===============================================================================================
            //---------------ManagerOne-------- One to One الجداول الذي العلاقة بينها-------------------------
            // استرجاع كل الموظفين
            var ManagerOne = await _employeeRepository.GetAllAsync();
            // تحقق من عدم وجودها في جدول PersonalData
            var filteredManage = ManagerOne.Where(g => !_context.personalDatas.Any(pd => pd.EmployeeId == g.Id));
            ViewData["ManagerOne"] = new SelectList(filteredManage, "Id", "EmployeeName");
            //===============================================================================================

        }
      
   }
}

