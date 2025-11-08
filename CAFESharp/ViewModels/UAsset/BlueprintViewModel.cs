using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class BlueprintViewModel (
    ILogger<BlueprintViewModel> logger
) : UAssetViewModel {
    #region Fields

    private Dictionary<string, int> _map = [];

    #endregion Fields

    #region Properties

    public bool HasScabbard {
        get => _map.ContainsKey("Scabbard_Path");
    }
    public bool IsLoadedHasScabbard {
        get => IsLoaded && HasScabbard;
    }
    public string MeshPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["Mesh_Path"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'MeshPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: MeshPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["Mesh_Path"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'MeshPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: _map["Mesh_Name"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'MeshName'."
                    )
                );
            }
        );
    }
    public string BloodSplatterPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["BloodSplatter_Path"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'BloodSplatterPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: BloodSplatterPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["BloodSplatter_Path"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BloodSplatterPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: _map["BloodSplatter_Name"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BloodSplatterName'."
                    )
                );
            }
        );
    }
    public string ScabbardPath {
        get {
            if (!HasScabbard) {
                return string.Empty;
            }

            return _uasset.TryGetNameReferenceValue(
                index: _map["Scabbard_Path"],
                onError: (_) => logger.LogWarning(
                    message: "The value retrieved for 'ScabbardPath' does not appear valid."
                )
            );
        }
        set => SetProperty(
            oldValue: ScabbardPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                if (!HasScabbard) {
                    return;
                }

                uasset.TrySetNameReferenceValue(
                    index: _map["Scabbard_Path"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'ScabbardPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: _map["Scabbard_Name"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'ScabbardName'."
                    )
                );
            }
        );
    }
    public string FormPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["Form_Path"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'FormPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: FormPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["Form_Path"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'FormPath'."
                    )
                );

                uasset.FolderName = (FString) path;

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: _map["Form_Name_C"],
                    value: $"{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'FormName'."
                    )
                );
                uasset.TrySetNameReferenceValue(
                    index: _map["Form_Default__Name_C"],
                    value: $"DEFAULT__{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'FormName'."
                    )
                );
            }
        );
    }

    #endregion Properties

    #region Regular Expressions

    [GeneratedRegex("/SM_\\w+(?<!_Scabbard)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex MeshPathRegex ();
    [GeneratedRegex("^SM_\\w+(?<!_Scabbard)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex MeshNameRegex ();
    [GeneratedRegex("/MIC_\\w+_WeapBloodSplatter$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex BloodSplatterPathRegex ();
    [GeneratedRegex("^MIC_\\w+_WeapBloodSplatter$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex BloodSplatterNameRegex ();
    [GeneratedRegex("/SM_\\w+_Scabbard$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex ScabbardPathRegex ();
    [GeneratedRegex("^SM_\\w+_Scabbard$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex ScabbardNameRegex ();
    [GeneratedRegex("/BP_\\w+(?<!Generic.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex FormPathRegex ();
    [GeneratedRegex("^BP_\\w+_C(?<!Generic.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex FormNameCRegex ();
    [GeneratedRegex("^Default__BP_\\w+_C(?<!Generic.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex FormDefaultNameCRegex ();

    #endregion Regular Expressions

    #region Protected Methods

    protected override void MapNameReferences () {
        List<string> list = _uasset.GetNameMapIndexList().Select(x => x.Value).ToList();

        Dictionary<string, Regex> patterns = new() {
            { "Mesh_Path", MeshPathRegex() },
            { "Mesh_Name", MeshNameRegex() },
            { "BloodSplatter_Path", BloodSplatterPathRegex() },
            { "BloodSplatter_Name", BloodSplatterNameRegex() },
            { "Scabbard_Path", ScabbardPathRegex() },
            { "Scabbard_Name", ScabbardNameRegex() },
            { "Form_Path", FormPathRegex() },
            { "Form_Name_C", FormNameCRegex() },
            { "Form_Default__Name_C", FormDefaultNameCRegex() }
    };

        foreach (KeyValuePair<string, Regex> pattern in patterns) {
            int index = list.FindIndex(pattern.Value.IsMatch);

            if (index == -1) {
                logger.LogError(
                    message: "An error occured while mapping '{key}'.",
                    args: pattern.Key
                );

                _map.Clear();

                break;
            }

            _map[pattern.Key] = index;
        }
    }

    #endregion Protected Methods
}
