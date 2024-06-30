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
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection.PortableExecutable;
using PersonalData = N.G.HRS.Areas.Employees.Models.PersonalData;
using Microsoft.Graph;
using DocumentFormat.OpenXml.Wordprocessing;

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
        // استيراد ملف اكسل للموظفين

        //[HttpPost]
        //public async Task<IActionResult> Import(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        ViewBag.Message = "يرجى اختيار ملف Excel.";
        //        return View("Import");
        //    }

        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);

        //        using (var package = new ExcelPackage(stream))
        //        {
        //            var worksheet = package.Workbook.Worksheets[0];
        //            var rowCount = worksheet.Dimension.Rows;

        //            var employees = new List<Employee>();

        //            for (int row = 2; row <= rowCount; row++)
        //            {
        //                var employeeNumber = int.Parse(worksheet.Cells[row, 1].Text);

        //                // تحقق مما إذا كان الرقم الوظيفي موجودًا بالفعل
        //                var existingEmployee = await _context.employee
        //                    .FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);

        //                if (existingEmployee != null)
        //                {
        //                    // إذا كان الرقم الوظيفي موجودًا، يمكنك تخطيه أو تحديث بياناته
        //                    continue; // تخطي السجل المكرر
        //                }

        //                DateTime dateOfEmployment;
        //                DateTime? placementDateNullable = null, rehireDateNullable = null, dateOfStoppingWorkNullable = null, dateInsuranceNullable = null;

        //                if (DateTime.TryParse(worksheet.Cells[row, 3].Text, out dateOfEmployment))
        //                {
        //                    if (DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime placementDate))
        //                        placementDateNullable = placementDate;

        //                    if (DateTime.TryParse(worksheet.Cells[row, 6].Text, out DateTime rehireDate))
        //                        rehireDateNullable = rehireDate;

        //                    if (DateTime.TryParse(worksheet.Cells[row, 7].Text, out DateTime dateOfStoppingWork))
        //                        dateOfStoppingWorkNullable = dateOfStoppingWork;

        //                    if (DateTime.TryParse(worksheet.Cells[row, 10].Text, out DateTime dateInsurance))
        //                        dateInsuranceNullable = dateInsurance;

        //                    var employee = new Employee
        //                    {
        //                        EmployeeNumber = employeeNumber,
        //                        EmployeeName = worksheet.Cells[row, 2].Text,
        //                        DateOfEmployment = dateOfEmployment,
        //                        PlacementDate = placementDateNullable,
        //                        EmploymentStatus = worksheet.Cells[row, 5].Text,
        //                        RehireDate = rehireDateNullable.HasValue ? DateOnly.FromDateTime(rehireDateNullable.Value) : null,
        //                        DateOfStoppingWork = dateOfStoppingWorkNullable != null ? DateOnly.FromDateTime(dateOfStoppingWorkNullable.Value) : null,
        //                        UsedFingerprint = worksheet.Cells[row, 8].Text == "1",
        //                        SubjectToInsurance = worksheet.Cells[row, 9].Text == "1",
        //                        DateInsurance = dateInsuranceNullable != null ? DateOnly.FromDateTime(dateInsuranceNullable.Value) : null,
        //                        FingerPrintImage = byte.Parse(worksheet.Cells[row, 11].Text),
        //                        ImageFile = worksheet.Cells[row, 12].Text,
        //                        Notes = worksheet.Cells[row, 13].Text,
        //                        DepartmentsId = int.Parse(worksheet.Cells[row, 14].Text),
        //                        SectionsId = int.Parse(worksheet.Cells[row, 15].Text),
        //                        JobDescriptionId = int.Parse(worksheet.Cells[row, 16].Text),
        //                        FingerprintDevicesId = string.IsNullOrEmpty(worksheet.Cells[row, 17].Text) ? (int?)null : int.Parse(worksheet.Cells[row, 17].Text),
        //                        ManagerId = string.IsNullOrEmpty(worksheet.Cells[row, 18].Text) ? (int?)null : int.Parse(worksheet.Cells[row, 18].Text),
        //                        CurrentJop = worksheet.Cells[row, 19].Text
        //                    };

        //                    employees.Add(employee);
        //                    _context.employee.Add(employee);

        //                }
        //                else
        //                {
        //                    // Handle invalid date formats here if necessary
        //                    ViewBag.Message = "صيغة التاريخ غير صحيحة في الصف " + row;
        //                    return View("Import");
        //                }
        //            }

        //            _context.employee.AddRange(employees);
        //            await _context.SaveChangesAsync();
        //        }
        //    }

        //    ViewBag.Message = "تم استيراد البيانات بنجاح!";
        //    //return View("");
        //    return RedirectToAction("Index");

        //}

        //[HttpPost]
        //public async Task<IActionResult> Import(IFormFile file)
        //{
        //    if (file == null || file.Length <= 0)
        //    {
        //        ModelState.AddModelError("File", "يرجى تحديد ملف للتحميل.");
        //        return View("Import");
        //    }

        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);

        //        using (var package = new ExcelPackage(stream))
        //        {
        //            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
        //            if (worksheet == null)
        //            {
        //                TempData["Error"] = "الملف لا يحتوي على أوراق عمل.";
        //                return View("Import");
        //            }

        //            var rowCount = worksheet.Dimension.Rows;
        //            var employees = new List<Employee>();

        //            for (int row = 2; row <= rowCount; row++)
        //            {
        //                if (int.TryParse(worksheet.Cells[row, 1].Text, out int employeeNumber) &&
        //                    !string.IsNullOrWhiteSpace(worksheet.Cells[row, 2].Text) &&
        //                    DateTime.TryParse(worksheet.Cells[row, 3].Text, out DateTime dateOfEmployment) &&
        //                    DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime placementDate) &&
        //                    !string.IsNullOrWhiteSpace(worksheet.Cells[row, 5].Text) &&
        //                    DateOnly.TryParse(worksheet.Cells[row, 6].Text, out DateOnly rehireDate) &&
        //                    DateOnly.TryParse(worksheet.Cells[row, 7].Text, out DateOnly dateOfStoppingWork) &&
        //                    bool.TryParse(worksheet.Cells[row, 8].Text, out bool usedFingerprint) &&
        //                    bool.TryParse(worksheet.Cells[row, 9].Text, out bool subjectToInsurance) &&
        //                    DateOnly.TryParse(worksheet.Cells[row, 10].Text, out DateOnly dateInsurance) &&
        //                    int.TryParse(worksheet.Cells[row, 12].Text, out int departmentsId) &&
        //                    int.TryParse(worksheet.Cells[row, 13].Text, out int sectionsId) &&
        //                    int.TryParse(worksheet.Cells[row, 14].Text, out int jobDescriptionId))
        //                {
        //                    var existingEmployee = _context.employee.FirstOrDefault(e => e.EmployeeNumber == employeeNumber);

        //                    if (existingEmployee == null)
        //                    {
        //                        var employee = new Employee
        //                        {
        //                            EmployeeNumber = employeeNumber,
        //                            EmployeeName = worksheet.Cells[row, 2].Text,
        //                            DateOfEmployment = dateOfEmployment,
        //                            PlacementDate = placementDate,
        //                            EmploymentStatus = worksheet.Cells[row, 5].Text,
        //                            RehireDate = rehireDate,
        //                            DateOfStoppingWork = dateOfStoppingWork,
        //                            UsedFingerprint = usedFingerprint,
        //                            SubjectToInsurance = subjectToInsurance,
        //                            DateInsurance = dateInsurance,
        //                            Notes = worksheet.Cells[row, 11].Text,
        //                            DepartmentsId = departmentsId,
        //                            SectionsId = sectionsId,
        //                            JobDescriptionId = jobDescriptionId
        //                        };

        //                        employees.Add(employee);
        //                    }
        //                    else
        //                    {
        //                        TempData["Error"] = $"الموظف برقم وظيفي {employeeNumber} موجود بالفعل. تم تخطيه.";
        //                    }
        //                }
        //                else
        //                {
        //                    TempData["Error"] = $"يوجد خطأ في الصف {row}. تم تخطيه.";
        //                }
        //            }

        //            if (employees.Any())
        //            {
        //                _context.employee.AddRange(employees);
        //                await _context.SaveChangesAsync();
        //                TempData["Success"] = "تم استيراد البيانات بنجاح.";
        //            }
        //            else
        //            {
        //                TempData["Error"] = "لم يتم استيراد أي بيانات جديدة.";
        //            }
        //        }
        //    }

        //    return RedirectToAction(nameof(Index));
        //}نننننننننننننننننننننننننننننننننننننننننن

        //[HttpPost]
        //public async Task<IActionResult> Import(IFormFile file)
        //{
        //    if (file == null || file.Length <= 0)
        //    {
        //        ModelState.AddModelError("File", "يرجى تحديد ملف للتحميل.");
        //        return View("Import");
        //    }

        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);

        //        using (var package = new ExcelPackage(stream))
        //        {
        //            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
        //            if (worksheet == null)
        //            {
        //                TempData["Error"] = "الملف لا يحتوي على أوراق عمل.";
        //                return View("Import");
        //            }

        //            var rowCount = worksheet.Dimension.Rows;
        //            var employees = new List<Employee>();

        //            for (int row = 2; row <= rowCount; row++)
        //            {
        //                if (int.TryParse(worksheet.Cells[row, 1].Text, out int employeeNumber) &&
        //                    !string.IsNullOrWhiteSpace(worksheet.Cells[row, 2].Text) &&
        //                    DateTime.TryParse(worksheet.Cells[row, 3].Text, out DateTime dateOfEmployment) &&
        //                    DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime placementDate) &&
        //                    !string.IsNullOrWhiteSpace(worksheet.Cells[row, 5].Text) &&
        //                    DateOnly.TryParse(worksheet.Cells[row, 6].Text, out DateOnly rehireDate) &&
        //                    DateOnly.TryParse(worksheet.Cells[row, 7].Text, out DateOnly dateOfStoppingWork) &&
        //                    bool.TryParse(worksheet.Cells[row, 8].Text, out bool usedFingerprint) &&
        //                    bool.TryParse(worksheet.Cells[row, 9].Text, out bool subjectToInsurance) &&
        //                    DateOnly.TryParse(worksheet.Cells[row, 10].Text, out DateOnly dateInsurance) &&
        //                    int.TryParse(worksheet.Cells[row, 12].Text, out int departmentsId) &&
        //                    int.TryParse(worksheet.Cells[row, 13].Text, out int sectionsId) &&
        //                    int.TryParse(worksheet.Cells[row, 14].Text, out int jobDescriptionId))
        //                {
        //                    var existingEmployee = _context.employee.FirstOrDefault(e => e.EmployeeNumber == employeeNumber);

        //                    if (existingEmployee == null)
        //                    {
        //                        var employee = new Employee
        //                        {
        //                            EmployeeNumber = employeeNumber,
        //                            EmployeeName = worksheet.Cells[row, 2].Text,
        //                            DateOfEmployment = dateOfEmployment,
        //                            PlacementDate = placementDate,
        //                            EmploymentStatus = worksheet.Cells[row, 5].Text,
        //                            RehireDate = rehireDate,
        //                            DateOfStoppingWork = dateOfStoppingWork,
        //                            UsedFingerprint = usedFingerprint,
        //                            SubjectToInsurance = subjectToInsurance,
        //                            DateInsurance = dateInsurance,
        //                            Notes = worksheet.Cells[row, 11].Text,
        //                            DepartmentsId = departmentsId,
        //                            SectionsId = sectionsId,
        //                            JobDescriptionId = jobDescriptionId
        //                        };

        //                        employees.Add(employee);
        //                    }
        //                    else
        //                    {
        //                        TempData["Error"] = $"الموظف برقم وظيفي {employeeNumber} موجود بالفعل. تم تخطيه.";
        //                    }
        //                }
        //                else
        //                {
        //                    TempData["Error"] = $"يوجد خطأ في الصف {row}. تم تخطيه.";
        //                }
        //            }

        //            if (employees.Any())
        //            {
        //                _context.employee.AddRange(employees);
        //                await _context.SaveChangesAsync();
        //                TempData["Success"] = "تم استيراد البيانات بنجاح.";
        //            }
        //            else
        //            {
        //                TempData["Error"] = "لم يتم استيراد أي بيانات جديدة.";
        //            }
        //        }
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        [HttpGet]
        public async Task<IActionResult> ExportToExcelEmployee()
        {
            var employees = await _context.employee
                .Include(e => e.Departments)
                .Include(e => e.Sections)
                .Include(e => e.JobDescription)
                .Include(e => e.Manager)
                .ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Adding Headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Employee Number";
                worksheet.Cells[1, 3].Value = "Employee Name";
                worksheet.Cells[1, 4].Value = "Date Of Employment";
                worksheet.Cells[1, 5].Value = "Placement Date";
                worksheet.Cells[1, 6].Value = "Employment Status";
                worksheet.Cells[1, 7].Value = "Rehire Date";
                worksheet.Cells[1, 8].Value = "Date Of Stopping Work";
                worksheet.Cells[1, 9].Value = "Used Fingerprint";
                worksheet.Cells[1, 10].Value = "Subject To Insurance";
                worksheet.Cells[1, 11].Value = "Date Insurance";
                worksheet.Cells[1, 12].Value = "Finger Print Image";
                worksheet.Cells[1, 13].Value = "Image File";
                worksheet.Cells[1, 14].Value = "Notes";
                worksheet.Cells[1, 15].Value = "Department Name";
                worksheet.Cells[1, 16].Value = "Section Name";
                worksheet.Cells[1, 17].Value = "Job Description Name";
                worksheet.Cells[1, 18].Value = "Fingerprint Devices ID";
                worksheet.Cells[1, 19].Value = "Manager Name";

                // Adding Data
                for (int i = 0; i < employees.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = employees[i].Id;
                    worksheet.Cells[i + 2, 2].Value = employees[i].EmployeeNumber;
                    worksheet.Cells[i + 2, 3].Value = employees[i].EmployeeName;
                    worksheet.Cells[i + 2, 4].Value = employees[i].DateOfEmployment?.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 5].Value = employees[i].PlacementDate?.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 6].Value = employees[i].EmploymentStatus;
                    worksheet.Cells[i + 2, 7].Value = employees[i].RehireDate?.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 8].Value = employees[i].DateOfStoppingWork?.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 9].Value = employees[i].UsedFingerprint;
                    worksheet.Cells[i + 2, 10].Value = employees[i].SubjectToInsurance;
                    worksheet.Cells[i + 2, 11].Value = employees[i].DateInsurance?.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 12].Value = employees[i].FingerPrintImage;
                    worksheet.Cells[i + 2, 13].Value = employees[i].ImageFile;
                    worksheet.Cells[i + 2, 14].Value = employees[i].Notes;
                    worksheet.Cells[i + 2, 15].Value = employees[i].Departments?.SubAdministration;
                    worksheet.Cells[i + 2, 16].Value = employees[i].Sections?.SectionsName;
                    worksheet.Cells[i + 2, 17].Value = employees[i].JobDescription?.JopName;
                    worksheet.Cells[i + 2, 18].Value = employees[i].FingerprintDevicesId;
                    worksheet.Cells[i + 2, 19].Value = employees[i].Manager?.EmployeeName;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"Employees-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }


        //////تصدير ملف اكسل للموظفين
        


        [HttpGet]
        public IActionResult GetSections(int departmentId)
        {
            var sections = _context.Sections.Include(x => x.Departments).Where(s => s.DepartmentsId == departmentId).Select(x => new { id = x.Id, name = x.SectionsName }).ToList();
            if (sections == null)
            {
                return NotFound();
            }
            return Json(new { sections });
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
        public async Task<IActionResult> ImportPracticalExperiences(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "يرجى تحديد ملف Excel صالح.");
                return View();
            }

            var practicalExperiences = new List<PracticalExperiences>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        ModelState.AddModelError("", "الملف لا يحتوي على ورقة عمل.");
                        return View();
                    }

                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            var employeeName = worksheet.Cells[row, 2].Text.Trim(); // العمود 2 للعمود "Employee Name"
                            var experiencesName = worksheet.Cells[row, 3].Text.Trim(); // العمود 3 للعمود "Experience Name"
                            var placeToGainExperience = worksheet.Cells[row, 4].Text.Trim(); // العمود 4 للعمود "Place To Gain Experience"
                            var fromDateText = worksheet.Cells[row, 5].Text.Trim(); // العمود 5 للعمود "From Date"
                            var toDateText = worksheet.Cells[row, 6].Text.Trim(); // العمود 6 للعمود "To Date"
                            var duration = worksheet.Cells[row, 7].Text.Trim(); // العمود 7 للعمود "Duration"

                            if (string.IsNullOrEmpty(employeeName) || string.IsNullOrEmpty(experiencesName) || string.IsNullOrEmpty(placeToGainExperience) || string.IsNullOrEmpty(fromDateText) || string.IsNullOrEmpty(toDateText))
                            {
                                continue; // تخطي الصفوف ذات البيانات المفقودة
                            }

                            var employee = _context.employee.FirstOrDefault(e => e.EmployeeName == employeeName);
                            if (employee == null)
                            {
                                continue; // تخطي إذا لم يتم العثور على الموظف
                            }

                            if (!DateTime.TryParse(fromDateText, out DateTime fromDate) || !DateTime.TryParse(toDateText, out DateTime toDate))
                            {
                                continue; // تخطي الصفوف ذات التواريخ غير الصالحة
                            }

                            var practicalExperience = new PracticalExperiences
                            {
                                EmployeeId = employee.Id,
                                ExperiencesName = experiencesName,
                                PlacToGainExperience = placeToGainExperience,
                                FromDate = DateOnly.FromDateTime(fromDate),
                                ToDate = DateOnly.FromDateTime(toDate),
                                Duration = duration
                            };

                            practicalExperiences.Add(practicalExperience);
                        }
                        catch (Exception ex)
                        {
                            // سجل الخطأ للمراجعة
                            ModelState.AddModelError("", $"حدث خطأ في الصف {row}: {ex.Message}");
                        }
                    }
                }
            }

            if (practicalExperiences.Count > 0)
            {
                _context.practicalExperiences.AddRange(practicalExperiences);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index"); // تعديل الوجهة حسب متطلباتك
        }


        // تصدير  بيانات الخبرات

        //public async Task<IActionResult> ExportPracticalExperiencesToExcel()
        //{
        //    var practicalExperiences = await _context.practicalExperiences.Include(pe => pe.Employee).ToListAsync();

        //    using (var package = new ExcelPackage())
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("PracticalExperiences");

        //        // إعداد العناوين
        //        worksheet.Cells[1, 1].Value = "Id";
        //        worksheet.Cells[1, 2].Value = "Employee Name";
        //        worksheet.Cells[1, 3].Value = "Experience Name";
        //        worksheet.Cells[1, 4].Value = "Place To Gain Experience";
        //        worksheet.Cells[1, 5].Value = "From Date";
        //        worksheet.Cells[1, 6].Value = "To Date";
        //        worksheet.Cells[1, 7].Value = "Duration";

        //        worksheet.Row(1).Style.Font.Bold = true;
        //        worksheet.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
        //        worksheet.Row(1).Style.Fill.BackgroundColor.SetColor(Color.LightGray);

        //        // تعبئة البيانات
        //        int row = 2;
        //        foreach (var experience in practicalExperiences)
        //        {
        //            worksheet.Cells[row, 1].Value = experience.Id;
        //            worksheet.Cells[row, 2].Value = experience.Employee.EmployeeName;
        //            worksheet.Cells[row, 3].Value = experience.ExperiencesName;
        //            worksheet.Cells[row, 4].Value = experience.PlacToGainExperience;
        //            worksheet.Cells[row, 5].Value = experience.FromDate.ToShortDateString();
        //            worksheet.Cells[row, 6].Value = experience.ToDate.ToShortDateString();
        //            worksheet.Cells[row, 7].Value = experience.Duration;
        //            row++;
        //        }

        //        // إعداد التنسيق
        //        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        //        var fileName = "PracticalExperiences.xlsx";
        //        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        var fileContents = package.GetAsByteArray();

        //        return File(fileContents, mimeType, fileName);
        //    }
        //}

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


        // Action Method لتصدير البيانات  الاسرة إلى ملف Excel
        public IActionResult ExportFamilyToExcel()
        {
            // استرجاع البيانات التي تريد تصديرها إلى Excel مع تضمين الكيانات ذات الصلة
            var families = _context.Family
                .Include(f => f.Employees)
                .Include(f => f.RelativesType)
                .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Families");

                // إضافة العناوين
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "الموظف";
                worksheet.Cells[1, 3].Value = "الاسم";
                worksheet.Cells[1, 4].Value = "صلة القرابة";
                worksheet.Cells[1, 5].Value = "الملاحظة";

                // إضافة البيانات
                for (int i = 0; i < families.Count; i++)
                {
                    var family = families[i];
                    worksheet.Cells[i + 2, 1].Value = family.Id; // Id
                    worksheet.Cells[i + 2, 2].Value = family.Employees?.EmployeeName; // اسم الموظف
                    worksheet.Cells[i + 2, 3].Value = family.Name;
                    worksheet.Cells[i + 2, 4].Value = family.RelativesType?.RelativeName; // صلة القرابة
                    worksheet.Cells[i + 2, 5].Value = family.Notes;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Families.xlsx");
            }
        }

        // Action Method لاستيراد البيانات من ملف Excel
        [HttpPost]
        public async Task<IActionResult> ImportFamilyFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "يرجى اختيار ملف Excel.";
                return RedirectToAction("AddFamilyToEmployee");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        TempData["Error"] = "الملف المحدد فارغ.";
                        return RedirectToAction("AddFamilyToEmployee");
                    }

                    for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                    {
                        // قراءة الأعمدة بدءاً من العمود الثاني لتجنب قراءة Id
                        var employeeName = worksheet.Cells[row, 2].Value?.ToString();
                        var name = worksheet.Cells[row, 3].Value?.ToString();
                        var relativeTypeName = worksheet.Cells[row, 4].Value?.ToString();
                        var notes = worksheet.Cells[row, 5].Value?.ToString();

                        // العثور على الكيانات المرتبطة باستخدام الأسماء
                        var employee = _context.employee.FirstOrDefault(e => e.EmployeeName == employeeName);
                        var relativeType = _context.relativesTypes.FirstOrDefault(r => r.RelativeName == relativeTypeName);

                        if (employee != null && relativeType != null)
                        {
                            var family = new Family
                            {
                                EmployeeId = employee.Id,
                                Name = name,
                                RelativesTypeId = relativeType.Id,
                                Notes = notes
                            };

                            _context.Family.Add(family);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
            }

            TempData["Success"] = "تم استيراد البيانات بنجاح.";
            return RedirectToAction("Index");
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


        public IActionResult ExportPersonalDataToExcel()
        {
            // استرجاع البيانات التي تريد تصديرها إلى Excel
            var personalDataList = _context.personalDatas
                .Include(p => p.employee)
                .Include(p => p.Sex)
                .Include(p => p.Nationality)
                .Include(p => p.Religion)
                .Include(p => p.MaritalStatus)
                .Include(p => p.guarantees)
                .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("PersonalData");

                // إضافة العناوين
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "الموظف";

                worksheet.Cells[1, 3].Value = "تاريخ الميلاد";
                worksheet.Cells[1, 4].Value = "العمر";
                worksheet.Cells[1, 5].Value = "الجنس";
                worksheet.Cells[1, 6].Value = "الجنسية";
                worksheet.Cells[1, 7].Value = "الديانة";
                worksheet.Cells[1, 8].Value = "الحالة الإجتماعية";
                worksheet.Cells[1, 9].Value = "هاتف المنزل";
                worksheet.Cells[1, 10].Value = "الايميل";
                worksheet.Cells[1, 11].Value = "الموبايل";
                worksheet.Cells[1, 12].Value = "العنوان";
                worksheet.Cells[1, 13].Value = "الملاحظات";
                worksheet.Cells[1, 14].Value = "نوع البطاقة";
                worksheet.Cells[1, 15].Value = "صادرة من";
                worksheet.Cells[1, 16].Value = "رقم البطاقة";
                worksheet.Cells[1, 17].Value = "تاريخ الاصدار";
                worksheet.Cells[1, 18].Value = "تاريخ الانتهاء";
                worksheet.Cells[1, 19].Value = "الضمين";

                // إضافة البيانات
                for (int i = 0; i < personalDataList.Count; i++)
                {
                    var personalData = personalDataList[i];
                    var row = i + 2; // ابدأ من الصف الثاني لأن الصف الأول مخصص للعناوين

                    worksheet.Cells[row, 1].Value = personalData.Id;
                    worksheet.Cells[row, 2].Value = personalData.employee?.EmployeeName; // اسم الموظف
                    worksheet.Cells[row, 3].Value = personalData.DateOfBirth.ToString("yyyy-MM-dd");

                    worksheet.Cells[row, 4].Value = personalData.Age;
                    worksheet.Cells[row, 5].Value = personalData.Sex?.Name; // اسم الجنس
                    worksheet.Cells[row, 6].Value = personalData.Nationality?.NationalityName; // اسم الجنسية
                    worksheet.Cells[row, 7].Value = personalData.Religion?.Name; // اسم الديانة
                    worksheet.Cells[row, 8].Value = personalData.MaritalStatus?.Name; // اسم الحالة الاجتماعية
                    worksheet.Cells[row, 9].Value = personalData.HomePhone;
                    worksheet.Cells[row, 10].Value = personalData.Email;
                    worksheet.Cells[row, 11].Value = personalData.PhoneNumber;
                    worksheet.Cells[row, 12].Value = personalData.Address;
                    worksheet.Cells[row, 13].Value = personalData.Notes;
                    worksheet.Cells[row, 14].Value = personalData.CardType;
                    worksheet.Cells[row, 15].Value = personalData.ToRelease;
                    worksheet.Cells[row, 16].Value = personalData.CardNumber;
                    worksheet.Cells[row, 17].Value = personalData.ReleaseDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 18].Value = personalData.CardExpiryDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 19].Value = personalData.guarantees?.Name; // اسم الضمين

                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PersonalData.xlsx");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> ImportPersonalData(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        ModelState.AddModelError("", "يرجى تحديد ملف Excel صالح.");
        //        return View();
        //    }

        //    var personalDataList = new List<PersonalData>();
        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);

        //        using (var package = new ExcelPackage(stream))
        //        {
        //            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
        //            if (worksheet == null)
        //            {
        //                ModelState.AddModelError("", "الملف لا يحتوي على ورقة عمل.");
        //                return View();
        //            }

        //            var rowCount = worksheet.Dimension.Rows;

        //            for (int row = 2; row <= rowCount; row++)
        //            {
        //                try
        //                {
        //                    var employeeName = worksheet.Cells[row, 2].Text.Trim(); 
        //                    var dateOfBirth = worksheet.Cells[row, 3].Text.Trim();
        //                    var age = int.Parse(worksheet.Cells[row, 4].Text.Trim());
        //                    var homePhone = worksheet.Cells[row, 5].Text.Trim();
        //                    var email = worksheet.Cells[row, 6].Text.Trim();
        //                    var phoneNumber = worksheet.Cells[row, 7].Text.Trim();
        //                    var address = worksheet.Cells[row, 8].Text.Trim();
        //                    var notes = worksheet.Cells[row, 9].Text.Trim();
        //                    var cardType = worksheet.Cells[row, 10].Text.Trim();
        //                    var toRelease = worksheet.Cells[row, 11].Text.Trim();
        //                    var cardNumber = worksheet.Cells[row, 12].Text.Trim();
        //                    var releaseDate = worksheet.Cells[row, 13].Text.Trim();
        //                    var cardExpiryDate = worksheet.Cells[row, 14].Text.Trim();
        //                    var sex = worksheet.Cells[row, 15].Text.Trim();
        //                    var nationality = worksheet.Cells[row, 16].Text.Trim();
        //                    var religion = worksheet.Cells[row, 17].Text.Trim();
        //                    var maritalStatus = worksheet.Cells[row, 18].Text.Trim();
        //                    var guarantees = worksheet.Cells[row, 19].Text.Trim();




        //                    //EmployeeId = int.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
        //                    //DateOfBirth = DateOnly.Parse(worksheet.Cells[row, 3].Value?.ToString() ?? DateOnly.MinValue.ToString()),
        //                    //Age = int.Parse(worksheet.Cells[row, 4].Value?.ToString() ?? "0"),
        //                    //HomePhone = worksheet.Cells[row, 5].Value?.ToString(),
        //                    //Email = worksheet.Cells[row, 6].Value?.ToString(),
        //                    //PhoneNumber = worksheet.Cells[row, 7].Value?.ToString(),
        //                    //Address = worksheet.Cells[row, 8].Value?.ToString(),
        //                    //Notes = worksheet.Cells[row, 9].Value?.ToString(),
        //                    //CardType = worksheet.Cells[row, 10].Value?.ToString(),
        //                    //ToRelease = worksheet.Cells[row, 11].Value?.ToString(),
        //                    //CardNumber = int.Parse(worksheet.Cells[row, 12].Value?.ToString() ?? "0"),
        //                    //ReleaseDate = DateOnly.Parse(worksheet.Cells[row, 13].Value?.ToString() ?? DateOnly.MinValue.ToString()),
        //                    //CardExpiryDate = DateOnly.Parse(worksheet.Cells[row, 14].Value?.ToString() ?? DateOnly.MinValue.ToString()),
        //                    //SexId = int.Parse(worksheet.Cells[row, 15].Value?.ToString() ?? "0"),
        //                    //NationalityId = int.Parse(worksheet.Cells[row, 16].Value?.ToString() ?? "0"),
        //                    //ReligionId = int.Parse(worksheet.Cells[row, 17].Value?.ToString() ?? "0"),
        //                    //MaritalStatusId = int.Parse(worksheet.Cells[row, 18].Value?.ToString() ?? "0"),
        //                    //GuaranteesId = int.Parse(worksheet.Cells[row, 19].Value?.ToString() ?? "0")




        //                    var employee = _context.employee.FirstOrDefault(e => e.EmployeeName == employeeName);
        //                    if (employee == null)
        //                    {
        //                        continue; // تخطي إذا لم يتم العثور على الموظف
        //                    }
        //                    var seX = _context.sex.FirstOrDefault(e => e.Name == sex);
        //                    if (seX == null)
        //                    {
        //                        continue; // تخطي إذا لم يتم العثور على الموظف
        //                    }

        //                    var nationalitY = _context.nationality.FirstOrDefault(e => e.NationalityName == nationality);
        //                    if (nationalitY == null)
        //                    {
        //                        continue; // تخطي إذا لم يتم العثور على الموظف
        //                    }

        //                    var religIon = _context.religion.FirstOrDefault(e => e.Name == religion);
        //                    if (religIon == null)
        //                    {
        //                        continue; // تخطي إذا لم يتم العثور على الموظف
        //                    }

        //                    var maritalStatuss = _context.maritalStatuses.FirstOrDefault(e => e.Name == maritalStatus);
        //                    if (maritalStatuss == null)
        //                    {
        //                        continue; // تخطي إذا لم يتم العثور على الموظف
        //                    }


        //                    var personalData = new PersonalData
        //                    {
        //                        EmployeeId = int.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
        //                        DateOfBirth = DateOnly.Parse(worksheet.Cells[row, 3].Value?.ToString() ?? DateOnly.MinValue.ToString()),
        //                        Age = int.Parse(worksheet.Cells[row, 4].Value?.ToString() ?? "0"),
        //                        HomePhone = worksheet.Cells[row, 5].Value?.ToString(),
        //                        Email = worksheet.Cells[row, 6].Value?.ToString(),
        //                        PhoneNumber = worksheet.Cells[row, 7].Value?.ToString(),
        //                        Address = worksheet.Cells[row, 8].Value?.ToString(),
        //                        Notes = worksheet.Cells[row, 9].Value?.ToString(),
        //                        CardType = worksheet.Cells[row, 10].Value?.ToString(),
        //                        ToRelease = worksheet.Cells[row, 11].Value?.ToString(),
        //                        CardNumber = int.Parse(worksheet.Cells[row, 12].Value?.ToString() ?? "0"),
        //                        ReleaseDate = DateOnly.Parse(worksheet.Cells[row, 13].Value?.ToString() ?? DateOnly.MinValue.ToString()),
        //                        CardExpiryDate = DateOnly.Parse(worksheet.Cells[row, 14].Value?.ToString() ?? DateOnly.MinValue.ToString()),
        //                        SexId = int.Parse(worksheet.Cells[row, 15].Value?.ToString() ?? "0"),
        //                        NationalityId = int.Parse(worksheet.Cells[row, 16].Value?.ToString() ?? "0"),
        //                        ReligionId = int.Parse(worksheet.Cells[row, 17].Value?.ToString() ?? "0"),
        //                        MaritalStatusId = int.Parse(worksheet.Cells[row, 18].Value?.ToString() ?? "0"),
        //                        GuaranteesId = int.Parse(worksheet.Cells[row, 19].Value?.ToString() ?? "0")

        //                    };
        //                    PersonalData.Add(personalData);

        //                }
        //                catch (Exception ex)
        //                {
        //                    // سجل الخطأ للمراجعة
        //                    ModelState.AddModelError("", $"حدث خطأ في الصف {row}: {ex.Message}");
        //                }
        //            }
        //        }

        //            await _context.SaveChangesAsync();
                
        //    }

        //    ViewBag.Message = "Data imported successfully!";
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ImportPracticalExperiences11(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        ModelState.AddModelError("", "يرجى تحديد ملف Excel صالح.");
        //        return View();
        //    }

        //    var practicalExperiences = new List<PracticalExperiences>();

        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);

        //        using (var package = new ExcelPackage(stream))
        //        {
        //            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
        //            if (worksheet == null)
        //            {
        //                ModelState.AddModelError("", "الملف لا يحتوي على ورقة عمل.");
        //                return View();
        //            }

        //            var rowCount = worksheet.Dimension.Rows;

        //            for (int row = 2; row <= rowCount; row++)
        //            {
        //                try
        //                {
        //                    var employeeName = worksheet.Cells[row, 2].Text.Trim(); // العمود 2 للعمود "Employee Name"
        //                    var experiencesName = worksheet.Cells[row, 3].Text.Trim(); // العمود 3 للعمود "Experience Name"
        //                    var placeToGainExperience = worksheet.Cells[row, 4].Text.Trim(); // العمود 4 للعمود "Place To Gain Experience"
        //                    var fromDateText = worksheet.Cells[row, 5].Text.Trim(); // العمود 5 للعمود "From Date"
        //                    var toDateText = worksheet.Cells[row, 6].Text.Trim(); // العمود 6 للعمود "To Date"
        //                    var duration = worksheet.Cells[row, 7].Text.Trim(); // العمود 7 للعمود "Duration"

        //                    if (string.IsNullOrEmpty(employeeName) || string.IsNullOrEmpty(experiencesName) || string.IsNullOrEmpty(placeToGainExperience) || string.IsNullOrEmpty(fromDateText) || string.IsNullOrEmpty(toDateText))
        //                    {
        //                        continue; // تخطي الصفوف ذات البيانات المفقودة
        //                    }

        //                    var employee = _context.employee.FirstOrDefault(e => e.EmployeeName == employeeName);
        //                    if (employee == null)
        //                    {
        //                        continue; // تخطي إذا لم يتم العثور على الموظف
        //                    }

        //                    if (!DateTime.TryParse(fromDateText, out DateTime fromDate) || !DateTime.TryParse(toDateText, out DateTime toDate))
        //                    {
        //                        continue; // تخطي الصفوف ذات التواريخ غير الصالحة
        //                    }

        //                    var practicalExperience = new PracticalExperiences
        //                    {
        //                        EmployeeId = employee.Id,
        //                        ExperiencesName = experiencesName,
        //                        PlacToGainExperience = placeToGainExperience,
        //                        FromDate = DateOnly.FromDateTime(fromDate),
        //                        ToDate = DateOnly.FromDateTime(toDate),
        //                        Duration = duration
        //                    };

        //                    practicalExperiences.Add(practicalExperience);
        //                }
        //                catch (Exception ex)
        //                {
        //                    // سجل الخطأ للمراجعة
        //                    ModelState.AddModelError("", $"حدث خطأ في الصف {row}: {ex.Message}");
        //                }
        //            }
        //        }
        //    }

        //    if (practicalExperiences.Count > 0)
        //    {
        //        _context.practicalExperiences.AddRange(practicalExperiences);
        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToAction("Index"); // تعديل الوجهة حسب متطلباتك
        //}


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
        public async Task<IActionResult> Importguarantees(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                ModelState.AddModelError(string.Empty, "لم يتم تحميل ملف.");
                return RedirectToAction("Index");
            }

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError(string.Empty, "الرجاء تحميل ملف Excel.");
                return RedirectToAction("Index");
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            Guarantees guarantee = new Guarantees();

                            guarantee.Name = worksheet.Cells[row, 2].Value.ToString();
                            guarantee.PhoneNumber = worksheet.Cells[row, 3].Value.ToString();
                            guarantee.NameOfTheBusiness = worksheet.Cells[row, 4].Value.ToString();
                            if (int.TryParse(worksheet.Cells[row, 5].Value.ToString(), out int commercialRegistrationNumber))
                            {
                                guarantee.CommercialRegistrationNo = commercialRegistrationNumber;
                            }
                            guarantee.ShopAddress = worksheet.Cells[row, 6].Value.ToString();
                            guarantee.HomeAdress = worksheet.Cells[row, 7].Value.ToString();
                            guarantee.Notes = worksheet.Cells[row, 9].Value.ToString();

                            // التحقق من العدد الذي يعول
                            if (int.TryParse(worksheet.Cells[row, 8].Value.ToString(), out int numberOfDependents))
                            {
                                guarantee.NumberOfDependents = numberOfDependents;
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, $"خطأ في تنسيق عدد من يعول في الصف رقم {row}");
                                continue;
                            }

                            // التحقق من الحالة الاجتماعية باستخدام الاسم
                            string maritalStatusName = worksheet.Cells[row, 10].Value.ToString();
                            var maritalStatus = await _context.maritalStatuses.FirstOrDefaultAsync(ms => ms.Name == maritalStatusName);
                            if (maritalStatus != null)
                            {
                                guarantee.MaritalStatusId = maritalStatus.Id;
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, $"خطأ في تنسيق الحالة الاجتماعية في الصف رقم {row}");
                                continue;
                            }

                            _context.guarantees.Add(guarantee);
                        }
                        TempData["Success"] = "تم استيراد البيانات بنجاح.";

                        await _context.SaveChangesAsync();
                    }
                }


                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"حدث خطأ أثناء استيراد البيانات: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        // الإجراء لتصدير البيانات إلى ملف Excel
        public async Task<IActionResult> ExportToExcelGuarantees()
        {
            var guarantees = await _context.guarantees.Include(g => g.MaritalStatus).ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Guarantees");

                // إضافة رؤوس الأعمدة
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "اسم الضمين";
                worksheet.Cells[1, 3].Value = "الموبايل";
                worksheet.Cells[1, 4].Value = "اسم المحل التجاري";
                worksheet.Cells[1, 5].Value = "رقم السجل";
                worksheet.Cells[1, 6].Value = "عنوان المحل التجاري";
                worksheet.Cells[1, 7].Value = "عنوان السكن الدائم";
                worksheet.Cells[1, 8].Value = "عدد من يعول";
                worksheet.Cells[1, 9].Value = "الملاحظة";
                worksheet.Cells[1, 10].Value = "الحالة الإجتماعية";


                // إضافة البيانات إلى الأعمدة
                for (int i = 0; i < guarantees.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = guarantees[i].Id; // الـ ID
                    worksheet.Cells[i + 2, 2].Value = guarantees[i].Name;
                    worksheet.Cells[i + 2, 3].Value = guarantees[i].PhoneNumber;
                    worksheet.Cells[i + 2, 4].Value = guarantees[i].NameOfTheBusiness;
                    worksheet.Cells[i + 2, 5].Value = guarantees[i].CommercialRegistrationNo;
                    worksheet.Cells[i + 2, 6].Value = guarantees[i].ShopAddress;
                    worksheet.Cells[i + 2, 7].Value = guarantees[i].HomeAdress;
                    worksheet.Cells[i + 2, 8].Value = guarantees[i].NumberOfDependents;
                    worksheet.Cells[i + 2, 9].Value = guarantees[i].Notes;
                    worksheet.Cells[i + 2, 10].Value = guarantees[i].MaritalStatus?.Name; // التأكد من وجود الحالة الاجتماعية

                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Guarantees.xlsx";

                return File(stream, contentType, fileName);
            }
        }

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


        // Action Method لتصدير البيانات إلى ملف Excel


        public IActionResult ExportTrainingToExcel()
        {
            // استرجاع البيانات التي تريد تصديرها إلى Excel مع تضمين الكيانات ذات الصلة
            var training = _context.trainingCourses
                .Include(tc => tc.EmployeeOne)
                .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("TrainingCourses");

                // إضافة العناوين
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "الموظف";
                worksheet.Cells[1, 3].Value = "اسم الدورة";
                worksheet.Cells[1, 4].Value = "مكان الحصول عليها";
                worksheet.Cells[1, 5].Value = "من تاريخ";
                worksheet.Cells[1, 6].Value = "الى تاريخ";

                // إضافة البيانات
                for (int i = 0; i < training.Count; i++)
                {
                    var course = training[i];
                    worksheet.Cells[i + 2, 1].Value = course.Id; // Id
                    worksheet.Cells[i + 2, 2].Value = course.EmployeeOne?.EmployeeName; // اسم الموظف
                    worksheet.Cells[i + 2, 3].Value = course.NameCourses;
                    worksheet.Cells[i + 2, 4].Value = course.WhereToGetIt;
                    worksheet.Cells[i + 2, 5].Value = course.FromDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 6].Value = course.ToDate.ToString("yyyy-MM-dd");
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TrainingCourses.xlsx");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImportTrainingFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "يرجى اختيار ملف Excel.";
                return RedirectToAction("Index");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        TempData["Error"] = "الملف المحدد فارغ.";
                        return RedirectToAction("Index");
                    }

                    for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                    {
                        var employeeName = worksheet.Cells[row, 2].Value?.ToString();
                        var name = worksheet.Cells[row, 3].Value?.ToString();
                        var trainingName = worksheet.Cells[row, 4].Value?.ToString();
                        var fromDateValue = worksheet.Cells[row, 5].Value?.ToString();
                        var toDateValue = worksheet.Cells[row, 6].Value?.ToString();

                        var employee = _context.employee.FirstOrDefault(e => e.EmployeeName == employeeName);
                        if (employee == null)
                        {
                            continue; // تخطي إذا لم يتم العثور على الموظف
                        }

                        DateTime? fromDate = null;
                        DateTime? toDate = null;

                        if (DateTime.TryParse(fromDateValue, out DateTime parsedFromDate))
                        {
                            fromDate = parsedFromDate;
                        }

                        if (DateTime.TryParse(toDateValue, out DateTime parsedToDate))
                        {
                            toDate = parsedToDate;
                        }

                        var trainingCourse = new TrainingCourses
                        {
                            EmployeeId = employee.Id, // Use 'employee' here
                            NameCourses = worksheet.Cells[row, 3].Value?.ToString(),
                            WhereToGetIt = worksheet.Cells[row, 4].Value?.ToString(),
                            FromDate = DateOnly.FromDateTime(fromDate.GetValueOrDefault()), // Convert DateTime? to DateOnly
                            ToDate = DateOnly.FromDateTime(toDate.GetValueOrDefault()) // Convert DateTime? to DateOnly
                        };

                        _context.trainingCourses.Add(trainingCourse);
                    }

                    await _context.SaveChangesAsync();
                }
            }

            TempData["Success"] = "تم استيراد البيانات بنجاح.";
            return RedirectToAction("Index");
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
     
        public async Task<IActionResult> Profile(int Id )
        {
            if (Id==0 ) { 
                return NotFound();
            }
            var employee= _context.employee.Include(x=>x.JobDescription).Where(e => e.Id == Id).Select(x => new { authorities = x.JobDescription.Authorities, responsibilities = x.JobDescription.Responsibilities}).FirstOrDefault();
            var external = _context.AdditionalExternalOfWork.Where(e => e.EmployeeId == Id).ToList();
            return View(new { employee, external });
        }
        public async Task<IActionResult> details(int Id )
        {
            if(Id == 0)
            {
                return NotFound();
            }
            else
            {
                var employees = await _context.employee.Include(e => e.Departments)
                .Include(e => e.FingerprintDevices).Include(e => e.JobDescription)
                .Include(e => e.Manager).Include(e => e.Sections).FirstOrDefaultAsync(e => e.Id == Id);
                //-------------------------------------------------------
                var practicalExperiences = await _context.practicalExperiences
                    .Include(p => p.Employee).FirstOrDefaultAsync(e => e.EmployeeId == Id);
                //-------------------------------------------------------
                var family = await _context.Family.Include(f => f.Employees)
                    .Include(f => f.RelativesType).FirstOrDefaultAsync(e => e.EmployeeId == Id);
                //-------------------------------------------------------
                var personalData = await _context.personalDatas.Include(p => p.MaritalStatus)
                    .Include(p => p.Nationality).Include(p => p.Religion).Include(p => p.Sex)
                    .Include(p => p.employee).Include(p => p.guarantees).FirstOrDefaultAsync(e => e.EmployeeId == Id);
                //-------------------------------------------------------
                var guarantees = await _context.guarantees.Include(g => g.MaritalStatus).FirstOrDefaultAsync(e => e.Id == Id);
                //-------------------------------------------------------
                var FinancialStatements = await _context.financialStatements.Include(f => f.Currency).Include(f => f.employee).FirstOrDefaultAsync(e => e.EmployeeId == Id);
                //-------------------------------------------------------
                var TrainingCourses = await _context.trainingCourses.Include(t => t.EmployeeOne).FirstOrDefaultAsync(e => e.EmployeeId == Id);

                //-------------------------------------------------------
                var EmployeeArchives = await _context.EmployeeArchives.Include(e => e.employee).FirstOrDefaultAsync(e => e.EmployeeId == Id);

                //-------------------------------------------------------


                var viewModel = new EmployeeVM
                {
                    Employee = employees,
                    PracticalExperiences = practicalExperiences,
                    Family = family,
                    PersonalData = personalData,
                    Guarantees = guarantees,
                    FinancialStatements = FinancialStatements,
                    TrainingCourses = TrainingCourses,
                    EmployeeArchives = EmployeeArchives,

                };
                return View(viewModel);
            }
        }
        //TempData["Edit"] = "Edit";
        //var employeeViewModel = new EmployeeVM
        //{
        //    PersonalData = await _personalDatarepository.GetByIdAsync(id),
        //    Employee = await _employeeRepository.GetByIdAsync(id),
        //    Family = await _familyrepository.GetByIdAsync(id),
        //    PracticalExperiences = await _practicalExperiencesrepository.GetByIdAsync(id)
        //};
    }

}

