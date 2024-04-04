using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.Employees.Models;
using N.G.HRS.Areas.Employees.ViewModel;
using N.G.HRS.Areas.Finance.Models;
using N.G.HRS.Areas.GeneralConfiguration.Models;
using N.G.HRS.Areas.OrganizationalChart.Models;
using N.G.HRS.Areas.PlanningAndJobDescription.Models;
using N.G.HRS.Date;
using N.G.HRS.HRSelectList;
using N.G.HRS.Repository;
using N.G.HRS.Repository.File_Upload;
using NuGet.Protocol.Core.Types;
using System.Drawing;

namespace N.G.HRS.Areas.Employees.Controllers
{
    [Area("Employees")]

    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileUploadService _fileUploadService;
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
        //private readonly IRepository<EducationalQualification> _educationalQualificationrepository;
        //private readonly IRepository<Specialties> _specialtiesrepository;
        //private readonly IRepository<Universities> _universitiesrepository;
        //private readonly IRepository<Qualifications> _qualificationsrepository;
        private readonly IRepository<MaritalStatus> _maritalStatusrepository;
        private readonly IRepository<Currency> _currencyrepository;
        private readonly IRepository<TrainingCourses> _trainingCoursesrepository;
        private readonly IRepository<EmployeeArchives> _employeeArchivesrepository;
        private readonly IRepository<FinancialStatements> _financialStatementsrepository;

