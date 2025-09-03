using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class BlueprintViewModel (
    ILogger<BlueprintViewModel> logger
) : UAssetViewModel {
    #region Constants

    private readonly Dictionary<bool, Dictionary<string, int>> INDEX = new() {
        { true, new() {
            { "Mesh_Path", 53 },
            { "Mesh_Name", 55 },
            { "BloodSplatter_Path", 0 },
            { "BloodSplatter_Name", 49 },
            { "Scabbard_Path", 56 },
            { "Scabbard_Name", 57 },
            { "Form_Path", 24 },
            { "Form_Name_C", 10 },
            { "Form_Default__Name_C", 11 }
        }},
        { false, new() {
            { "Mesh_Path", 52 },
            { "Mesh_Name", 54 },
            { "BloodSplatter_Path", 0 },
            { "BloodSplatter_Name", 48 },
            { "Form_Path", 23 },
            { "Form_Name_C", 10 },
            { "Form_Default__Name_C", 11 }
        }}
    };

    #endregion Constants

    #region Properties

    // naive approach and hoping there aren't a bunch of different mappings
    public bool HasScabbard {
        get => _uasset.GetNameMapIndexList().Count > 55
            ? true
            : false;
    }
    public bool IsLoadedHasScabbard {
        get => IsLoaded && HasScabbard;
    }
    public string MeshPath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX[HasScabbard]["Mesh_Path"],
            onError: () => logger.LogWarning(
                message: "The value retrieved for 'MeshPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: MeshPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasScabbard]["Mesh_Path"],
                    value: path,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'MeshPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasScabbard]["Mesh_Name"],
                    value: name,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'MeshName'."
                    )
                );
            }
        );
    }
    public string BloodSplatterPath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX[HasScabbard]["BloodSplatter_Path"],
            onError: () => logger.LogWarning(
                message: "The value retrieved for 'BloodSplatterPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: BloodSplatterPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasScabbard]["BloodSplatter_Path"],
                    value: path,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'BloodSplatterPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasScabbard]["BloodSplatter_Name"],
                    value: name,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'BloodSplatterName'."
                    )
                );
            }
        );
    }
    public string ScabbardPath {
        get => HasScabbard
            ? _uasset.TryGetNameReferenceValue(
                index: INDEX[HasScabbard]["Scabbard_Path"],
                onError: () => logger.LogWarning(
                    message: "The value retrieved for 'ScabbardPath' does not appear valid."
                )
            )
            : string.Empty;
        set => SetProperty(
            oldValue: ScabbardPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                if (!HasScabbard) return;

                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasScabbard]["Scabbard_Path"],
                    value: path,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'ScabbardPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasScabbard]["Scabbard_Name"],
                    value: name,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'ScabbardName'."
                    )
                );
            }
        );
    }
    public string FormPath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX[HasScabbard]["Form_Path"],
            onError: () => logger.LogWarning(
                message: "The value retrieved for 'FormPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: FormPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasScabbard]["Form_Path"],
                    value: path,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'FormPath'."
                    )
                );

                uasset.FolderName = (FString) path;

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasScabbard]["Form_Name_C"],
                    value: $"{name}_C",
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'FormName'."
                    )
                );
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasScabbard]["Form_Default__Name_C"],
                    value: $"DEFAULT__{name}_C",
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'FormName'."
                    )
                );
            }
        );
    }

    #endregion Properties
}
