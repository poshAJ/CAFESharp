// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;

namespace CAFESharp.ViewModels.Armor;

public sealed partial class BodyPartViewModel (
    ILogger<BodyPartViewModel> logger
) : UAssetViewModel(logger) {
    #region Properties

    public string MalePath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["MalePath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'BodyPart.MalePath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: MalePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["MalePath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BodyPart.MalePath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["MaleName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BodyPart.MaleName'."
                    )
                );
            }
        );
    }

    public string FemalePath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["FemalePath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'BodyPart.FemalePath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: FemalePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["FemalePath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BodyPart.FemalePath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["FemaleName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BodyPart.FemaleName'."
                    )
                );
            }
        );
    }

    public string AssetPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["AssetPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'BodyPart.AssetPath' does not appear valid."
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
                        message: "An error occured while setting 'BodyPart.AssetPath'."
                    )
                );

                uasset.SetFolderName(value: path);

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["AssetNameC"],
                    value: $"{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BodyPart.AssetNameC'."
                    )
                );

                uasset.TrySetNameReferenceValue(
                    index: _map["AssetDefaultNameC"],
                    value: $"Default__{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BodyPart.AssetDefaultNameC'."
                    )
                );
            }
        );
    }

    protected override List<Definition> Definitions { get; } = [
        new Definition { Required = true, Category = "Male", Type = "Path", Pattern = "/SK_\\w+_m$" },
        new Definition { Required = true, Category = "Male", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = true, Category = "Female", Type = "Path", Pattern = "/SK_\\w+_f$" },
        new Definition { Required = true, Category = "Female", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = true, Category = "Asset", Type = "Path", Pattern = "/BP_BDP_\\w+$" },
        new Definition { Required = true, Category = "Asset", Type = "NameC", Pattern = "^{0}_C$" },
        new Definition { Required = true, Category = "Asset", Type = "DefaultNameC", Pattern = "^Default__{0}_C$" }
    ];

    #endregion Properties
}
