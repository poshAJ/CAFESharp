// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;

namespace CAFESharp.ViewModels.Weapon;

public sealed partial class WeaponViewModel (
    ILogger<WeaponViewModel> logger
) : UAssetViewModel(logger) {
    #region Properties

    public string IconPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["IconPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'Weapon.IconPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: IconPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["IconPath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Weapon.IconPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["IconName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Weapon.IconName'."
                    )
                );
            }
        );
    }

    public string BlueprintPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["BlueprintPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'Weapon.BlueprintPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: BlueprintPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["BlueprintPath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Weapon.BlueprintPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["BlueprintName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Weapon.BlueprintName'."
                    )
                );

                uasset.TrySetNameReferenceValue(
                    index: _map["BlueprintNameC"],
                    value: $"{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Weapon.BlueprintNameC'."
                    )
                );
            }
        );
    }

    public string AssetPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["AssetPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'Weapon.AssetPath' does not appear valid."
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
                        message: "An error occured while setting 'Weapon.AssetPath'."
                    )
                );

                uasset.SetFolderName(value: path);

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["AssetName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Weapon.AssetName'."
                    )
                );
            }
        );
    }

    protected override List<Definition> Definitions { get; } = [
        new Definition { Required = true, Category = "Icon", Type = "Path", Pattern = "/T_\\w+$" },
        new Definition { Required = true, Category = "Icon", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = true, Category = "Blueprint", Type = "Path", Pattern = "/BP_\\w+$" },
        new Definition { Required = true, Category = "Blueprint", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = true, Category = "Blueprint", Type = "NameC", Pattern = "^{0}_C$" },
        new Definition { Required = true, Category = "Asset", Type = "Path", Pattern = "^/Game/Forms/items/weapons/(?!BP_)" },
        new Definition { Required = true, Category = "Asset", Type = "Name", Pattern = "^{0}$" }
    ];

    #endregion Properties
}
