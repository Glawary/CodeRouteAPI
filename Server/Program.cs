using CodeRoute.Repositories;
using CodeRoute.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.OpenApi.Models;
using Prometheus;

[assembly: ApiController]

namespace CodeRoute
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

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

            //Метрики прометеуса
            app.UseMetricServer();
            app.UseHttpMetrics();


            //Настройа сваггера
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/v1/swagger.json", "v1");
                c.RoutePrefix = "api/swagger";
            });


            //Перенаправляет HTTP на HTTPS
            app.UseHttpsRedirection();

            app.UseRouting();

            app.MapControllers();
            app.Run();
        }
    }
}