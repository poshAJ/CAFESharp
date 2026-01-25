// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;

namespace CAFESharp.ViewModels.Armor;

public sealed partial class BlueprintViewModel (
    ILogger<BlueprintViewModel> logger
) : UAssetViewModel(logger) {
    #region Properties

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

    public string FormPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["FormPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'Blueprint.FormPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: FormPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["FormPath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.FormPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["FormName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Blueprint.FormName'."
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
        new Definition { Required = true, Category = "Mesh", Type = "Path", Pattern = "/SM_\\w+_gnd$" },
        new Definition { Required = true, Category = "Mesh", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = true, Category = "Form", Type = "Path", Pattern = "^/Game/Forms/items/armor/(?!BP_)" },
        new Definition { Required = true, Category = "Form", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = true, Category = "Asset", Type = "Path", Pattern = "/BP_\\w+$" },
        new Definition { Required = true, Category = "Asset", Type = "NameC", Pattern = "^{0}_C$" },
        new Definition { Required = true, Category = "Asset", Type = "DefaultNameC", Pattern = "^DEFAULT__{0}_C$" }
    ];

    #endregion Properties
}
