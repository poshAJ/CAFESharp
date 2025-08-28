using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace CAFESharp.ViewModels;

public partial class MainViewModel (
        ILogger<MainViewModel> logger,
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

    #endregion Properties

    #region Handlers

    [RelayCommand]
    private void SaveAll () {
        UAssetViewModel[] viewModels = [
            BlueprintViewModel,
            WeaponViewModel,
            MaterialInstanceViewModel,
            BloodSplatterViewModel
        ];

        foreach (var viewModel in viewModels) {
            if (string.IsNullOrEmpty(viewModel.FilePath))
                continue;

            try {
                viewModel._uasset.Write(viewModel.FilePath);

                logger.LogInformation(message: "Saved '{FileName}'.", viewModel.FileName);
            } catch {
                logger.LogError(message: "An error occured while saving '{FileName}'.", viewModel.FileName);
            }

        }
    }

    #endregion Handlers
}
