using Microsoft.EntityFrameworkCore;
using Project_Appointments.Contexts;
using Project_Appointments.Models.Services;

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
        builder.Services.AddTransient<OdontologistService>();
        builder.Services.AddTransient<ScheduleService>();
        builder.Services.AddTransient<BreakTimeService>();
        builder.Services.AddTransient<AppointmentService>();

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