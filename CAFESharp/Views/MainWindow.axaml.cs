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

        ToastManager = new(host: this);
    }

    #endregion Constructors

    #region Handlers

    private async void OpenGitHub (object? sender, RoutedEventArgs args) => await Launcher.LaunchUriAsync(
        uri: new Uri("https://github.com/poshAJ/CAFESharp")
    );

    private async void OpenNexus (object? sender, RoutedEventArgs args) => await Launcher.LaunchUriAsync(
        uri: new Uri("https://www.nexusmods.com/oblivionremastered/mods/4891")
    );

    #endregion Handlers
}
