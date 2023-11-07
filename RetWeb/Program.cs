using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetWeb.DataAccess.Data;
using RetWeb.Utility;
using RetWeb.DataAccess.IRepository;
using RetWeb.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//configure the program that secrets are injected inside the stripeSettings with their values
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

//To add the role we have to customize the AddIdentity
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); 

//this will help us to direct the right url for authorization when user doesn't have access this needs to be added after adding the IdentityService
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe: SecretKey").Get<string>(); // set the configuration for stripe
app.UseRouting();
app.UseAuthentication(); // Add the authentication before authorization
app.UseAuthorization();
app.MapRazorPages();  // Add the routing for the razor pages
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
