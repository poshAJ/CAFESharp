using System.Collections.Generic;
using System.IO;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;

namespace CAFESharp.ViewModels;

public partial class WeaponViewModel (
    ILogger<WeaponViewModel> logger
) : UAssetViewModel {
    #region Constants

    private readonly Dictionary<string, int> INDEX = new() {
        { "Icon_Path", 0 },
        { "Icon_Name", 4 },
        { "Blueprint_Path", 1 },
        { "Blueprint_Name", 2 },
        { "Blueprint_Name_C", 3 },
        { "Form_Path", 7 },
        { "Form_Name", 5 }
    };

    #endregion Constants

    #region Properties

    public string IconPath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX["Icon_Path"],
            onError: () => logger.LogWarning(message: "The value retrieved for 'IconPath' does not appear valid.")
        );
        set => SetProperty(
            oldValue: IconPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: INDEX["Icon_Path"],
                    value: path,
                    onError: () => logger.LogError(message: "An error occured while setting 'IconPath'.")
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX["Icon_Name"],
                    value: name,
                    onError: () => logger.LogError(message: "An error occured while setting 'IconName'.")
                );
            }
        );
    }
    public string BlueprintPath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX["Blueprint_Path"],
            onError: () => logger.LogWarning(message: "The value retrieved for 'BlueprintPath' does not appear valid.")
        );
        set => SetProperty(
            oldValue: BlueprintPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: INDEX["Blueprint_Path"],
                    value: path,
                    onError: () => logger.LogError(message: "An error occured while setting 'BlueprintPath'.")
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX["Blueprint_Name"],
                    value: name,
                    onError: () => logger.LogError(message: "An error occured while setting 'BlueprintName'.")
                );
                uasset.TrySetNameReferenceValue(
                    index: INDEX["Blueprint_Name_C"],
                    value: $"{name}_C",
                    onError: () => logger.LogError(message: "An error occured while setting 'BlueprintName'.")
                );
            }
        );
    }
    public string FormPath {
        get => _uasset.TryGetNameReferenceValue(
            index: INDEX["Form_Path"],
            onError: () => logger.LogWarning(message: "The value retrieved for 'FormPath' does not appear valid.")
        );
        set => SetProperty(
            oldValue: FormPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: INDEX["Form_Path"],
                    value: path,
                    onError: () => logger.LogError(message: "An error occured while setting 'FormPath'.")
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: INDEX["Form_Name"],
                    value: name,
                    onError: () => logger.LogError(message: "An error occured while setting 'FormName'.")
                );
            }
        );
    }

    #endregion Properties
}
