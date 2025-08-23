using System.IO;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class BlueprintViewModel : UAssetViewModel {
    #region Constants

    private const int INDEX_MESH_PATH = 53;
    private const int INDEX_MESH_NAME = 55;
    private const int INDEX_BLOODSPLATTER_PATH = 0;
    private const int INDEX_BLOODSPLATTER_NAME = 49;
    private const int INDEX_SCABBARD_PATH = 56;
    private const int INDEX_SCABBARD_NAME = 57;
    private const int INDEX_FORM_PATH = 24;
    private const int INDEX_FORM_NAME_C = 10;
    private const int INDEX_FORM_DEFAULT__NAME_C = 11;

    #endregion Constants

    #region Properties

    public string MeshPath {
        get => _uasset.GetNameReference(INDEX_MESH_PATH).Value;
        set => SetProperty(
            oldValue: MeshPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX_MESH_PATH, (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX_MESH_NAME, (FString) name);
            }
        );
    }
    public string BloodSplatterPath {
        get => _uasset.GetNameReference(INDEX_BLOODSPLATTER_PATH).Value;
        set => SetProperty(
            oldValue: BloodSplatterPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX_BLOODSPLATTER_PATH, (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX_BLOODSPLATTER_NAME, (FString) name);
            }
        );
    }
    public string ScabbardPath {
        get => _uasset.GetNameReference(INDEX_SCABBARD_PATH).Value;
        set => SetProperty(
            oldValue: ScabbardPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX_SCABBARD_PATH, (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX_SCABBARD_NAME, (FString) name);
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
                _uasset.SetNameReference(INDEX_FORM_NAME_C, (FString) $"{name}_C");
                _uasset.SetNameReference(INDEX_FORM_DEFAULT__NAME_C, (FString) $"DEFAULT__{name}_C");
            }
        );
    }

    #endregion Properties
}
