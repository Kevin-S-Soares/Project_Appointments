using Microsoft.EntityFrameworkCore;
using Project_Appointments.Contexts;
using Project_Appointments.Services.AppointmentService;
using Project_Appointments.Services.BreakTimeService;
using Project_Appointments.Services.EmailService;
using Project_Appointments.Services.OdontologistService;
using Project_Appointments.Services.ScheduleService;
using Project_Appointments.Services.UserService;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApplicationContext>(x => x.UseSqlServer());
        builder.Services.AddScoped<IOdontologistService, OdontologistService>();
        builder.Services.AddScoped<IScheduleService, ScheduleService>();
        builder.Services.AddScoped<IBreakTimeService, BreakTimeService>();
        builder.Services.AddScoped<IAppointmentService, AppointmentService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IEmailService, EmailService>();

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
    }
}