        public EmployeesController(AppDbContext context, IFileUploadService fileUploadService, IRepository<Employee> employeeRepository,
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
            //IRepository<EducationalQualification> EducationalQualificationrepository,
            //IRepository<Specialties> Specialtiesrepository,
            //IRepository<Universities> Universitiesrepository,
            //IRepository<Qualifications> Qualificationsrepository,
            IRepository<MaritalStatus> MaritalStatusrepository,
            IRepository<Currency> Currencyrepository,
            IRepository<TrainingCourses> TrainingCoursesrepository,
            IRepository<EmployeeArchives> EmployeeArchivesrepository,
            IRepository<FinancialStatements> FinancialStatementsrepository

            )
        {
            this._context = context;
            this._fileUploadService = fileUploadService;
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
            //this._educationalQualificationrepository = EducationalQualificationrepository;
            //this._specialtiesrepository = Specialtiesrepository;
            //this._universitiesrepository = Universitiesrepository;
            //this._qualificationsrepository = Qualificationsrepository;
            this._maritalStatusrepository = MaritalStatusrepository;
            this._currencyrepository = Currencyrepository;
            this._trainingCoursesrepository = TrainingCoursesrepository;
            this._employeeArchivesrepository = EmployeeArchivesrepository;
            this._financialStatementsrepository = FinancialStatementsrepository;
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
            var guarantees = await _context.guarantees.Include(g => g.MaritalStatus).ToListAsync();
            //-------------------------------------------------------
            var FinancialStatements = await _context.financialStatements.Include(f => f.Currency).Include(f => f.employee).ToListAsync();
            //-------------------------------------------------------
            var TrainingCourses = await _context.trainingCourses.Include(t => t.EmployeeOne).ToListAsync();

            //-------------------------------------------------------
            var EmployeeArchives = await _context.EmployeeArchives.Include(e => e.employee).ToListAsync();

            //-------------------------------------------------------


            var viewModel = new EmployeeVM
                {
                    EmployeeList = employees,
                    PracticalExperiencesList = practicalExperiences,
                    FamilyList = family,
                    PersonalDataList = personalData,
                    guaranteesList = guarantees,
                    FinancialStatementsList = FinancialStatements,
                    TrainingCoursesList = TrainingCourses,
                    EmployeeArchivesList = EmployeeArchives,

                };
                return View(viewModel);
            
  
        }
        // Example with English naming convention
        public async Task<IActionResult> AddEmployee()
        {
            await PopulateDropdownListsAsync();
            var viewModel = new EmployeeVM();
             
                return View(viewModel);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(EmployeeVM viewModel)
        {
            try
            {
                await PopulateDropdownListsAsync();

                if (viewModel.Employee != null)
                {
                    var exist = _context.employee.Any(e => e.EmployeeNumber == viewModel.Employee.EmployeeNumber);
                    if (!exist)
                    {
                        await _employeeRepository.AddAsync(viewModel.Employee);
                        TempData["Success"] = "تم الحفظ بنجاح";
                        return RedirectToAction(nameof(AddEmployee));
                    }
                    else
                    {
                        TempData["Error"] = "الرقم الوظيفي موجود بالفعل";
                        return View(viewModel);
                    }
    
                }
                else
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["SystemError"]=ex.Message;  
                return View(viewModel);
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPracticalExperiencesToEmployee(EmployeeVM viewModel)
        {
            try
            {
                // Populate ViewData for dropdown lists
                await PopulateDropdownListsAsync();

                if (viewModel.PracticalExperiences != null)
                {
                    await _practicalExperiencesrepository.AddAsync(viewModel.PracticalExperiences);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddEmployee));
                }
                else
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["Error"] = "حدث خطأ أثناء محاولة إضافة الخبرات العملية للموظف";
            }

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFamilyToEmployee(EmployeeVM viewModel)
        {
            try
            {
                // Populate ViewData for dropdown lists
                await PopulateDropdownListsAsync();

                if (viewModel.Family != null)
                {
                    await _familyrepository.AddAsync(viewModel.Family);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddEmployee));
                }
                else
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["Error"] = "حدث خطأ أثناء محاولة إضافة العائلة للموظف";
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPersonalDataToEmployee( EmployeeVM viewModel)
        {
            try
            {
                // Populate ViewData for dropdown lists
                await PopulateDropdownListsAsync();

                if (viewModel.PersonalData != null)
                {
                    await _personalDatarepository.AddAsync(viewModel.PersonalData);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddEmployee));
                }
                else
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["Error"] = "حدث خطأ أثناء محاولة إضافة البيانات الشخصية للموظف";
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGuarantees( EmployeeVM viewModel)
        {
            try
            {
                // Populate ViewData for dropdown lists
                await PopulateDropdownListsAsync();

                if (viewModel.Guarantees != null)
                {
                    await _guaranteesrepository.AddAsync(viewModel.Guarantees);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddEmployee));
                }
                else
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["Error"] = "حدث خطأ أثناء محاولة إضافة الضمانات للموظف";
            }

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFinancialStatements(EmployeeVM viewModel)
        {
            try
            {
                // Populate ViewData for dropdown lists
                await PopulateDropdownListsAsync();

                if (viewModel.FinancialStatements != null)
                {
                    await _financialStatementsrepository.AddAsync(viewModel.FinancialStatements);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddEmployee));
                }
                else
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["Error"] = "حدث خطأ أثناء محاولة إضافة البيانات المالية للموظف";
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTrainingCourses(EmployeeVM viewModel)
        {
            try
            {
                // Populate ViewData for dropdown lists
                await PopulateDropdownListsAsync();

                if (viewModel.TrainingCourses != null)
                {
                    await _trainingCoursesrepository.AddAsync(viewModel.TrainingCourses);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddEmployee));
                }
                else
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["Error"] = "حدث خطأ أثناء محاولة إضافة البيانات الدورات للموظف";
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployeeArchives(EmployeeVM viewModel)
        {
            try
            {
                // Populate ViewData for dropdown lists
                await PopulateDropdownListsAsync();

                if (viewModel.EmployeeArchives != null)
                {
                    // تحميل الملف باستخدام خدمة التحميل الملفات
                    var file = viewModel.EmployeeArchives.FileUpload;
                    var filePath = await _fileUploadService.UploadFileAsync(file, "Upload/PDF");
                    viewModel.EmployeeArchives.File = filePath;
                    await _employeeArchivesrepository.AddAsync(viewModel.EmployeeArchives);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(AddEmployee));
                }
                else
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["Error"] = "حدث خطأ أثناء محاولة إضافة البيانات الارشيف للموظف";
            }

            return View(viewModel);
        }
        public bool EmployeeNumber(string id)
        {
            if (id != null)
            {
                var employeeNumber = _context.employee.Any(e => e.EmployeeNumber == id);
                if (!employeeNumber)
                {
                    return true;                }
            }
            return false;
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
            //-----------------------Guarantees---------------------------
            var Guarantees = await _guaranteesrepository.GetAllAsync();
            ViewData["Guarantees"] = new SelectList(Guarantees, "Id", "Name");
               //-----------------------Guarantees---------------------------
            var Currency = await _currencyrepository.GetAllAsync();
            ViewData["Currency"] = new SelectList(Currency, "Id", "CurrencyName");

            ////-----------------------educationalQualificationr---------------------------
            //var educationalQualificationr = await _educationalQualificationrepository.GetAllAsync();
            //ViewData["educationalQualificationr"] = new SelectList(educationalQualificationr, "Id", "Name");
            ////-----------------------specialties---------------------------
            //var specialties = await _specialtiesrepository.GetAllAsync();
            //ViewData["specialties"] = new SelectList(specialties, "Id", "Name");
            ////-----------------------universities---------------------------
            //var universities = await _universitiesrepository.GetAllAsync();
            //ViewData["universities"] = new SelectList(universities, "Id", "Name");

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
            List<EmployeeStatusList> employeeStatus = new List<EmployeeStatusList>
            {
                new EmployeeStatusList () { id = 1, name = "مثبت" },
                new EmployeeStatusList () { id = 2, name = "متعاقد" },
                new EmployeeStatusList () { id = 3, name = "متدرب" },
                new EmployeeStatusList () { id = 4, name = "مستمر" },
                new EmployeeStatusList () { id = 5, name = "موقف" },
                new EmployeeStatusList () { id = 6, name = "تم إنهاء الخدمة" },
                new EmployeeStatusList () { id = 7, name = "حارس أمن" }
            };
            SelectList listItems = new SelectList(employeeStatus, "id", "name");
            ViewData["Employee"] = listItems;
        }
        //TempData["Edit"] = "Edit";
        //var employeeViewModel = new EmployeeVM
        //{
        //    PersonalData = await _personalDatarepository.GetByIdAsync(id),
        //    Employee = await _employeeRepository.GetByIdAsync(id),
        //    Family = await _familyrepository.GetByIdAsync(id),
        //    PracticalExperiences = await _practicalExperiencesrepository.GetByIdAsync(id)
        //};

        public async Task<IActionResult> EmployeeData(int id)
        {
            if (id != 0)
            {
                try
                {
                    var employee = await _context.employee.FirstOrDefaultAsync(X => X.Id == id);
                    if (employee != null)
                    {
                        return Ok(employee);
                    }
                    else
                    {
                        TempData["Message"] = "لم يتم العثور على الموظف المطلوب!";
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return Ok();
                }
            }
            else
            {
                TempData["Message"] = "أختر الموظف";
                return Ok();
            }
        }
    }

}

