using System.Collections.Immutable;
using System.IO;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class MaterialInstanceViewModel : UAssetViewModel {
    #region Constants

    private readonly ImmutableDictionary<bool, ImmutableDictionary<string, int>> INDEX =
        ImmutableDictionary.Create<bool, ImmutableDictionary<string, int>>()
            .Add(true, ImmutableDictionary.Create<string, int>()
                .Add("Diffuse_Path", 24)
                .Add("Diffuse_Name", 6)
                .Add("NNRM_Path", 26)
                .Add("NNRM_Name", 7)
                .Add("Form_Path", 11)
                .Add("Form_Name", 10)
            )
            .Add(false, ImmutableDictionary.Create<string, int>()
                .Add("Diffuse_Path", 23)
                .Add("Diffuse_Name", 4)
                .Add("NNRM_Path", 25)
                .Add("NNRM_Name", 5)
                .Add("Form_Path", 10)
                .Add("Form_Name", 9)
            );

    #endregion Constants

    #region Properties

    // starting with a very naive approach and hoping there aren't a bunch of different mappings
    public bool HasNNRE { get => _uasset.GetNameMapIndexList().Count > 26 ? true : false; }
    public string DiffusePath {
        get => _uasset.GetNameReference(INDEX[HasNNRE]["Diffuse_Path"]).Value;
        set => SetProperty(
            oldValue: DiffusePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX[HasNNRE]["Diffuse_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX[HasNNRE]["Diffuse_Name"], (FString) name);
            }
        );
    }
    public string NNRMPath {
        get => _uasset.GetNameReference(INDEX[HasNNRE]["NNRM_Path"]).Value;
        set => SetProperty(
            oldValue: NNRMPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX[HasNNRE]["NNRM_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX[HasNNRE]["NNRM_Name"], (FString) name);
            }
        );
    }
    public string FormPath {
        get => _uasset.GetNameReference(INDEX[HasNNRE]["Form_Path"]).Value;
        set => SetProperty(
            oldValue: FormPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                _uasset.SetNameReference(INDEX[HasNNRE]["Form_Path"], (FString) path);

                string name = Path.GetFileNameWithoutExtension(path);
                _uasset.SetNameReference(INDEX[HasNNRE]["Form_Name"], (FString) name);
            }
        );
    }

    #endregion Properties
}
