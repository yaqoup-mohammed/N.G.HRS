#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using N.G.HRS.Date;

namespace N.G.HRS.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager; // Inject RoleManager

        private readonly AppDbContext _context;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context
            
            ) // Add RoleManager to constructor
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager; // Assign RoleManager
            _context = context;

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "كلمة المرور وتأكيد كلمة المرور غير متطابقين.")]
            public string ConfirmPassword { get; set; }


            [Required]
            [RegularExpression("^[0-9]*$", ErrorMessage = "يرجى إدخال أرقام فقط")]

            [Display(Name = "Employee Number Jop")]
            public int EmployeeNumberJop { get; set; }

            [Display(Name = "Add Permission")]
            public bool AddPermission { get; set; }

            [Display(Name = "Edit Permission")]
            public bool EditPermission { get; set; }

            [Display(Name = "View Permission")]
            public bool ViewPermission { get; set; }

            [Display(Name = "Admin Permission")]
            public bool AdminPermission { get; set; }

            [Display(Name = "Delete Permission")]
            public bool DeletePermission { get; set; }

            [Display(Name = "Details Permission")]
            public bool DetailsPermission { get; set; }

            [Display(Name = "Male Photo")]
            public bool MalePhoto { get; set; }

            [Display(Name = "Female Photo")]
            public bool FemalePhoto { get; set; }

            [Display(Name = "Profile Permission")]
            public bool ProfilePermission { get; set; }

        }

       
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return Forbid(); // تمنع الوصول للمستخدمين الذين ليس لديهم دور "Admin"
            }

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return Page();
        }



        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        //{
        //    returnUrl ??= Url.Content("~/");
        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //    if (ModelState.IsValid)
        //    {
        //        // استعلام عن بيانات الموظف باستخدام الرقم الوظيفي
        //        var employee = await _context.employee
        //            .Include(e => e.personalData)
        //            .ThenInclude(pd => pd.Sex)
        //            .FirstOrDefaultAsync(e => e.EmployeeNumber == Input.EmployeeNumberJop);

        //        if (employee == null)
        //        {
        //            ModelState.AddModelError(string.Empty, "رقم الموظف غير موجود.");
        //            return Page();
        //        }

        //        var user = CreateUser();

        //        await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
        //        await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
        //        var result = await _userManager.CreateAsync(user, Input.Password);

        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation("قام المستخدم بإنشاء حساب جديد بكلمة مرور.");

        //            // إضافة الأدوار بناءً على الاختيارات
        //            await EnsureRoleExistsAsync("Add");
        //            await EnsureRoleExistsAsync("Edit");
        //            await EnsureRoleExistsAsync("View");
        //            await EnsureRoleExistsAsync("Admin");
        //            await EnsureRoleExistsAsync("Delete");
        //            await EnsureRoleExistsAsync("Details");
        //            await EnsureRoleExistsAsync("MalePhoto");
        //            await EnsureRoleExistsAsync("FemalePhoto");
        //            await EnsureRoleExistsAsync("Profile");


        //            if (Input.AddPermission)
        //            {
        //                await _userManager.AddToRoleAsync(user, "Add");
        //            }
        //            if (Input.EditPermission)
        //            {
        //                await _userManager.AddToRoleAsync(user, "Edit");
        //            }
        //            if (Input.ViewPermission)
        //            {
        //                await _userManager.AddToRoleAsync(user, "View");
        //            }
        //            if (Input.AdminPermission)
        //            {
        //                await _userManager.AddToRoleAsync(user, "Admin");
        //            }
        //            if (Input.DeletePermission)
        //            {
        //                await _userManager.AddToRoleAsync(user, "Delete");
        //            }
        //            if (Input.DetailsPermission)
        //            {
        //                await _userManager.AddToRoleAsync(user, "Details");
        //            } if (Input.ProfilePermission)
        //            {
        //                await _userManager.AddToRoleAsync(user, "Profile");
        //            }

        //            // إضافة صلاحيات الصور بناءً على الجنس
        //            if (employee.personalData.Sex.Name == "ذكر" && Input.MalePhoto)
        //            {
        //                await _userManager.AddToRoleAsync(user, "MalePhoto");
        //            }
        //            else if (employee.personalData.Sex.Name == "أنثى" && Input.FemalePhoto)
        //            {
        //                await _userManager.AddToRoleAsync(user, "FemalePhoto");
        //            }

        //            // تأكيد البريد الإلكتروني وإلغاء قفل الحساب
        //            user.EmailConfirmed = true;
        //            user.LockoutEnabled = false;
        //            await _userManager.UpdateAsync(user);

        //            var userId = await _userManager.GetUserIdAsync(user);
        //            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //            var callbackUrl = Url.Page(
        //                "/Account/ConfirmEmail",
        //                pageHandler: null,
        //                values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
        //                protocol: Request.Scheme);

        //            await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
        //                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //            if (_userManager.Options.SignIn.RequireConfirmedAccount)
        //            {
        //                return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
        //            }
        //            else
        //            {
        //                await _signInManager.SignInAsync(user, isPersistent: false);
        //                return LocalRedirect(returnUrl);
        //            }
        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    // إذا وصلنا إلى هنا، فإن شيئًا ما قد فشل، وأعد عرض النموذج
        //    return Page();
        //}
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                // استعلام عن بيانات الموظف باستخدام الرقم الوظيفي
                var employee = await _context.employee
                    .Include(e => e.personalData)
                    .ThenInclude(pd => pd.Sex)
                    .FirstOrDefaultAsync(e => e.EmployeeNumber == Input.EmployeeNumberJop);

                if (employee == null)
                {
                    ModelState.AddModelError(string.Empty, "رقم الموظف غير موجود.");
                    return Page();
                }

                if (employee.personalData == null)
                {
                    ModelState.AddModelError(string.Empty, "البيانات الشخصية للموظف غير موجودة.");
                    return Page();
                }

                if (employee.personalData.Sex == null)
                {
                    ModelState.AddModelError(string.Empty, "الجنس غير محدد في البيانات الشخصية للموظف.");
                    return Page();
                }

                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("قام المستخدم بإنشاء حساب جديد بكلمة مرور.");

                    // إضافة الأدوار بناءً على الاختيارات
                    await EnsureRoleExistsAsync("Add");
                    await EnsureRoleExistsAsync("Edit");
                    await EnsureRoleExistsAsync("View");
                    await EnsureRoleExistsAsync("Admin");
                    await EnsureRoleExistsAsync("Delete");
                    await EnsureRoleExistsAsync("Details");
                    await EnsureRoleExistsAsync("MalePhoto");
                    await EnsureRoleExistsAsync("FemalePhoto");
                    await EnsureRoleExistsAsync("Profile");

                    if (Input.AddPermission)
                    {
                        await _userManager.AddToRoleAsync(user, "Add");
                    }
                    if (Input.EditPermission)
                    {
                        await _userManager.AddToRoleAsync(user, "Edit");
                    }
                    if (Input.ViewPermission)
                    {
                        await _userManager.AddToRoleAsync(user, "View");
                    }
                    if (Input.AdminPermission)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    if (Input.DeletePermission)
                    {
                        await _userManager.AddToRoleAsync(user, "Delete");
                    }
                    if (Input.DetailsPermission)
                    {
                        await _userManager.AddToRoleAsync(user, "Details");
                    }
                    if (Input.ProfilePermission)
                    {
                        await _userManager.AddToRoleAsync(user, "Profile");
                    }

                    // إضافة صلاحيات الصور بناءً على الجنس
                    if (employee.personalData.Sex.Name == "ذكر" && Input.MalePhoto)
                    {
                        await _userManager.AddToRoleAsync(user, "MalePhoto");
                    }
                    else if (employee.personalData.Sex.Name == "أنثى" && Input.FemalePhoto)
                    {
                        await _userManager.AddToRoleAsync(user, "FemalePhoto");
                    }

                    // تأكيد البريد الإلكتروني وإلغاء قفل الحساب
                    user.EmailConfirmed = true;
                    user.LockoutEnabled = false;
                    await _userManager.UpdateAsync(user);

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // إذا وصلنا إلى هنا، فإن شيئًا ما قد فشل، وأعد عرض النموذج
            return Page();
        }

        [HttpGet("ValidateEmployeeNumber")]
        public async Task<IActionResult> ValidateEmployeeNumber(int employeeNumber)
        {
            var employeeExists = await _context.employee.AnyAsync(e => e.EmployeeNumber == employeeNumber);
            return new JsonResult(new { isValid = employeeExists });
        }



        [HttpGet]
        public async Task<IActionResult> OnGetValidateEmployeeNumberAsync(int employeeNumber)
        {
            // تأكد من أن الحقل مطابق لما هو موجود في قاعدة البيانات
            var employeeExists = await _context.employee.AnyAsync(e => e.EmployeeNumber == employeeNumber);
            return new JsonResult(new { isValid = employeeExists });
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("تتطلب واجهة المستخدم الافتراضية وجود متجر مستخدم مع دعم عبر البريد الإلكتروني.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }

        private async Task EnsureRoleExistsAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

      
    }
}


