using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using N.G.HRS.Date;
using N.G.HRS.Repository;
using N.G.HRS.Repository.File_Upload;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.FingerPrintSetting;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // √Ê .Commercial Õ”» ‰Ê⁄ «· —ŒÌ’ «·–Ì  „·ﬂÂ

// Configure Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();


 
// Dependency Injection
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<Periods, Periods>();
// Uncomment and configure the following lines if ZkemClient and CZKEM are needed
// builder.Services.AddScoped<ZkemClient, ZkemClient>();
// builder.Services.AddScoped<ZkemClient, ZkemClient>(provider =>
// {
//     var czkem = new CZKEM();
//     return new ZkemClient(czkem);
// });
builder.Services.AddScoped<IFileUploadService, FileUploadService>();

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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Configure endpoint routing
    endpoints.MapControllerRoute(
        name: "area",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    //// Redirect to the login page on root access
    //endpoints.MapGet("/", context =>
    //{
    //    context.Response.Redirect("/RegisterAndLogin/Logins/Create");
    //    return System.Threading.Tasks.Task.CompletedTask;
    //});
});

app.Run();
