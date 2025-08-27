using System.Collections.Immutable;
using System.IO;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class WeaponViewModel : UAssetViewModel {
    #region Constants

    private readonly ImmutableDictionary<string, int> INDEX =
        ImmutableDictionary.Create<string, int>()
            .Add("Icon_Path", 0)
            .Add("Icon_Name", 4)
            .Add("Blueprint_Path", 1)
            .Add("Blueprint_Name", 2)
            .Add("Blueprint_Name_C", 3)
            .Add("Form_Path", 7)
            .Add("Form_Name", 5);

    #endregion Constants

    #region Properties

    public string IconPath {
        get => _uasset.GetNameReference(INDEX["Icon_Path"]).Value;
        set => SetProperty(
            oldValue: IconPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX["Icon_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX["Icon_Name"], (FString) name);
            }
        );
    }
    public string BlueprintPath {
        get => _uasset.GetNameReference(INDEX["Blueprint_Path"]).Value;
        set => SetProperty(
            oldValue: BlueprintPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX["Blueprint_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX["Blueprint_Name"], (FString) name);
                _uasset.SetNameReference(INDEX["Blueprint_Name_C"], (FString) $"{name}_C");
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
