using Microsoft.EntityFrameworkCore;
using PensionManagementPensionerService.Models.Context;
using PensionManagementPensionerService.Models.Repository.Implementation;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Pensioner_Log1.txt", rollingInterval:RollingInterval.Hour)
    .MinimumLevel.Information()    
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PMSConnectionString"));
});

builder.Services.AddScoped<IPensionPlanRepository, PensionPlanRepository>();
builder.Services.AddScoped<IPensionerRepository, PensionerRepository>();
builder.Services.AddScoped<IGuardianRepository, GuardianRepository>();
builder.Services.AddCors(x => x.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
