// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using CAFESharp.Logger;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace CAFESharp.Extensions;

public static class ILoggingBuilderExtensions {
    #region Methods

    public static ILoggingBuilder AddToastLogging (
        this ILoggingBuilder builder
    ) {
        builder.ClearProviders();

        builder.Services.TryAddSingleton<ILoggerProvider, ToastLoggerProvider>();

        return builder;
    }

    #endregion Methods
}
