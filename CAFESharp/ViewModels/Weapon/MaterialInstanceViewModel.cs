// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;

namespace CAFESharp.ViewModels.Weapon;

public sealed partial class MaterialInstanceViewModel (
    ILogger<MaterialInstanceViewModel> logger
) : UAssetViewModel(logger) {
    #region Properties

    public string DiffusePath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["DiffusePath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'MaterialInstance.DiffusePath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: DiffusePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["DiffusePath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'MaterialInstance.DiffusePath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["DiffuseName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'MaterialInstance.DiffuseName'."
                    )
                );
            }
        );
    }

    public string NNRMPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["NNRMPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'MaterialInstance.NNRMPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: NNRMPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["NNRMPath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'MaterialInstance.NNRMPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["NNRMName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'MaterialInstance.NNRMName'."
                    )
                );
            }
        );
    }

    public string AssetPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["AssetPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'MaterialInstance.AssetPath' does not appear valid."
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
                        message: "An error occured while setting 'MaterialInstance.AssetPath'."
                    )
                );

                uasset.SetFolderName(value: path);

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["AssetName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'MaterialInstance.AssetName'."
                    )
                );
            }
        );
    }

    protected override List<Definition> Definitions { get; } = [
        new Definition { Required = true, Category = "Diffuse", Type = "Path", Pattern = "/T_\\w+_D$" },
        new Definition { Required = true, Category = "Diffuse", Type = "Name", Pattern = "{0}$" },
        new Definition { Required = true, Category = "NNRM", Type = "Path", Pattern = "/T_\\w+_NNRM$" },
        new Definition { Required = true, Category = "NNRM", Type = "Name", Pattern = "{0}$" },
        new Definition { Required = true, Category = "Asset", Type = "Path", Pattern = "/MIC_\\w+(?<!Base.*)$" },
        new Definition { Required = true, Category = "Asset", Type = "Name", Pattern = "{0}$" }
    ];

    #endregion Properties
}
