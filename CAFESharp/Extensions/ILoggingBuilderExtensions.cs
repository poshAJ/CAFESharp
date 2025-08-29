using CAFESharp.Logger;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace CAFESharp.Extensions;

public static class ILoggingBuilderExtensions {
    public static ILoggingBuilder AddToastLogging (
        this ILoggingBuilder builder
    ) {
        builder.ClearProviders();

        builder.Services.TryAddSingleton<ILoggerProvider, ToastLoggerProvider>();

        return builder;
    }
}
