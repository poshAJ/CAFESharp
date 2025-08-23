using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CAFESharp.ViewModels;
using CAFESharp.Views;

namespace CAFESharp;

public partial class App : Application {
    #region Application Members

    public override void Initialize () {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted () {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            DisableAvaloniaDataAnnotationValidation();

            desktop.MainWindow = new MainWindow {
                DataContext = new MainViewModel(),
            };
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

        foreach (var plugin in dataValidationPluginsToRemove) {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    #endregion Private Methods
}
