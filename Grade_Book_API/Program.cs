using Grade_Book_API;
using Grade_Book_API.Entities;
using Grade_Book_API.Middleware;
using Grade_Book_API.Services;
using Microsoft.AspNetCore.Identity;
using NLog.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseNLog();
builder.Services.AddControllers();
builder.Services.AddDbContext<GradeBookDbContext>();
builder.Services.AddScoped<GradeBookSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IFinalGradeService, FinalGradeService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<Student>, PasswordHasher<Student>>();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<GradeBookSeeder>();

seeder.Seed();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Grade book API");
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
