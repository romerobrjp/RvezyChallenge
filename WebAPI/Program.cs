using Business.Services.Interfaces;
using Business.Services;
using Infra.Data.Models.Context;
using Microsoft.EntityFrameworkCore;
using Infra.Data.Repositories;
using Infra.Data.Repositories.Interfaces;
using Infra.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MainDbContext>(options => options
  .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
  .UseSnakeCaseNamingConvention());
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddControllers();
builder.Services.AddRouting(_ => _.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

try
{
  Console.WriteLine($">>> Starting up {builder.Environment.ApplicationName} on {builder.Environment.EnvironmentName} environment.");
  app.SeedData();
  app.Run();
}
catch (Exception ex)
{
  Console.WriteLine($">>> Application {builder.Environment.ApplicationName} start-up failed. Reason: {ex.Message}");
}

