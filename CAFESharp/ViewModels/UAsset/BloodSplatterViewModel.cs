using System.Collections.Immutable;
using System.IO;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class BloodSplatterViewModel : UAssetViewModel {
    #region Constants

    private readonly ImmutableDictionary<string, int> INDEX =
        ImmutableDictionary.Create<string, int>()
            .Add("MaterialInstance_Path", 8)
            .Add("MaterialInstance_Name", 9)
            .Add("Form_Path", 2)
            .Add("Form_Name", 1);

    #endregion Constants

    #region Properties

    public string MaterialInstancePath {
        get => _uasset.GetNameReference(INDEX["MaterialInstance_Path"]).Value;
        set => SetProperty(
            oldValue: MaterialInstancePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX["MaterialInstance_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX["MaterialInstance_Name"], (FString) name);
            }
        );
    }
    public string FormPath {
        get => _uasset.GetNameReference(INDEX["Form_Path"]).Value;
        set => SetProperty(
            oldValue: FormPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX["Form_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX["Form_Name"], (FString) name);
            }
        );
    }

    #endregion Properties
}
