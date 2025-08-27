using System.Collections.Immutable;
using System.IO;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class BlueprintViewModel : UAssetViewModel {
    #region Constants

    private readonly ImmutableDictionary<bool, ImmutableDictionary<string, int>> INDEX =
        ImmutableDictionary.Create<bool, ImmutableDictionary<string, int>>()
            .Add(true, ImmutableDictionary.Create<string, int>()
                .Add("Mesh_Path", 53)
                .Add("Mesh_Name", 55)
                .Add("BloodSplatter_Path", 0)
                .Add("BloodSplatter_Name", 49)
                .Add("Scabbard_Path", 56)
                .Add("Scabbard_Name", 57)
                .Add("Form_Path", 24)
                .Add("Form_Name_C", 10)
                .Add("Form_Default__Name_C", 11)
            )
            .Add(false, ImmutableDictionary.Create<string, int>()
                .Add("Mesh_Path", 52)
                .Add("Mesh_Name", 54)
                .Add("BloodSplatter_Path", 0)
                .Add("BloodSplatter_Name", 48)
                .Add("Form_Path", 23)
                .Add("Form_Name_C", 10)
                .Add("Form_Default__Name_C", 11)
            );

    #endregion Constants

    #region Properties

    // starting with a very naive approach and hoping there aren't a bunch of different mappings
    public bool HasScabbard { get => _uasset.GetNameMapIndexList().Count > 55 ? true : false; }
    public string MeshPath {
        get => _uasset.GetNameReference(INDEX[HasScabbard]["Mesh_Path"]).Value;
        set => SetProperty(
            oldValue: MeshPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX[HasScabbard]["Mesh_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX[HasScabbard]["Mesh_Name"], (FString) name);
            }
        );
    }
    public string BloodSplatterPath {
        get => _uasset.GetNameReference(INDEX[HasScabbard]["BloodSplatter_Path"]).Value;
        set => SetProperty(
            oldValue: BloodSplatterPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX[HasScabbard]["BloodSplatter_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX[HasScabbard]["BloodSplatter_Name"], (FString) name);
            }
        );
    }
    public string ScabbardPath {
        get => HasScabbard ? _uasset.GetNameReference(INDEX[HasScabbard]["Scabbard_Path"]).Value : string.Empty;
        set => SetProperty(
            oldValue: ScabbardPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                if (!HasScabbard) return;

                _uasset.SetNameReference(INDEX[HasScabbard]["Scabbard_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX[HasScabbard]["Scabbard_Name"], (FString) name);
            }
        );
    }
    public string FormPath {
        get => _uasset.GetNameReference(INDEX[HasScabbard]["Form_Path"]).Value;
        set => SetProperty(
            oldValue: FormPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX[HasScabbard]["Form_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX[HasScabbard]["Form_Name_C"], (FString) $"{name}_C");
                _uasset.SetNameReference(INDEX[HasScabbard]["Form_Default__Name_C"], (FString) $"DEFAULT__{name}_C");
            }
        );
    }

    #endregion Properties
}
