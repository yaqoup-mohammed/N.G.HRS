using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using N.G.HRS.Repository;
using N.G.HRS.Repository.File_Upload;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.FingerPrintSetting;
using OfficeOpenXml;
using N.G.HRS.Date;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

// Configure Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

// Dependency Injection
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<Periods, Periods>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();



// Add Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AddPolicy", policy => policy.RequireRole("Add"));
    options.AddPolicy("EditPolicy", policy => policy.RequireRole("Edit"));
    options.AddPolicy("ViewPolicy", policy => policy.RequireRole("View"));
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("DeletePolicy", policy => policy.RequireRole("Delete"));
    options.AddPolicy("DetailsPolicy", policy => policy.RequireRole("Details"));
    options.AddPolicy("MalePhotoPolicy", policy => policy.RequireRole("MalePhoto"));
    options.AddPolicy("FemalePhotoPolicy", policy => policy.RequireRole("FemalePhoto"));
    options.AddPolicy("ProfilePolicy", policy => policy.RequireRole("Profile"));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// إنشاء حساب مدير دائم
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var existingUser = await userManager.FindByNameAsync("adminn@example.com");

    if (existingUser == null)
    {
        var adminUser = new IdentityUser { UserName = "adminn@example.com", Email = "adminn@example.com" };
        var result = await userManager.CreateAsync(adminUser, "Yaqoup@1234");
        if (result.Succeeded) 
        {
            // إضافة أدوار "Admin" للمستخدم
            var roles = new[] { "Add", "Edit", "View", "Admin", "Delete", "Details", "MalePhoto", "FemalePhoto", "Profile" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                await userManager.AddToRoleAsync(adminUser, role);
            }
            // تأكيد البريد الإلكتروني
            adminUser.EmailConfirmed = true;
            await userManager.UpdateAsync(adminUser);
        }
    }
    else
    {
        // تأكد من أن الحساب الرئيسي لديه جميع الأدوار
        var roles = new[] { "Add", "Edit", "View", "Admin", "Delete", "Details", "MalePhoto", "FemalePhoto", "Profile" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
            if (!await userManager.IsInRoleAsync(existingUser, role))
            {
                await userManager.AddToRoleAsync(existingUser, role);
            }
        }
    }
}

app.UseEndpoints(endpoints =>
{
    // Configure endpoint routing
    endpoints.MapControllerRoute(
        name: "area",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});

app.Run();





