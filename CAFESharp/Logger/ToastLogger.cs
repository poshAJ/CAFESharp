using System;
using Avalonia.Controls.Notifications;
using CAFESharp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ursa.Controls;

namespace CAFESharp.Logger;

public sealed class ToastLogger (
    IServiceProvider serviceProvider
) : ILogger {
    #region ILogger Members

    public IDisposable? BeginScope<TState> (TState state) where TState : notnull => default!;

    // should have an actual implementation but lazy
    public bool IsEnabled (LogLevel logLevel) => true;

    public void Log<TState> (
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    ) {
        WindowToastManager toastManager = serviceProvider.GetRequiredService<MainWindow>().ToastManager;

        Toast toast = new($"{formatter(state, exception)}");

        switch (logLevel) {
            case LogLevel.Trace:
                return;
            case LogLevel.Debug:
                return;
            case LogLevel.Information:
                toastManager.Show(
                    content: toast,
                    showIcon: false,
                    showClose: false,
                    type: NotificationType.Success,
                    classes: ["Light"]
                );
                return;
            case LogLevel.Warning:
                toastManager.Show(
                    content: toast,
                    showIcon: false,
                    showClose: false,
                    type: NotificationType.Warning,
                    classes: ["Light"]
                );
                return;
            case LogLevel.Error:
                toastManager.Show(
                    content: toast,
                    showIcon: false,
                    showClose: false,
                    type: NotificationType.Error,
                    classes: ["Light"]
                );
                return;
            case LogLevel.Critical:
                return;
            default:
                return;
        }
    }

    #endregion ILogger Members
}
