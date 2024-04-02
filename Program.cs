using Backend.Customization.SecurityInfra;
using Backend.Logic;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AdmissionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Cms"));
});
builder.Services.AddDbContext<AppSecurityDbContext>(options =>
{
  
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecurityConnStr"));

    
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppSecurityDbContext>();
builder.Services.AddScoped<SecurityManagement>();
builder.Services.AddAuthentication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", policy =>
    {
        // Allowing any browser client to access the API
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});



builder.Services.AddScoped<IDataRepository<College,int>, CollegeRepository>();
builder.Services.AddScoped<ICourseRepository<Course, int>, CourseRepository>();

builder.Services.AddScoped<IStudentRepository<Student,int>, StudentRepository>();
builder.Services.AddScoped<IStudentReportRepository<StudentReport,int>, StudentReportRepository>();
/*builder.Services.AddScoped<IStudentRepository<Admission, int>, AdmissionRepository>();*/



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("StudentPolicy", policy =>
    {
        policy.RequireRole("Student");
    });

    options.AddPolicy("CollegePolicy", policy =>
    {
        policy.RequireRole("College");
    });
});




builder.Services.AddControllers()
 .AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
     // Optionally, you can also configure other JSON serialization options here
     options.JsonSerializerOptions.PropertyNamingPolicy = null; // Suppress camel case conversion
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
app.UseAuthentication();
app.UseCors("cors");

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
