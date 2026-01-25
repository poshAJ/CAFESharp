// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System.Linq;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CAFESharp.ViewModels;

public sealed partial class MainViewModel (
    ILogger<MainViewModel> logger
) : ObservableObject {
    #region Events

    [RelayCommand]
    public void SaveAll () {
        UAssetViewModel[] models = App.Services.GetServices<UAssetViewModel>().ToArray();

        foreach (var model in models) {
            if (string.IsNullOrEmpty(model.FilePath)) {
                continue;
            }

            try {
                model.Save();

                logger.LogInformation(
                    message: "Saved '{FileName}'.", model.FileName
                );
            } catch {
                logger.LogError(
                    message: "An error occured while saving '{FileName}'.", model.FileName
                );
            }
        }
    }

    #endregion Events

    #region Properties

    public string? Version { get; } = Assembly
        .GetExecutingAssembly()
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
        .InformationalVersion;

    #endregion Properties
}
