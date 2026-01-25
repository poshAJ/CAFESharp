// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;

namespace CAFESharp.ViewModels.Weapon;

public sealed partial class BlueprintViewModel (
    ILogger<BlueprintViewModel> logger
) : UAssetViewModel(logger) {
    #region Properties

    public bool HasScabbard {
        get => _map.ContainsKey("ScabbardPath");
    }

    public bool IsLoadedHasScabbard {
        get => IsLoaded && HasScabbard;
    }

    public string MeshPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["MeshPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'Blueprint.MeshPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: MeshPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["MeshPath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.MeshPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["MeshName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.MeshName'."
                    )
                );
            }
        );
    }

    public string BloodSplatterPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["BloodSplatterPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'Blueprint.BloodSplatterPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: BloodSplatterPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["BloodSplatterPath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.BloodSplatterPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["BloodSplatterName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.BloodSplatterName'."
                    )
                );
            }
        );
    }

    public string ScabbardPath {
        get {
            if (!HasScabbard) {
                return string.Empty;
            }

            return _uasset.TryGetNameReferenceValue(
                index: _map["ScabbardPath"],
                onError: (_) => logger.LogWarning(
                    message: "The value retrieved for 'Blueprint.ScabbardPath' does not appear valid."
                )
            );
        }
        set => SetProperty(
            oldValue: ScabbardPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                if (!HasScabbard) {
                    return;
                }

                uasset.TrySetNameReferenceValue(
                    index: _map["ScabbardPath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.ScabbardPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["ScabbardName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.ScabbardName'."
                    )
                );
            }
        );
    }

    public string AssetPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["AssetPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'Blueprint.AssetPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: AssetPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["AssetPath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.AssetPath'."
                    )
                );

                uasset.SetFolderName(value: path);

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["AssetNameC"],
                    value: $"{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.AssetNameC'."
                    )
                );

                uasset.TrySetNameReferenceValue(
                    index: _map["AssetDefaultNameC"],
                    value: $"Default__{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.AssetDefaultNameC'."
                    )
                );
            }
        );
    }

    protected override List<Definition> Definitions { get; } = [
        new Definition { Required = true, Category = "Mesh", Type = "Path", Pattern = "/SM_\\w+(?<!_Scabbard)$" },
        new Definition { Required = true, Category = "Mesh", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = true, Category = "BloodSplatter", Type = "Path", Pattern = "/MIC_\\w+_WeapBloodSplatter$" },
        new Definition { Required = true, Category = "BloodSplatter", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = false, Category = "Scabbard", Type = "Path", Pattern = "/SM_\\w+_Scabbard$" },
        new Definition { Required = false, Category = "Scabbard", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = true, Category = "Asset", Type = "Path", Pattern = "/BP_\\w+(?<!Generic.*)$" },
        new Definition { Required = true, Category = "Asset", Type = "NameC", Pattern = "^{0}_C$" },
        new Definition { Required = true, Category = "Asset", Type = "DefaultNameC", Pattern = "^Default__{0}_C$" },
    ];

    #endregion Properties
}
