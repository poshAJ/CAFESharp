using System.IO;
using CommunityToolkit.Mvvm.Input;

namespace CAFESharp.ViewModels;

public partial class MainViewModel (
        BlueprintViewModel blueprintViewModel,
        WeaponViewModel weaponViewModel,
        MaterialInstanceViewModel materialInstanceViewModel,
        BloodSplatterViewModel bloodSplatterViewModel
    ) : BaseViewModel {
    #region Properties

    public BlueprintViewModel BlueprintViewModel { get; } = blueprintViewModel;
    public WeaponViewModel WeaponViewModel { get; } = weaponViewModel;
    public MaterialInstanceViewModel MaterialInstanceViewModel { get; } = materialInstanceViewModel;
    public BloodSplatterViewModel BloodSplatterViewModel { get; } = bloodSplatterViewModel;
    public bool CreateBackup { get; set; } = false;

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
