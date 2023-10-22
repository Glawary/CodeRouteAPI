using CodeRoute.Repositories;
using CodeRoute.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[assembly: ApiController]

namespace CodeRoute
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            //Изъятие строки подключения из конфигурации
            var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseNpgsql(connectionStrings);
            });


            builder.Services.AddScoped<RouteService>();
            builder.Services.AddScoped<RouteRepository>();


            var app = builder.Build();

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });


            //Перенаправляет HTTP на HTTPS
            app.UseHttpsRedirection();
            app.MapControllers();

            app.UseCors();

            app.Run();
        }
    }
}