using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class BloodSplatterViewModel (
    ILogger<BloodSplatterViewModel> logger
) : UAssetViewModel {
    #region Fields

    private Dictionary<string, int> _map = [];

    #endregion Fields

    #region Properties

    public string MaterialInstancePath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["MaterialInstance_Path"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'MaterialInstancePath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: MaterialInstancePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["MaterialInstance_Path"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'MaterialInstancePath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: _map["MaterialInstance_Name"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'MaterialInstanceName'."
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

    [GeneratedRegex("/MIC_\\w+_WeapBloodSplatter$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex MaterialInstancePathRegex ();
    [GeneratedRegex("^MIC_\\w+_WeapBloodSplatter$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex MaterialInstanceNameRegex ();
    [GeneratedRegex("/MIC_\\w+(?<!_WeapBloodSplatter)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex FormPathRegex ();
    [GeneratedRegex("^MIC_\\w+(?<!_WeapBloodSplatter)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex FormNameRegex ();

    #endregion Regular Expressions

    #region Protected Methods

    protected override void MapNameReferences () {
        List<string> list = _uasset.GetNameMapIndexList().Select(x => x.Value).ToList();

        Dictionary<string, Regex> patterns = new() {
            { "MaterialInstance_Path", MaterialInstancePathRegex() },
            { "MaterialInstance_Name", MaterialInstanceNameRegex() },
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
