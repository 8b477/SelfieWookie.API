using System.Collections.Concurrent;

using Microsoft.Extensions.Logging;

namespace SelfieWookie.Core.Infrastructure.Logger
{
    public class LoggerCustomProvider : ILoggerProvider
    {
        private ConcurrentDictionary<string, LoggerCustomMessage> _loggerList = new ConcurrentDictionary<string, LoggerCustomMessage>();

        public ILogger CreateLogger(string categoryName)
        {
            _loggerList.GetOrAdd(categoryName, key => new LoggerCustomMessage());

            return _loggerList[categoryName];
        }

        public void Dispose()
        {
            _loggerList.Clear();
        }
    }
}
