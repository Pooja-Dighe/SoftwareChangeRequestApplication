using Microsoft.EntityFrameworkCore;
using SCRSApplication.Data;
using Microsoft.AspNetCore.Identity;
using SCRSApplication.Services;
using Microsoft.AspNetCore.Identity.UI.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDBContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders().AddRoles<IdentityRole>().
    AddEntityFrameworkStores<ApplicationDBContext>();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDBContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddControllersWithViews(); // For MVC
builder.Services.AddRazorPages(); // For Razor Pages


var app = builder.Build();

var scope = app.Services.CreateScope();                        // use to define or seed the role at application startup
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

var roles = new[] { "Manager", "Developer", "User" };

foreach (var role in roles)
{
    if (!await roleManager.RoleExistsAsync(role))
    {
        await roleManager.CreateAsync(new IdentityRole(role));
    }
}

//var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
//var adminUser = await userManager.FindByEmailAsync("Sam@outlook.com");

//if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Manager"))
//{
//    await userManager.AddToRoleAsync(adminUser, "Manager");
//}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute(); // For MVC
app.MapRazorPages();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
