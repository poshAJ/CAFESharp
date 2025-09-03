using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class BloodSplatterViewModel (
    ILogger<BloodSplatterViewModel> logger
) : UAssetViewModel {
    #region Constants

    private readonly Dictionary<string, int> INDEX = new() {
        { "MaterialInstance_Path", 8 },
        { "MaterialInstance_Name", 9 },
        { "Form_Path", 2 },
        { "Form_Name", 1 }
    };

    #endregion Constants

    #region Properties

    public string MaterialInstancePath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX["MaterialInstance_Path"],
            onError: () => logger.LogWarning(
                message: "The value retrieved for 'MaterialInstancePath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: MaterialInstancePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: INDEX["MaterialInstance_Path"],
                    value: path,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'MaterialInstancePath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX["MaterialInstance_Name"],
                    value: name,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'MaterialInstanceName'."
                    )
                );
            }
        );
    }
    public string FormPath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX["Form_Path"],
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
                    index: INDEX["Form_Path"],
                    value: path,
                    onError: () => logger.LogError(
                        message: "An error occured while setting 'FormPath'."
                    )
                );

                uasset.FolderName = (FString) path;

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX["Form_Name"],
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
