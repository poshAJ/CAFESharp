// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using CAFESharp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Armor = CAFESharp.ViewModels.Armor;
using Weapon = CAFESharp.ViewModels.Weapon;

namespace CAFESharp.Extensions;

public static class IServiceCollectionExtensions {
    #region Methods

    public static IServiceCollection AddViewModels (
        this IServiceCollection serviceCollection
    ) {
        serviceCollection.AddSingleton<UAssetViewModel, Armor.ArmorViewModel>();
        serviceCollection.AddSingleton<UAssetViewModel, Armor.BlueprintViewModel>();
        serviceCollection.AddSingleton<UAssetViewModel, Armor.MaterialInstanceViewModel>();
        serviceCollection.AddSingleton<UAssetViewModel, Armor.BodyPartViewModel>();

        serviceCollection.AddSingleton<UAssetViewModel, Weapon.WeaponViewModel>();
        serviceCollection.AddSingleton<UAssetViewModel, Weapon.BlueprintViewModel>();
        serviceCollection.AddSingleton<UAssetViewModel, Weapon.MaterialInstanceViewModel>();
        serviceCollection.AddSingleton<UAssetViewModel, Weapon.BloodSplatterViewModel>();

        serviceCollection.AddSingleton<MainViewModel>();

        return serviceCollection;
    }

    #endregion Methods
}
