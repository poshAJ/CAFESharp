using System.IO;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class BloodSplatterViewModel : UAssetViewModel {
    #region Constants

    private const int INDEX_MATERIALINSTANCE_PATH = 8;
    private const int INDEX_MATERIALINSTANCE_NAME = 9;
    private const int INDEX_FORM_PATH = 2;
    private const int INDEX_FORM_NAME = 1;

    #endregion Constants

    #region Properties

    public string MaterialInstancePath {
        get => _uasset.GetNameReference(INDEX_MATERIALINSTANCE_PATH).Value;
        set => SetProperty(
            oldValue: MaterialInstancePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX_MATERIALINSTANCE_PATH, (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX_MATERIALINSTANCE_NAME, (FString) name);
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
