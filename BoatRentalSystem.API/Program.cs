using BoatRentalSystem.API.Helpers;
using BoatRentalSystem.API.MappingProfiles;
using BoatRentalSystem.Application.Services;
using BoatRentalSystem.Core.Interfaces;
using BoatRentalSystem.Infrastructure;
using BoatRentalSystem.Infrastructure.Repositories;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region RegisterRepo&Service
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<IAdditionRepository, AdditionRepository>();
builder.Services.AddScoped<AdditionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
#endregion

#region AutoMapper
//Start AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));
//End  AutoMapper Configuration
#endregion

#region DbConfiguration
//Start  DB Configuration 
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
//End  DB Configuration 
#endregion

#region  Serilog
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

#endregion

# region Hangfire
//Start Hang fire Configuration
builder.Services.AddHangfire(config =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    config.UseSqlServerStorage(connectionString);
});
//End Hang fire Configuration
#endregion

#region JWTConfiguration
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters 
    { 
        ValidateIssuerSigningKey = true,
        ValidateIssuer= true,
        ValidateAudience= true,
        ValidateLifetime= true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Issuer"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
});
#endregion

// JWT Configuration
builder.Services.Configure<Jwt>(configuration.GetSection("Jwt"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Start Hang fire Configuration
app.UseHangfireDashboard("/dashboard");
app.UseHangfireServer();
//End Hang fire Configuration


app.Run();
