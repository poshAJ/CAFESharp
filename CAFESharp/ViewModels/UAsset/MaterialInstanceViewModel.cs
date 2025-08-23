using System.IO;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class MaterialInstanceViewModel : UAssetViewModel {
    #region Constants

    private const int INDEX_DIFFUSE_PATH = 24;
    private const int INDEX_DIFFUSE_NAME = 6;
    private const int INDEX_NNRM_PATH = 26;
    private const int INDEX_NNRM_NAME = 7;
    private const int INDEX_FORM_PATH = 11;
    private const int INDEX_FORM_NAME = 10;

    #endregion Constants

    #region Properties

    public string DiffusePath {
        get => _uasset.GetNameReference(INDEX_DIFFUSE_PATH).Value;
        set => SetProperty(
            oldValue: DiffusePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX_DIFFUSE_PATH, (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX_DIFFUSE_NAME, (FString) name);
            }
        );
    }
    public string NNRMPath {
        get => _uasset.GetNameReference(INDEX_NNRM_PATH).Value;
        set => SetProperty(
            oldValue: NNRMPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX_NNRM_PATH, (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX_NNRM_NAME, (FString) name);
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
