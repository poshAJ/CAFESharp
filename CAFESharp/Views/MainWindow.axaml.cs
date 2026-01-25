// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System;
using Avalonia.Interactivity;
using CAFESharp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Ursa.Controls;

namespace CAFESharp.Views;

public sealed partial class MainWindow : UrsaWindow {
    #region Events

    private async void OpenGitHub (object? sender, RoutedEventArgs args) => await Launcher.LaunchUriAsync(
        uri: new Uri("https://github.com/poshAJ/CAFESharp")
    );

    private async void OpenNexus (object? sender, RoutedEventArgs args) => await Launcher.LaunchUriAsync(
        uri: new Uri("https://www.nexusmods.com/oblivionremastered/mods/4891")
    );

    #endregion Events

    #region Properties

    public WindowToastManager ToastManager { get; set; }

    #endregion Properties

    #region Constructors

    public MainWindow () {
        InitializeComponent();

        DataContext = App.Services.GetRequiredService<MainViewModel>();

        ToastManager = new(host: this);
    }

    #endregion Constructors
}
