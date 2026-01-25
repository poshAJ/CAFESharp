// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;

namespace CAFESharp.ViewModels.Armor;

public sealed partial class ArmorViewModel (
    ILogger<ArmorViewModel> logger
) : UAssetViewModel(logger) {
    #region Properties

    public bool HasBlueprint {
        get => _map.ContainsKey("BlueprintPath");
    }

    public bool IsLoadedHasBlueprint {
        get => IsLoaded && HasBlueprint;
    }

    public string MeshPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["MeshPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'Armor.MeshPath' does not appear valid."
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
                        message: "An error occured while setting 'Armor.MeshPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["MeshName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Armor.MeshName'."
                    )
                );
            }
        );
    }

    public string BlueprintPath {
        get {
            if (!HasBlueprint) {
                return string.Empty;
            }

            return _uasset.TryGetNameReferenceValue(
                index: _map["BlueprintPath"],
                onError: (_) => logger.LogWarning(
                    message: "The value retrieved for 'Armor.BlueprintPath' does not appear valid."
                )
            );
        }
        set => SetProperty(
            oldValue: BlueprintPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                if (!HasBlueprint) {
                    return;
                }

                uasset.TrySetNameReferenceValue(
                    index: _map["BlueprintPath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Armor.BlueprintPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["BlueprintName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Armor.BlueprintName'."
                    )
                );

                uasset.TrySetNameReferenceValue(
                    index: _map["BlueprintNameC"],
                    value: $"{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Armor.BlueprintNameC'."
                    )
                );
            }
        );
    }

    public string BodyPartPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["BodyPartPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'Armor.BodyPartPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: BodyPartPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["BodyPartPath"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Armor.BodyPartPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["BodyPartNameC"],
                    value: $"{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Armor.BodyPartNameC'."
                    )
                );

                uasset.TrySetNameReferenceValue(
                    index: _map["BodyPartDefaultNameC"],
                    value: $"Default__{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Armor.BodyPartDefaultNameC'."
                    )
                );
            }
        );
    }

    public string AssetPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["AssetPath"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'Armor.AssetPath' does not appear valid."
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
                        message: "An error occured while setting 'Armor.AssetPath'."
                    )
                );

                uasset.SetFolderName(value: path);

                string name = Path.GetFileNameWithoutExtension(path: path);

                uasset.TrySetNameReferenceValue(
                    index: _map["AssetName"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'Armor.AssetName'."
                    )
                );
            }
        );
    }

    protected override List<Definition> Definitions { get; } = [
        new Definition { Required = true, Category = "Mesh", Type = "Path", Pattern = "/SM_\\w+_gnd$" },
        new Definition { Required = true, Category = "Mesh", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = false, Category = "Blueprint", Type = "Path", Pattern = "/BP_(?!BDP_)\\w+$" },
        new Definition { Required = false, Category = "Blueprint", Type = "Name", Pattern = "^{0}$" },
        new Definition { Required = false, Category = "Blueprint", Type = "NameC", Pattern = "^{0}_C$" },
        new Definition { Required = true, Category = "BodyPart", Type = "Path", Pattern = "/BP_BDP_\\w+$" },
        new Definition { Required = true, Category = "BodyPart", Type = "NameC", Pattern = "^{0}_C$" },
        new Definition { Required = true, Category = "BodyPart", Type = "DefaultNameC", Pattern = "^Default__{0}_C$" },
        new Definition { Required = true, Category = "Asset", Type = "Path", Pattern = "^/Game/Forms/items/armor/(?!BP_)" },
        new Definition { Required = true, Category = "Asset", Type = "Name", Pattern = "^{0}$" }
    ];

    #endregion Properties
}
