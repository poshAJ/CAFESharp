// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CAFESharp.Extensions;
using CAFESharp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CAFESharp;

public sealed partial class App : Application {
    #region Properties

    public static IServiceProvider Services { get; private set; } = null!;

    #endregion Properties

    #region Application Members

    public override void Initialize () {
        AvaloniaXamlLoader.Load(obj: this);
    }

    public override void OnFrameworkInitializationCompleted () {
        Services = new ServiceCollection()
            .AddLogging(builder => builder.AddToastLogging())
            .AddViewModels()
            .AddSingleton<MainWindow>()
            .BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            DisableAvaloniaDataAnnotationValidation();

            MainWindow mainWindow = Services.GetRequiredService<MainWindow>();

            desktop.MainWindow = mainWindow;
        } else {
            throw new NotSupportedException();
        }

        base.OnFrameworkInitializationCompleted();
    }

    #endregion Application Members

    #region Methods

    private void DisableAvaloniaDataAnnotationValidation () {
        DataAnnotationsValidationPlugin[] dataValidationPluginsToRemove = BindingPlugins
                .DataValidators
                .OfType<DataAnnotationsValidationPlugin>()
                .ToArray();

        foreach (var plugin in dataValidationPluginsToRemove) {
            BindingPlugins.DataValidators.Remove(item: plugin);
        }
    }

    #endregion Methods
}
