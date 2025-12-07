using Microsoft.Extensions.Logging;

namespace FreePLM.Common.Logging
{
    public class SerilogLoggerWrapper : ILogger
    {
        private readonly Serilog.ILogger _serilogLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogLoggerWrapper"/> class.
        /// </summary>
        /// <param name="logger">An instance of <see cref="Serilog.ILogger"/> to wrap.</param>
        public SerilogLoggerWrapper(Serilog.ILogger logger)
        {
            _serilogLogger = logger;
        }

        /// <summary>
        /// Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="state">The state associated with the operation scope.</param>
        /// <returns>A disposable object that ends the scope when disposed.</returns>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        /// <summary>
        /// Determines whether the specified log level is enabled.
        /// </summary>
        /// <param name="logLevel">The log level to check.</param>
        /// <returns>True if the log level is enabled; otherwise, false.</returns>
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <summary>
        /// Writes a log entry with the specified log level, event ID, state, and exception.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="logLevel">The log level of the entry.</param>
        /// <param name="eventId">The event ID associated with the log entry.</param>
        /// <param name="state">The state to log.</param>
        /// <param name="exception">An exception associated with the log entry, if any.</param>
        /// <param name="formatter">A function to format the log entry.</param>
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Information:
                    _serilogLogger.Information(exception, formatter(state, exception));
                    break;
                case LogLevel.Warning:
                    _serilogLogger.Warning(exception, formatter(state, exception));
                    break;
                case LogLevel.Error:
                    _serilogLogger.Error(exception, formatter(state, exception));
                    break;
                case LogLevel.Critical:
                    _serilogLogger.Fatal(exception, formatter(state, exception));
                    break;
                case LogLevel.Debug:
                    _serilogLogger.Debug(exception, formatter(state, exception));
                    break;
                case LogLevel.Trace:
                    _serilogLogger.Verbose(exception, formatter(state, exception));
                    break;
            }
        }
    }
}