using BoatRentalSystem.API.MappingProfiles;
using BoatRentalSystem.Application.Services;
using BoatRentalSystem.Core.Interfaces;
using BoatRentalSystem.Infrastructure;
using BoatRentalSystem.Infrastructure.Repositories;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<IAdditionRepository, AdditionRepository>();
builder.Services.AddScoped<AdditionService>();

//Start AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));
//End  AutoMapper Configuration

//Start  DB Configuration 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
//End  DB Configuration 

//Start Serilog Configuration
var configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .Build();

    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

    builder.Host.UseSerilog();
//End Serilog Configuration

//Start Hang fire Configuration
builder.Services.AddHangfire(config =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    config.UseSqlServerStorage(connectionString);
});

//End Hang fire Configuration


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Start Hang fire Configuration
app.UseHangfireDashboard("/dashboard");
app.UseHangfireServer();
//End Hang fire Configuration


app.Run();
