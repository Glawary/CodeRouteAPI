using CodeRoute.Repositories;
using CodeRoute.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

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
            builder.Services.AddScoped<UserService>();


            builder.Services.AddScoped<RouteRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<VertexRepository>();


            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();


            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });


            //Перенаправляет HTTP на HTTPS
            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}