using Microsoft.EntityFrameworkCore;
using N5Company.DataAccess;
using N5Company.DataAccess.UnitOfWork;
using N5Company.Domain.Interfaces;
using Serilog.Events;
using Serilog;
using System.Text.Json.Serialization;
using Nest;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateLogger();

services.AddSingleton<IElasticClient>(provider =>
{
	var configuration = provider.GetRequiredService<IConfiguration>();
	var uri = new Uri(configuration["ElasticsearchSettings:Uri"]);
	var settings = new ConnectionSettings(uri)
		.DefaultIndex("Permissions");
	return new ElasticClient(settings);
});

services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddControllers(option => option.EnableEndpointRouting = false).AddJsonOptions(options =>
{
	options.JsonSerializerOptions.PropertyNamingPolicy = null;
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

//DataBase
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

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

app.Run();
