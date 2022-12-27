using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using server.Database;
using server.Services.LoginService;
using server.Services.ResponseService;
using server.Validations.Classes;
using server.Validations.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
/* DATABASE SETUP */
builder.Services.AddDbContext<DBMain>(options =>
{
    var mainConnectionString = builder.Configuration.GetConnectionString("DBConnection");
    if (mainConnectionString != null)
    {
        mainConnectionString = mainConnectionString.Replace("_MAIN_", "_MAIN_" + DateTime.Now.Year.ToString());
        options.UseSqlServer(mainConnectionString);
    }
    else
    {
        Console.WriteLine("ERROR: Unable to connect to SQL server(Main)");
    }
});
builder.Services.AddDbContext<DBRegistries>(options =>
{
    var dbregistriesconnection = builder.Configuration.GetConnectionString("DBConnection");
    if(dbregistriesconnection != null)
    {
        dbregistriesconnection = dbregistriesconnection.Replace("_MAIN_", "_REGISTRIES_");
        options.UseSqlServer(dbregistriesconnection);
    }
    else
    {
        Console.WriteLine("ERROR: Unable to connect to SQL server(Registries)");
    }
});

/* MODEL USERS */
builder.Services.AddScoped<server.Repositories.Interfaces.IUser, server.Repositories.Classes.UserRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserValidations, UserValidations>();
/* SCHOOL LIST */
builder.Services.AddScoped<server.Repositories.Interfaces.ISchoolList, server.Repositories.Classes.SchoolListRepositorycs>();
builder.Services.AddScoped<ISchoolListValidations, SchoolListValidations>();
/* CLASS DEPARTMENTS */
builder.Services.AddScoped<server.Repositories.Interfaces.IClassDepartment, server.Repositories.Classes.ClassDepartmentRepository>();
builder.Services.AddScoped<IClassDepartmentValidations, ClassDepartmentValidations>();
/* CDSP */
builder.Services.AddScoped<server.Repositories.Interfaces.ICDSP, server.Repositories.Classes.CDSPRepository>();
builder.Services.AddScoped<ICDSPValidations, CDSPValidations>();
/* STUDENT GRADES */
builder.Services.AddScoped<server.Repositories.Interfaces.IStudentGrades, server.Repositories.Classes.StudentGradesReposiory>();
builder.Services.AddScoped<IStudentGradesValidations, StudentGradesValidations>();
/* SUBJECTS */
builder.Services.AddScoped<server.Repositories.Interfaces.ISubjects, server.Repositories.Classes.SubjectRepository>();
builder.Services.AddScoped<server.Validations.Interfaces.ISubjectValidations, server.Validations.Classes.SubjectValidations>();
 
/* FUNCTIONS */
builder.Services.AddSingleton<IResponseService, ResponseService>(); 
/* AUTOMAPPER */
builder.Services.AddAutoMapper(typeof(Program).Assembly);
/* TOKEN */
builder.Services.AddScoped<server.Repositories.Interfaces.ITokenHandler, server.Repositories.Classes.TokenHandlerRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    }); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
