using static System.Net.Mime.MediaTypeNames;

namespace CodeRoute
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
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                var path = context.Request.Path;

                WriteError(path, ex.Message);

                context.Response.StatusCode = 500;
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
