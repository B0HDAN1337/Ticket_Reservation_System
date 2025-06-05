using Microsoft.EntityFrameworkCore;
using System_Rezerwacji_Biletów.Repository;
using System_Rezerwacji_Biletów.Data;
using System_Rezerwacji_Biletów.Service;
using System_Rezerwacji_Biletów.Models;
using FluentValidation;
using System_Rezerwacji_Biletów.Validator;
using System_Rezerwacji_Biletów.ViewModels;
using System_Rezerwacji_Biletów.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

//using System_Rezerwacji_Biletów.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("pl-PL")
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures; 
});

//Add database
builder.Services.AddDbContext<SystemReservationContext>(options => 
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<SystemReservationContext>();
builder.Services.AddRazorPages();

static async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string[] roleNames = { "Admin", "Manager", "User" };

    foreach (var roleName in roleNames)
    {
        if(!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    await CreateUserWithRole(userManager, "admin@email.com", "Admin!123", "Admin");
    await CreateUserWithRole(userManager, "manager@email.com", "Manager!123", "Manager");
}

static async Task CreateUserWithRole(UserManager<IdentityUser> userManager, string email, string password, string role )
{
    var user = await userManager.FindByEmailAsync(email);

    if(user == null)
    {
        user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);
        if(result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ManagerOrAdmin", policy => policy.RequireRole("Manager", "Admin"));
    options.AddPolicy("AllUsers", policy => policy.RequireRole("User", "Manager", "Admin"));
});

//Repository
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
//builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

//Service
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITicketService, TIcketService>();
//builder.Services.AddScoped<IReservationService, ReservationService>();

//Validator
builder.Services.AddScoped<IValidator<UserViewModel>, UserValidator>();
builder.Services.AddScoped<IValidator<EventViewModel>, EventValidator>();
builder.Services.AddScoped<IValidator<TicketViewModel>, TicketValidator>();

//Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.UseRequestLocalization();

InitializeDatabase(app);

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

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

void InitializeDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<SystemReservationContext>();
            DbInitializer.Initializer(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
}

app.Run();





