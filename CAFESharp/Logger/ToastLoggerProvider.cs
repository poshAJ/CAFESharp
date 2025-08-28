using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace CAFESharp.Logger;

public sealed class ToastLoggerProvider (
    IServiceProvider serviceProvider
) : ILoggerProvider {
    #region Fields

    private readonly ConcurrentDictionary<string, ToastLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    #endregion Fields

    #region ILoggerProvider Members

    public ILogger CreateLogger (string categoryName) => _loggers.GetOrAdd(categoryName, new ToastLogger(serviceProvider));

    public void Dispose () => _loggers.Clear();

    #endregion ILoggerProvider Members
}
