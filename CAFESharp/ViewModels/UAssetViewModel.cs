// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using UAssetAPI;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public abstract partial class UAssetViewModel (
    ILogger<UAssetViewModel> logger
) : ObservableObject {
    #region Nested

    protected class Definition {
        public required bool Required { get; set; }
        public required string Category { get; set; }
        public required string Type { get; set; }
        public required string Pattern { get; set; }
    }

    #endregion Nested

    #region Events

    [RelayCommand]
    public void Reset () {
        _uasset = new();
        _map.Clear();

        RefreshAllProperties();
    }

    #endregion Events

    #region Fields

    protected UAsset _uasset = new();
    protected Dictionary<string, int> _map = [];

    #endregion Fields

    #region Properties

    public string StartPath {
        get => Path.GetDirectoryName(FilePath) ?? Environment.GetFolderPath(
            folder: Environment.SpecialFolder.UserProfile
        );
    }

    public string FilePath {
        get => _uasset.FilePath;
        set => SetProperty(
            oldValue: FilePath,
            newValue: value,
            model: _uasset,
            callback: (_, path) => {
                if (IsLoaded) {
                    _uasset.Write(path);

                    _uasset.FilePath = path;

                    OnPropertyChanged(propertyName: nameof(FilePath));
                    OnPropertyChanged(propertyName: nameof(StartPath));
                    OnPropertyChanged(propertyName: nameof(FileName));
                } else {
                    _uasset = new(
                        path: path,
                        engineVersion: EngineVersion.VER_UE5_3
                    );
                    _map.Clear();

                    MapNameReferences();
                    RefreshAllProperties();
                }
            }
        );
    }

    public string FileName {
        get => Path.GetFileName(path: FilePath);
    }

    public bool IsLoaded {
        get => !string.IsNullOrEmpty(value: FilePath);
    }

    protected abstract List<Definition> Definitions { get; }

    #endregion Properties

    #region Methods

    public void Save () => _uasset.Write(FilePath);

    private static Regex BuildRegex (string pattern) {
        return new Regex(
            pattern,
            RegexOptions.IgnoreCase | RegexOptions.Compiled
        );
    }

    private void MapNameReferences () {
        List<string> list = _uasset
            .GetNameMapIndexList()
            .Select(x => x.Value)
            .ToList();

        foreach (var definition in Definitions) {
            string keyName = $"{definition.Category}{definition.Type}";
            Regex? regex = null;

            if (_map.TryGetValue($"{definition.Category}Path", out int value)) {
                string name = Path.GetFileNameWithoutExtension(path: list[value]);

                regex = BuildRegex(string.Format(definition.Pattern, Regex.Escape(name)));
            }

            regex ??= BuildRegex(definition.Pattern);

            int index = list.FindIndex(regex.IsMatch);

            if (index == -1 && definition.Required != true) {
                continue;
            }

            if (index == -1) {
                logger.LogError(
                    message: "An error occured while mapping '{keyName}'.",
                    args: keyName
                );

                Reset();

                return;
            }

            _map[keyName] = index;
        }
    }

    private void RefreshAllProperties () {
        PropertyInfo[] properties = GetType()
            .GetProperties(bindingAttr: BindingFlags.Instance | BindingFlags.Public);

        foreach (var property in properties) {
            OnPropertyChanged(propertyName: property.Name);
        }
    }

    #endregion Methods
}
