using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class WeaponViewModel (
    ILogger<WeaponViewModel> logger
) : UAssetViewModel {
    #region Fields

    private Dictionary<string, int> _map = [];

    #endregion Fields

    #region Properties

    public string IconPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["Icon_Path"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'IconPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: IconPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["Icon_Path"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'IconPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: _map["Icon_Name"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'IconName'."
                    )
                );
            }
        );
    }
    public string BlueprintPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["Blueprint_Path"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'BlueprintPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: BlueprintPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["Blueprint_Path"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BlueprintPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: _map["Blueprint_Name"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BlueprintName'."
                    )
                );
                uasset.TrySetNameReferenceValue(
                    index: _map["Blueprint_Name_C"],
                    value: $"{name}_C",
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'BlueprintName'."
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
                    index: _map["Form_Name"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'FormName'."
                    )
                );
            }
        );
    }

    #endregion Properties

    #region Regular Expressions

    [GeneratedRegex("/T_\\w+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex IconPathRegex ();
    [GeneratedRegex("^T_\\w+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex IconNameRegex ();
    [GeneratedRegex("/BP_\\w+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex BlueprintPathRegex ();
    [GeneratedRegex("^BP_\\w+(?<!_C)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex BlueprintNameRegex ();
    [GeneratedRegex("^BP_\\w+_C$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex BlueprintNameCRegex ();
    [GeneratedRegex("/Weap\\w+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex FormPathRegex ();
    [GeneratedRegex("^Weap\\w+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex FormNameRegex ();

    #endregion Regular Expressions

    #region Protected Methods

    protected override void MapNameReferences () {
        List<string> list = _uasset.GetNameMapIndexList().Select(x => x.Value).ToList();

        Dictionary<string, Regex> patterns = new() {
            { "Icon_Path", IconPathRegex() },
            { "Icon_Name", IconNameRegex() },
            { "Blueprint_Path", BlueprintPathRegex() },
            { "Blueprint_Name", BlueprintNameRegex() },
            { "Blueprint_Name_C", BlueprintNameCRegex() },
            { "Form_Path", FormPathRegex() },
            { "Form_Name", FormNameRegex() }
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
