
using CarPoolingApplication.Data;
using CarPoolingApplication.Services.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using CarPoolingApplication.Services.Repository.Services;
using CarPoolingApplication.Data.Interfaces;
using CarPoolingApplication.Data.Repository;
using CarPoolingApplication.Helpers;
using CarPoolingApplication;
using IdentityServer4;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Configuration;

Log.Logger = new LoggerConfiguration().WriteTo.File("Logs/log.txt",rollingInterval:RollingInterval.Day).CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
try
{
    Log.Information("Starting web host");
    builder.Services.AddControllers();
    
    var configuration = new ConfigurationBuilder() .AddJsonFile("appsettings.json").Build();
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    configuration = new ConfigurationBuilder().AddConfiguration(configuration).AddJsonFile($"appsettings.{env}.json").Build();
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var variable = builder.Configuration.GetValue<string>("var1");
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"
            ), b => b.MigrationsAssembly("CarPoolingApplication"));
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options => options.AddPolicy(name: "AllowOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
        }));
    builder.Services.AddScoped<IUsers, UsersService>();
    builder.Services.AddScoped<IOfferedRides, OfferedRidesService>();
    builder.Services.AddScoped<IBookedRides, BookedRidesService>();
    builder.Services.AddScoped<ILogin, LoginService>();
    builder.Services.AddScoped<IUsersRepository, UsersRepository>();
    builder.Services.AddScoped<IOfferedRidesRepository, OfferedRidesRepository>();
    builder.Services.AddScoped<IBookedRidesRepository, BookedRidesRepository>();
    builder.Services.AddScoped<ILoginRepository, LoginRepository>();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = builder.Configuration["JWT:Issuer"],
             ValidAudience = builder.Configuration["JWT:Audience"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
         };
     });
    /* builder.Services.AddIdentityServerAuthentication(options =>
     {
         options.Authority = "http://localhost:5000";
         options.ApiName = "CarPoolingApplication";
         options.RequireHttpsMetadata = false;
     });*/
    IdentityModelEventSource.ShowPII = true;
    builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors("AllowOrigin");

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}