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

public partial class App : Application {
    #region Application Members

    public override void Initialize () {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted () {
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddLogging(builder => builder.AddToastLogging())
            .AddViewModels()
            .AddSingleton<MainWindow>()
            .BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            DisableAvaloniaDataAnnotationValidation();

            MainWindow mainWindow = serviceProvider.GetRequiredService<MainWindow>();

            desktop.MainWindow = mainWindow;
        } else {
            throw new NotSupportedException();
        }

        base.OnFrameworkInitializationCompleted();
    }

    #endregion Application Members

    #region Private Methods

    private void DisableAvaloniaDataAnnotationValidation () {
        DataAnnotationsValidationPlugin[] dataValidationPluginsToRemove = [
            .. BindingPlugins
                .DataValidators
                .OfType<DataAnnotationsValidationPlugin>()
        ];

        foreach (var plugin in dataValidationPluginsToRemove)
            BindingPlugins.DataValidators.Remove(plugin);
    }

    #endregion Private Methods
}
