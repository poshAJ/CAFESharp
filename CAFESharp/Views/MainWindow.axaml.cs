using System;
using Avalonia.Interactivity;
using CAFESharp.ViewModels;
using Ursa.Controls;

namespace CAFESharp.Views;

public partial class MainWindow : UrsaWindow {
    #region Properties

    public WindowToastManager ToastManager { get; set; }

    #endregion Properties

    #region Constructors

    public MainWindow (MainViewModel mainViewModel) {
        InitializeComponent();

        DataContext = mainViewModel;

        ToastManager = new(this);
    }

    #endregion Constructors

    private async void OpenGitHub (object? sender, RoutedEventArgs e) =>
        await Launcher.LaunchUriAsync(
            new Uri("https://github.com/poshAJ/CAFESharp")
        );
}
