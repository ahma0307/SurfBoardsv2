using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurfBoardsv2.Data;
using SurfBoardsv2.Models;
using SurfBoardsv2.Areas.Identity.Pages.Account;
using Microsoft.VisualBasic;
using SurfBoardsv2.Core;
using Constants = SurfBoardsv2.Core.Constants;
using SurfBoardsv2.Core.Repositories;
using SurfBoardsv2.Repositories;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = "168184782830103";
        options.AppSecret = "893eaf058493e5c3bff89ef488d7e221";
    })
    .AddGoogle(options =>
    {
        options.ClientId = "582965217548-i6dlvsnk4nrvhvpmlde1huaa1d8a9hh6.apps.googleusercontent.com";
        options.ClientSecret = "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";
    })
    .AddCookie();


builder.Services.AddDefaultIdentity<SurfBoardsv2User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
#region Authorization

AddAuthorizationPolicies();



#endregion



AddScoped();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
        options.AddPolicy(Constants.Policies.RequireManager, policy => policy.RequireRole(Constants.Roles.Manager));


    });
}

void AddScoped()
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
}
