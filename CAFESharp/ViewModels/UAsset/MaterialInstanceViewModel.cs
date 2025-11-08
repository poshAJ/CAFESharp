using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CAFESharp.Extensions;
using Microsoft.Extensions.Logging;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.ViewModels;

public partial class MaterialInstanceViewModel (
    ILogger<MaterialInstanceViewModel> logger
) : UAssetViewModel {
    #region Fields

    private Dictionary<string, int> _map = [];

    #endregion Fields

    #region Properties

    public string DiffusePath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["Diffuse_Path"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'DiffusePath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: DiffusePath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["Diffuse_Path"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'DiffusePath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: _map["Diffuse_Name"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'DiffuseName'."
                    )
                );
            }
        );
    }
    public string NNRMPath {
        get => _uasset.TryGetNameReferenceValue(
            index: _map["NNRM_Path"],
            onError: (_) => logger.LogWarning(
                message: "The value retrieved for 'NNRMPath' does not appear valid."
            )
        );
        set => SetProperty(
            oldValue: NNRMPath,
            newValue: value,
            model: _uasset,
            callback: (uasset, path) => {
                uasset.TrySetNameReferenceValue(
                    index: _map["NNRM_Path"],
                    value: path,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'NNRMPath'."
                    )
                );

                string name = Path.GetFileNameWithoutExtension(path: path);
                uasset.TrySetNameReferenceValue(
                    index: _map["NNRM_Name"],
                    value: name,
                    onError: (_) => logger.LogError(
                        message: "An error occured while setting 'NNRMName'."
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

    [GeneratedRegex("/T_\\w+_D$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex DiffusePathRegex ();
    [GeneratedRegex("^T_\\w+_D$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex DiffuseNameRegex ();
    [GeneratedRegex("/T_\\w+_NNRM$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex NNRMPathRegex ();
    [GeneratedRegex("^T_\\w+_NNRM$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex NNRMNameRegex ();
    [GeneratedRegex("/MIC_\\w+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex FormPathRegex ();
    [GeneratedRegex("^MIC_\\w+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex FormNameRegex ();

    #endregion Regular Expressions

    #region Protected Methods

    protected override void MapNameReferences () {
        List<string> list = _uasset.GetNameMapIndexList().Select(x => x.Value).ToList();

        Dictionary<string, Regex> patterns = new() {
            { "Diffuse_Path", DiffusePathRegex() },
            { "Diffuse_Name", DiffuseNameRegex() },
            { "NNRM_Path", NNRMPathRegex() },
            { "NNRM_Name", NNRMNameRegex() },
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
