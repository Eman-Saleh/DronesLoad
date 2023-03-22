using DronesLoad.DB;
using DronesLoad.Repositories;
using DronesLoad.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<DronesDBContext>(x => x.UseSqlServer(connectionString));// for appsetting 


var connectionString = new SqlConnectionStringBuilder(
		builder.Configuration.GetConnectionString("DronesconnectionString"));

builder.Services.AddDbContext<DronesDBContext>(options =>
{
	options.UseSqlServer(connectionString.ConnectionString);
});// for using ecret file

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
app.Logger.LogInformation("Starting the app");

app.Run();
