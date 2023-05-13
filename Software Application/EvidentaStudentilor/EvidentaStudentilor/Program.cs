using Microsoft.EntityFrameworkCore;
using EvidentaStudentilor.Repositories;
using EvidentaStudentilor.LogicServices;
using EvidentaStudentilor.RepositoryInterfaces;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. It is used Inversion of Control (IoC).
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EvidentaStudentilorContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EvidentaStudentilorDB")));

// For HttpContext (session is used for authentication and authorize)
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// Registered Repositories
builder.Services.AddScoped<IWrapperRepository, WrapperRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ICurriculaRepository, CurriculaRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

// Registered Logic Services
builder.Services.AddScoped<ICurriculaService, CurriculaService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

// Services for user authentication with Google and Facebook account
builder.Services.AddAuthentication(o =>{ o.DefaultSignInScheme = "External";})                       // used in LoginController
    .AddCookie("External")
    .AddGoogle(o =>
        {
            o.ClientId = "1050361737246-ip8p0c4epk2o2mdvrcd07fvaniemotb3.apps.googleusercontent.com"; //livia.zane@gmail.com pe google console
            o.ClientSecret = "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";
        })
    .AddFacebook(o =>
    {
        o.AppId = "1210223106301489";                                                            // contul tatalui pe facebook developers
        o.ClientSecret = "b8cf986abb8ea46407bc052c65741f63";
    });

// Add services for logging message
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();