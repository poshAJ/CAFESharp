using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class MaterialInstanceViewModel (
    ILogger<MaterialInstanceViewModel> logger
) : UAssetViewModel {
    #region Constants

    private readonly Dictionary<bool, Dictionary<string, int>> INDEX = new() {
        { true, new() {
            { "Diffuse_Path", 24 },
            { "Diffuse_Name", 6 },
            { "NNRM_Path", 26 },
            { "NNRM_Name", 7 },
            { "Form_Path", 11 },
            { "Form_Name", 10 }
        }},
        { false, new() {
            { "Diffuse_Path", 23 },
            { "Diffuse_Name", 4 },
            { "NNRM_Path", 25 },
            { "NNRM_Name", 5 },
            { "Form_Path", 10 },
            { "Form_Name", 9 }
        }}
    };

    #endregion Constants

    #region Properties

    // naive approach and hoping there aren't a bunch of different mappings
    public bool HasNNRE {
        get => _uasset.GetNameMapIndexList().Count > 26
            ? true
            : false;
    }
    public string DiffusePath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX[HasNNRE]["Diffuse_Path"],
            onError: () => logger.LogWarning(
                message: "The value retrieved for 'DiffusePath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: DiffusePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasNNRE]["Diffuse_Path"],
                    value: path,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'DiffusePath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasNNRE]["Diffuse_Name"],
                    value: name,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'DiffuseName'."
                    )
                );
            }
        );
    }
    public string NNRMPath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX[HasNNRE]["NNRM_Path"],
            onError: () => logger.LogWarning(
                message: "The value retrieved for 'NNRMPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: NNRMPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasNNRE]["NNRM_Path"],
                    value: path,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'NNRMPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasNNRE]["NNRM_Name"],
                    value: name,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'NNRMName'."
                    )
                );
            }
        );
    }
    public string FormPath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX[HasNNRE]["Form_Path"],
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
                    index: INDEX[HasNNRE]["Form_Path"],
                    value: path,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'FormPath'."
                    )
                );

                uasset.FolderName = (FString) path;

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX[HasNNRE]["Form_Name"],
                    value: name,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'FormName'."
                    )
                );
            }
        );
    }

    #endregion Properties
}
