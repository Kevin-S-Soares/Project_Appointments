using Microsoft.EntityFrameworkCore;
using Project_Appointments.Contexts;
using Project_Appointments.Services.AppointmentService;
using Project_Appointments.Services.BreakTimeService;
using Project_Appointments.Services.OdontologistService;
using Project_Appointments.Services.ScheduleService;

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
        builder.Services.AddScoped<BreakTimeService>();
        builder.Services.AddScoped<AppointmentService>();

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