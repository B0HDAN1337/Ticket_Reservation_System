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

//using System_Rezerwacji_Biletów.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add database
builder.Services.AddDbContext<SystemReservationContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SystemReservationContext>();
builder.Services.AddRazorPages();

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

app.Run();



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

