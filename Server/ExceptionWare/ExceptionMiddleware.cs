using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace CodeRoute.ExceptionWare
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var path = context.Request.Path;

                WriteError(path, ex.Message);

                context.Response.StatusCode = 500;

                await Results.Problem(
                    title: "я где-то проебался, сорри",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, object>
                    {
                        { "traceId", Activity.Current?.Id }
                    }).ExecuteAsync(context);
            }
        }

        private async void WriteError(string path, string exceptionMessage)
        {
            await Console.Out.WriteLineAsync("Пиздец. Я обосрался!");
            await Console.Out.WriteAsync(DateTime.Now.ToString() + "  |  ");
            await Console.Out.WriteLineAsync("Произошла ошибка по пути: " + path);
            await Console.Out.WriteLineAsync(exceptionMessage);
        }
    }
}
