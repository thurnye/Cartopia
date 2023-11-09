using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cartopia.DataAccess.Data;
using Cartopia.Utility;
using Cartopia.DataAccess.IRepository;
using Cartopia.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using Cartopia.DataAccess.DBInitializer;

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

//configure facebook auth
builder.Services.AddAuthentication().AddFacebook(option =>
{
    option.AppId = "881289470248130";
    option.AppSecret = "bdca7cdcaa93dcf88a0ee475f0aedeca";
});

//This will help us with adding the sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
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
//Configure the Stripe API key
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>(); // set the configuration for stripe
app.UseRouting();
app.UseAuthentication(); // Add the authentication before authorization
app.UseAuthorization();
app.UseSession(); //add session to the pipeline
SeedDatabase(); // we invoke the initializer seedDatabase Method
app.MapRazorPages();  // Add the routing for the razor pages
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

//db initialize helper method by adding it to the pipeline

void SeedDatabase ()
{
    using(var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
