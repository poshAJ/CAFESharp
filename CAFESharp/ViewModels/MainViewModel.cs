using System.IO;
using CommunityToolkit.Mvvm.Input;

namespace CAFESharp.ViewModels;

public partial class MainViewModel : BaseViewModel {
    #region Properties

    public BlueprintViewModel BlueprintViewModel { get; } = new();
    public WeaponViewModel WeaponViewModel { get; } = new();
    public MaterialInstanceViewModel MaterialInstanceViewModel { get; } = new();
    public BloodSplatterViewModel BloodSplatterViewModel { get; } = new();
    public bool CreateBackup { get; set; } = true;

    #endregion Properties

    #region Handlers

    [RelayCommand]
    private void Process () {
        UAssetViewModel[] viewModels = [
            BlueprintViewModel,
            WeaponViewModel,
            MaterialInstanceViewModel,
            BloodSplatterViewModel
        ];

        foreach (var viewModel in viewModels) {
            if (string.IsNullOrEmpty(viewModel.FilePath))
                continue;

            if (CreateBackup) {
                File.Copy(
                    sourceFileName: viewModel.FilePath,
                    destFileName: viewModel.FilePath + ".bak",
                    overwrite: true
                );
            }

            viewModel._uasset.Write(viewModel.FilePath);
        }
    }

    #endregion Handlers
}
