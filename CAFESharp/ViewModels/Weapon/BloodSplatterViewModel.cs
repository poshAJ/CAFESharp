// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;

namespace CAFESharp.ViewModels.Weapon;

public sealed partial class BloodSplatterViewModel (
    ILogger<BloodSplatterViewModel> logger
) : UAssetViewModel(logger) {
    #region Properties

    public string MaterialInstancePath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["MaterialInstancePath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'BloodSplatter.MaterialInstancePath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: MaterialInstancePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["MaterialInstancePath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BloodSplatter.MaterialInstancePath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["MaterialInstanceName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BloodSplatter.MaterialInstanceName'."
                    )
                );
            }
        );
    }

    public string AssetPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["AssetPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'BloodSplatter.AssetPath' does not appear valid."
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
                        message: "An error occured while setting 'BloodSplatter.AssetPath'."
                    )
                );

                uasset.SetFolderName(value: path);

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["AssetName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BloodSplatter.AssetName'."
                    )
                );
            }
        );
    }

    protected override List<Definition> Definitions { get; } = [
        new Definition { Required = true, Category = "MaterialInstance", Type = "Path", Pattern = "/MIC_\\w+(?<!_WeapBloodSplatter)$" },
        new Definition { Required = true, Category = "MaterialInstance", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = true, Category = "Asset", Type = "Path", Pattern = "/MIC_\\w+_WeapBloodSplatter$" },
        new Definition { Required = true, Category = "Asset", Type = "Name", Pattern = "^{0}$" }
    ];

    #endregion Properties
}
