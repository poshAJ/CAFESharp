using System.IO;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class WeaponViewModel : UAssetViewModel {
    #region Constants

    private const int INDEX_ICON_PATH = 0;
    private const int INDEX_ICON_NAME = 4;
    private const int INDEX_BLUEPRINT_PATH = 1;
    private const int INDEX_BLUEPRINT_NAME = 2;
    private const int INDEX_BLUEPRINT_NAME_C = 3;
    private const int INDEX_FORM_PATH = 7;
    private const int INDEX_FORM_NAME = 5;

    #endregion Constants

    #region Properties

    public string IconPath {
        get => _uasset.GetNameReference(INDEX_ICON_PATH).Value;
        set => SetProperty(
            oldValue: IconPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX_ICON_PATH, (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX_ICON_NAME, (FString) name);
            }
        );
    }
    public string BlueprintPath {
        get => _uasset.GetNameReference(INDEX_BLUEPRINT_PATH).Value;
        set => SetProperty(
            oldValue: BlueprintPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX_BLUEPRINT_PATH, (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX_BLUEPRINT_NAME, (FString) name);
                _uasset.SetNameReference(INDEX_BLUEPRINT_NAME_C, (FString) $"{name}_C");
            }
        );
    }
    public string FormPath {
        get => _uasset.GetNameReference(INDEX_FORM_PATH).Value;
        set => SetProperty(
            oldValue: FormPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX_FORM_PATH, (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX_FORM_NAME, (FString) name);
            }
        );
    }

    #endregion Properties
}
