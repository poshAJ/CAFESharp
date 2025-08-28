using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CommunityToolkit.Mvvm.Input;
using UAssetAPI;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class UAssetViewModel : BaseViewModel {
    #region Fields

    internal UAsset _uasset = new();

    #endregion Fields

    #region Properties

    public string StartPath { get; } = Environment
        .GetFolderPath(folder: Environment.SpecialFolder.UserProfile);
    public string FilePath {
        get => _uasset.FilePath;
        set => SetProperty(
            oldValue: FilePath,
            newValue: value,
            model: _uasset,
            callback: (_, path) => {
                _uasset = new(
                    path: path,
                    engineVersion: EngineVersion.VER_UE5_3
                );

                RefreshAllProperties();
            }
        );
    }
    public string FileName {
        get => Path.GetFileName(path: FilePath);
    }
    public bool IsLoaded {
        get => !string.IsNullOrEmpty(FilePath);
    }

    #endregion Properties

    #region Private Methods

    private void RefreshAllProperties () {
        PropertyInfo[] properties = GetType()
            .GetProperties(bindingAttr: BindingFlags.Instance | BindingFlags.Public);

        foreach (var property in properties)
            OnPropertyChanged(propertyName: property.Name);
    }

    #endregion Private Methods

    #region Handlers

    [RelayCommand]
    internal void OpenUAsset (IReadOnlyList<string> paths) => FilePath = paths[0];

    #endregion Handlers
}
