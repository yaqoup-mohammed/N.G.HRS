using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection.PortableExecutable;
using PersonalData = N.G.HRS.Areas.Employees.Models.PersonalData;

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

            var employees = await _context.employee.Include(e => e.Departments)
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
                TempData["SystemError"] = ex.Message;
                return View(viewModel);
            }
            TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

            return View(viewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddEmployee(EmployeeVM viewModel)
        //{
        //    try
        //    {
        //        await PopulateDropdownListsAsync();

        //        if (viewModel.Employee != null)
        //        {
        //            var exist = _context.employee.Any(e => e.EmployeeNumber == viewModel.Employee.EmployeeNumber);
        //            if (!exist)
        //            {
        //                var lastNumber = await _employeeRepository.GetLastEmployeeNumber();
        //                if (string.IsNullOrEmpty(lastNumber))
        //                {
        //                    viewModel.Employee.EmployeeNumber = "1";
        //                }
        //                else
        //                {
        //                    var lastEmployeeNumber = int.Parse(lastNumber);
        //                    lastEmployeeNumber++;
        //                    viewModel.Employee.EmployeeNumber = lastEmployeeNumber.ToString();
        //                }

        //                await _employeeRepository.AddAsync(viewModel.Employee);
        //                TempData["Success"] = "تم الحفظ بنجاح";
        //                return RedirectToAction(nameof(AddEmployee));
        //            }
        //            else
        //            {
        //                TempData["Error"] = "الرقم الوظيفي موجود بالفعل";
        //                return View(viewModel);
        //            }

        //        }
        //        else
        //        {
        //            TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it accordingly
        //        TempData["SystemError"] = ex.Message;
        //        return View(viewModel);
        //    }
        //    TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

        //    return View(viewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddEmployee(EmployeeVM viewModel)
        //{
        //    try
        //    {
        //        await PopulateDropdownListsAsync();

        //        if (viewModel.Employee != null)
        //        {
        //            var exist = _context.employee.Any(e => e.EmployeeNumber == viewModel.Employee.EmployeeNumber);
        //            if (!exist)
        //            {
        //                // توليد الرقم الوظيفي المميز
        //                viewModel.Employee.EmployeeNumber = await GenerateEmployeeNumber();

        //                await _employeeRepository.AddAsync(viewModel.Employee);
        //                TempData["Success"] = "تم الحفظ بنجاح";
        //                return RedirectToAction(nameof(AddEmployee));
        //            }
        //            else
        //            {
        //                TempData["Error"] = "الرقم الوظيفي موجود بالفعل";
        //                return View(viewModel);
        //            }

        //        }
        //        else
        //        {
        //            TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // سجّل الاستثناء أو اتخذ إجراءً بناء عليه
        //        TempData["SystemError"] = ex.Message;
        //        return View(viewModel);
        //    }
        //    TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

        //    return View(viewModel);
        //}

        //public async Task<string> GenerateEmployeeNumber()
        //{
        //    // استعلام قاعدة البيانات للحصول على آخر قيمة تم استخدامها لتوليد الرقم الوظيفي
        //    var lastUsedNumber = await _employeeRepository.GetLastUsedEmployeeNumber();

        //    // إذا كانت هذه هي المرة الأولى توليد رقم وظيفي، فأعيد القيمة 1
        //    if (string.IsNullOrEmpty(lastUsedNumber))
        //    {
        //        return "1";
        //    }
        //    else
        //    {
        //        // إلا، زيادة القيمة المستخدمة بواحد وإعادتها
        //        var lastNumber = int.Parse(lastUsedNumber);
        //        lastNumber++;
        //        return lastNumber.ToString();
        //    }

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddEmployee(EmployeeVM viewModel)
        //{
        //    try
        //    {
        //        await PopulateDropdownListsAsync();

        //        if (viewModel.Employee != null)
        //        {
        //            var exist = _context.employee.Any(e => e.EmployeeNumber == viewModel.Employee.EmployeeNumber);
        //            if (exist)
        //            {
        //                var lastNumber = _context.employee
        //                                    .Where(e => e.EmployeeNumber.StartsWith(viewModel.Employee.EmployeeNumber))
        //                                    .Select(e => int.Parse(e.EmployeeNumber.Substring(viewModel.Employee.EmployeeNumber.Length)))
        //                                    .DefaultIfEmpty(0)
        //                                    .Max();

        //                viewModel.Employee.EmployeeNumber = viewModel.Employee.EmployeeNumber + (lastNumber + 1).ToString();
        //            }

        //            await _employeeRepository.AddAsync(viewModel.Employee);
        //            TempData["Success"] = "تم الحفظ بنجاح";
        //            return RedirectToAction(nameof(AddEmployee));
        //        }
        //        else
        //        {
        //            TempData["Error"] = "لم تتم الإضافة، هناك خطأ";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it accordingly
        //        TempData["SystemError"] = ex.Message;
        //    }
        //    TempData["Error"] = "البيانات غير صحيحة!! , لم تتم العملية!!";

        //    return View(viewModel);
        //}

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
        public async Task<IActionResult> SavePracticalExperiences(int practicalExperiencesEmployeeId, string practicalExperiencesExperiencesName, string practicalExperiencesPlacToGainExperience, DateOnly practicalExperiencesFromDate, DateOnly practicalExperiencesToDate, string practicalExperiencesDuration)
        {

            try
            {
                //// Parse the date strings to DateTime
                //DateTime fromDateValue = DateTime.ParseExact(practicalExperiencesFromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                //DateTime toDateValue = DateTime.ParseExact(practicalExperiencesToDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // إنشاء كائن لتخزين البيانات المستلمة
                var newPracticalExperience = new PracticalExperiences
                {
                   EmployeeId = practicalExperiencesEmployeeId,
                   ExperiencesName = practicalExperiencesExperiencesName,
                   PlacToGainExperience = practicalExperiencesPlacToGainExperience,
                   FromDate = practicalExperiencesFromDate,
                   ToDate = practicalExperiencesToDate,
                   Duration = practicalExperiencesDuration
                };

                // إضافة البيانات الجديدة إلى قاعدة البيانات
                _context.practicalExperiences.Add(newPracticalExperience);
                await _context.SaveChangesAsync();

                // إعادة توجيه المستخدم إلى الصفحة الرئيسية لإظهار التغييرات
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // في حالة حدوث أي خطأ، سنقوم بإرجاع استجابة سلبية مع رسالة الخطأ
                return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
            }
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
        public async Task<IActionResult> SaveFamily(int familyEmployeeId, string familyName, int familyRelativesTypeId, string familyNotes)
        {
            try
            {


                // إنشاء كائن لتخزين البيانات المستلمة
                var newFamily = new Family
                {
                    EmployeeId = familyEmployeeId,
                    Name = familyName,
                    RelativesTypeId = familyRelativesTypeId,
                    Notes = familyNotes
                    
                };

                // إضافة الدولة الجديدة إلى قاعدة البيانات باستخدام Entity Framework Core
                _context.Family.Add(newFamily);
                await _context.SaveChangesAsync();

                // إرجاع رسالة نجاح
                return Ok("تم حفظ البيانات بنجاح!");
            }
            catch (Exception ex)
            {
                // يمكنك تحسين هذا الجزء لتسجيل الخطأ بشكل أفضل
                return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
            }
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

        //public async Task<IActionResult> CheckData(string hp, string phone, string email, int card)
        //{
        //    if (!string.IsNullOrEmpty(hp) || !string.IsNullOrEmpty(phone) || !string.IsNullOrEmpty(email) || card != 0)
        //    {
        //        var existingData = await _context.personalDatas.FirstOrDefaultAsync(x =>
        //            x.HomePhone == hp ||
        //            x.PhoneNumber == phone ||
        //            x.Email == email ||
        //            x.CardNumber == card);

        //        if (existingData != null)
        //        {
        //            if (!string.IsNullOrEmpty(existingData.HomePhone) && existingData.HomePhone == hp)
        //            {
        //                return Json(1);
        //            }
        //            if (!string.IsNullOrEmpty(existingData.PhoneNumber) && existingData.PhoneNumber == phone)
        //            {
        //                return Json(2);
        //            }
        //            if (!string.IsNullOrEmpty(existingData.Email) && existingData.Email == email)
        //            {
        //                return Json(3);
        //            }
        //            if (existingData.CardNumber != 0 && existingData.CardNumber == card)
        //            {
        //                return Json(4);
        //            }
        //        }
        //        return Json(0);
        //    }
        //    return NotFound();
        //}

        public IActionResult CheckData(string hp, string phone, string email, int card)
        {
            if (hp != null || phone != null || email != null || card != 0)
            {
                var Hp = _context.personalDatas.FirstOrDefault(x => x.HomePhone == hp);
                var Phone = _context.personalDatas.FirstOrDefault(x => x.PhoneNumber == phone);
                var Email = _context.personalDatas.FirstOrDefault(x => x.Email == email);
                var Card = _context.personalDatas.FirstOrDefault(x => x.CardNumber == card);
                if (Hp != null)
                {
                    return Json(1);
                }
                if (Phone != null)
                {
                    return Json(2);
                }
                if (Email != null)
                {
                    return Json(3);
                }
                if (Card != null)
                {
                    return Json(4);
                }
                return Json(0);
            }
            return NotFound();
        }

        //[HttpPost]
        //public async Task<IActionResult> SavePersonalData(int personalDataEmployeeId, DateOnly PersonalDataDateOfBirth, int personalDataAge, int personalDataSexId, int personalDataNationalityId, int personalDataReligionId, int personalDataMaritalStatusId, int personalDataGuaranteesId, string personalDataHomePhone, string personalDataPhoneNumber, string personalDataEmail, string personalDataAddress, string personalDataCardType, int personalDataCardNumber, string personalDataToRelease, DateOnly personalDataReleaseDate, DateOnly personalDataCardExpiryDate, string personalDataNotes)
        //{
        //    try
        //    {

        //        // إنشاء كائن لتخزين البيانات المستلمة
        //        var newPersonalDatas = new PersonalData
        //        {
        //            EmployeeId = personalDataEmployeeId,
        //            DateOfBirth = PersonalDataDateOfBirth,
        //            Age = personalDataAge,
        //            SexId = personalDataSexId,
        //            NationalityId = personalDataNationalityId,
        //            ReligionId = personalDataReligionId,
        //            MaritalStatusId = personalDataMaritalStatusId,
        //            GuaranteesId = personalDataGuaranteesId,
        //            HomePhone = personalDataHomePhone,
        //            PhoneNumber = personalDataPhoneNumber,
        //            Email = personalDataEmail,
        //            Address = personalDataAddress,
        //            CardType = personalDataCardType,
        //            CardNumber = personalDataCardNumber,
        //            ToRelease = personalDataToRelease,
        //            ReleaseDate = personalDataReleaseDate,
        //            CardExpiryDate = personalDataCardExpiryDate,
        //            Notes = personalDataNotes


        //        };

        //        // إضافة الدولة الجديدة إلى قاعدة البيانات باستخدام Entity Framework Core
        //        _context.personalDatas.Add(newPersonalDatas);
        //        await _context.SaveChangesAsync();

        //        // إرجاع رسالة نجاح
        //        return Ok("تم حفظ البيانات بنجاح!");
        //    }
        //    catch (Exception ex)
        //    {
        //        // يمكنك تحسين هذا الجزء لتسجيل الخطأ بشكل أفضل
        //        return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
        //    }
        //}





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
        public async Task<IActionResult> SaveGuarantees(string guaranteesName1, string guaranteesPhoneNumber, string guaranteesNameOfTheBusiness, int guaranteesCommercialRegistrationNo, string guaranteesShopAddress1, string guaranteesHomeAdress1, int guaranteesMaritalStatusId, int guaranteesNumberOfDependents, string guaranteesNotes1)
        {
            try
            {

                           // إنشاء كائن لتخزين البيانات المستلمة
                   var newGuarantees = new Guarantees
                {
                    Name = guaranteesName1,
                    PhoneNumber = guaranteesPhoneNumber,
                    NameOfTheBusiness = guaranteesNameOfTheBusiness,
                    CommercialRegistrationNo = guaranteesCommercialRegistrationNo,
                    ShopAddress = guaranteesShopAddress1,
                    HomeAdress = guaranteesHomeAdress1,
                    MaritalStatusId = guaranteesMaritalStatusId,
                    NumberOfDependents = guaranteesNumberOfDependents,
                    Notes = guaranteesNotes1
                   
                };

                // إضافة الدولة الجديدة إلى قاعدة البيانات باستخدام Entity Framework Core
                _context.guarantees.Add(newGuarantees);
                await _context.SaveChangesAsync();

                // إرجاع رسالة نجاح
                return Ok("تم حفظ البيانات بنجاح!");
            }
            catch (Exception ex)
            {
                // يمكنك تحسين هذا الجزء لتسجيل الخطأ بشكل أفضل
                return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
            }
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
                TempData["Error"] = ex.Message;

                return View(viewModel);
            }

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> SaveFinancialStatements(int financialStatementsEmployeeId, decimal financialStatementsBasicSalary, int financialStatementsCurrencyId, int financialStatementsInsuranceAccountNumber, int financialStatementsBankAccountNumber, string financialStatementsNatureOfEmployment, DateTime financialStatementsSalaryStartDate, DateTime financialStatementsSalaryEndDate, string financialStatementsNotes)
        {
            try
            {






                // إنشاء كائن لتخزين البيانات المستلمة
                var newFinancialStatements = new FinancialStatements
                {
                    EmployeeId = financialStatementsEmployeeId,
                    BasicSalary = financialStatementsBasicSalary,
                    CurrencyId = financialStatementsCurrencyId,
                    InsuranceAccountNumber = financialStatementsInsuranceAccountNumber,
                    BankAccountNumber = financialStatementsBankAccountNumber,
                    NatureOfEmployment = financialStatementsNatureOfEmployment,
                    SalaryStartDate = financialStatementsSalaryStartDate,
                    SalaryEndDate = financialStatementsSalaryEndDate,
                    Notes = financialStatementsNotes,



                };

                // إضافة البيانات الجديدة إلى قاعدة البيانات
                _context.financialStatements.Add(newFinancialStatements);
                await _context.SaveChangesAsync();

                // إرجاع رقم الصفحة
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // في حال حدوث أي خطأ آخر
                return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
            }
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
        public async Task<IActionResult> SaveTrainingCourses(int trainingCoursesEmployeeId, string trainingCoursesNameCourses, string trainingCoursesWhereToGetIt, string trainingCoursesFromDate, string trainingCoursesToDate)
        {
           
            try
            {
                // Parse the date strings to DateTime
                DateTime fromDateValue = DateTime.ParseExact(trainingCoursesFromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime toDateValue = DateTime.ParseExact(trainingCoursesToDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // إنشاء كائن لتخزين البيانات المستلمة
                var newTrainingCourses = new TrainingCourses
                {
                    EmployeeId = trainingCoursesEmployeeId,
                    NameCourses = trainingCoursesNameCourses,
                    WhereToGetIt = trainingCoursesWhereToGetIt,
                    ToDate = DateOnly.FromDateTime(toDateValue),
                    FromDate = DateOnly.FromDateTime(fromDateValue)
                };

                // إضافة البيانات الجديدة إلى قاعدة البيانات
                _context.trainingCourses.Add(newTrainingCourses);
                await _context.SaveChangesAsync();

                // إرجاع رقم الصفحة
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // في حال حدوث أي خطأ آخر
                return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
            }
        }


        //[HttpPost]

        //public async Task<IActionResult> SaveTrainingCourses(int employeeId, string nameCourses, string whereToGetIt, string fromDate, string toDate)
        //{
        //    try
        //    {
        //        // Parse the date strings to DateTime
        //        DateTime fromDateValue = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //        DateTime toDateValue = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        //        // إنشاء كائن لتخزين البيانات المستلمة
        //        var newTrainingCourses = new TrainingCourses
        //        {
        //            EmployeeId = employeeId,
        //            NameCourses = nameCourses,
        //            WhereToGetIt = whereToGetIt,
        //            ToDate = DateOnly.FromDateTime(toDateValue),
        //            FromDate = DateOnly.FromDateTime(fromDateValue)


        //        };

        //        // إضافة البيانات الجديدة إلى قاعدة البيانات
        //        _context.trainingCourses.Add(newTrainingCourses);
        //        await _context.SaveChangesAsync();

        //        // إرجاع رسالة نجاح
        //        return Ok("تم حفظ البيانات بنجاح!");
        //    }
        //    catch (Exception ex)
        //    {
        //        // في حال حدوث أي خطأ آخر
        //        return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
        //    }
        //}

        //[HttpPost]
        //public IActionResult SaveTrainingCourses(int employeeId, string nameCourses, string whereToGetIt, DateTime fromDate, DateTime toDate)
        //{
        //    // هنا يجب عمل اللوجيك لحفظ البيانات في قاعدة البيانات أو المكان المناسب
        //    // يمكنك استخدام Entity Framework أو أي إطار عمل آخر للوصول إلى قاعدة البيانات

        //    try
        //    {
        //        // في هذا المثال، سنقوم بعرض البيانات المستلمة في وحدة التحكم
        //        Console.WriteLine($"بيانات الموظف: {employeeId}");
        //        Console.WriteLine($"اسم الدورة: {nameCourses}");
        //        Console.WriteLine($"مكان الحصول عليها: {whereToGetIt}");
        //        Console.WriteLine($"من تاريخ: {fromDate}");
        //        Console.WriteLine($"الى تاريخ: {toDate}");

        //        // هنا يمكنك كتابة الكود الخاص بحفظ البيانات في قاعدة البيانات

        //        // بعد حفظ البيانات، يمكنك إرسال رد بنجاح العملية
        //        return Ok("تم حفظ البيانات بنجاح");
        //    }
        //    catch (Exception ex)
        //    {
        //        // في حالة حدوث خطأ، يمكنك إرجاع استجابة توضيحية مع الخطأ
        //        return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
        //    }
        //}


 
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

        [HttpPost]
        public async Task<IActionResult> SaveArchives(IFormFile archivesFileUpload, int archivesEmployeeId, DateOnly archivesDate, string archivesDescriotion, string archivesNotes)
        {


            try
            {
              
                var file = archivesFileUpload;

               
                    var filePath = await _fileUploadService.UploadFileAsync(file," Upload/PDF");
                    // إنشاء كائن لتخزين البيانات المستلمة
                    var newEmployeeArchives = new EmployeeArchives
                    {
                        File = filePath,

                        EmployeeId = archivesEmployeeId,
                        Date = archivesDate,
                        Descriotion = archivesDescriotion,
                        Notes = archivesNotes

                    };                    // Rest of your code...



                // إضافة البيانات الجديدة إلى قاعدة البيانات
                await _employeeArchivesrepository.AddAsync(newEmployeeArchives);
                await _context.SaveChangesAsync();

                // إعادة توجيه المستخدم إلى الصفحة الرئيسية لإظهار التغييرات
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["SystemError"] = ex.Message;
                return View();
                
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> SaveArchives(string archivesFileUpload, int archivesEmployeeId, DateOnly archivesDate, string archivesDescriotion, string archivesNotes)
        //{
        //    try
        //    {
        //        // إنشاء كائن لتخزين البيانات المستلمة
        //        var newEmployeeArchives = new EmployeeArchives
        //        {
        //            EmployeeId = archivesEmployeeId,
        //            Date = archivesDate,
        //            Descriotion = archivesDescriotion,
        //            Notes = archivesNotes
        //        };

        //        // إذا كانت البيانات تشمل صورة، قم بحفظها كملف الصورة الأصلي على الخادم
        //        if (!string.IsNullOrEmpty(archivesFileUpload) && IsImage(archivesFileUpload))
        //        {
        //            // حفظ ملف الصورة على الخادم
        //            string imagePath = SaveImage(archivesFileUpload);

        //            // تعيين مسار الصورة في كائن الـ newEmployeeArchives
        //            newEmployeeArchives.File = imagePath;
        //        }
        //        // إذا كانت البيانات تشمل ملف PDF، قم بحفظه كملف PDF على الخادم
        //        else if (!string.IsNullOrEmpty(archivesFileUpload) && IsPdf(archivesFileUpload))
        //        {
        //            // حفظ ملف PDF على الخادم
        //            string pdfPath = SavePdf(archivesFileUpload);

        //            // تعيين مسار ملف PDF في كائن الـ newEmployeeArchives
        //            newEmployeeArchives.File = pdfPath;
        //        }

        //        // إضافة البيانات الجديدة إلى قاعدة البيانات
        //        _context.EmployeeArchives.Add(newEmployeeArchives);
        //        await _context.SaveChangesAsync();

        //        // إعادة توجيه المستخدم إلى الصفحة الرئيسية لإظهار التغييرات
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        // في حالة حدوث أي خطأ، سنقوم بإرجاع استجابة سلبية مع رسالة الخطأ
        //        return BadRequest("حدث خطأ أثناء حفظ البيانات: " + ex.Message);
        //    }
        //}

        //// دالة لحفظ ملف الصورة على الخادم
        //private string SaveImage(string imageData)
        //{
        //    // قم بتحويل البيانات الواردة كـ Base64 إلى مصفوفة بايت
        //    byte[] imageBytes = Convert.FromBase64String(imageData);

        //    // تحديد مسار الحفظ على الخادم
        //    string imagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".jpg");

        //    // حفظ ملف الصورة على الخادم
        //    System.IO.File.WriteAllBytes(imagePath, imageBytes);

        //    return imagePath;
        //}

        //// دالة لحفظ ملف PDF على الخادم
        //private string SavePdf(string pdfData)
        //{
        //    // قم بتحويل البيانات الواردة كـ Base64 إلى مصفوفة بايت
        //    byte[] pdfBytes = Convert.FromBase64String(pdfData);

        //    // تحديد مسار الحفظ على الخادم
        //    string pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

        //    // حفظ ملف PDF على الخادم
        //    System.IO.File.WriteAllBytes(pdfPath, pdfBytes);

        //    return pdfPath;
        //}

        //// دالة لفحص ما إذا كانت البيانات هي صورة
        //private bool IsImage(string data)
        //{
        //    // قم بتحليل البيانات كـ Base64 للتحقق مما إذا كانت البيانات تمثل صورة أم لا
        //    try
        //    {
        //        byte[] imageBytes = Convert.FromBase64String(data);
        //        using (var ms = new MemoryStream(imageBytes))
        //        {
        //            var image = System.Drawing.Image.FromStream(ms);
        //            return ImageFormat.Jpeg.Equals(image.RawFormat) || ImageFormat.Png.Equals(image.RawFormat) || ImageFormat.Gif.Equals(image.RawFormat);
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //// دالة لفحص ما إذا كانت البيانات هي ملف PDF
        //private bool IsPdf(string data)
        //{
        //    // قم بتحليل البيانات كـ Base64 للتحقق مما إذا كانت البيانات تمثل ملف PDF أم لا
        //    try
        //    {
        //        byte[] pdfBytes = Convert.FromBase64String(data);
        //        using (MemoryStream ms = new MemoryStream(pdfBytes))
        //        {
        //            using (PdfReader reader = new PdfReader(ms))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}



        public bool EmployeeNumber(int id)
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
            var filteredGuarantees = GuaranteesOne.Where(g => _context.personalDatas.Where(pd => pd.GuaranteesId == g.Id).Count()<=10) ;
            //var filteredGuarantees = GuaranteesOne.Where(g => !_context.personalDatas.Any(pd => pd.GuaranteesId == g.Id));
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
     
    }

}

