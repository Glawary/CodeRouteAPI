namespace CodeRoute.Logs
{
    public class FileLogger : ILogger, IDisposable
    {
        string filePath;
        static object _lock = new object();

        public FileLogger(string path)
        {
            filePath = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose() { }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(
            LogLevel logLevel, EventId eventId,
            TState state, Exception? exception, 
            Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                string log = logLevel.ToString() + ": " + formatter(state, exception) + Environment.NewLine;
                if (logLevel == LogLevel.Error)
                {

                }
                //Console.WriteLine(log);
                File.AppendAllText(filePath, log);
            }
        }
    }
}
