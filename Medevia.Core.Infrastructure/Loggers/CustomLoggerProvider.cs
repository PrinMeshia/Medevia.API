using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medevia.Core.Infrastructure.Loggers
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        #region Fields
        private ConcurrentDictionary<string,CustomMessageLogger> _loggers = new();
        #endregion
        #region Public methods
        public ILogger CreateLogger(string categoryName)
        {
            _loggers.GetOrAdd(categoryName, ket => new CustomMessageLogger());
            return _loggers[categoryName];
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
        #endregion

    }
}
