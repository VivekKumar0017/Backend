using Backend.Logic;
using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AdmissionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Cms"));
});
builder.Services.AddScoped<IDataRepository<College,int>, CollegeRepository>();
builder.Services.AddScoped<IDataRepository<Course, int>, CourseRepository>();

builder.Services.AddScoped<IStudentRepository<Student,int>, StudentRepository>();



builder.Services.AddControllers()
 .AddJsonOptions(options =>
  {
      // Supress thye default JSON Serialization Rule (camelCase)
      // in this case the API will respond JSON data in
      // as-it-is Property Names in Properties of Model Classes
      options.JsonSerializerOptions.PropertyNamingPolicy = null;
  });
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

app.Run();
