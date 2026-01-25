// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using CAFESharp.ViewModels;
using CAFESharp.ViewModels.Weapon;
using Microsoft.Extensions.DependencyInjection;

namespace CAFESharp.Views.Weapon;

public sealed partial class BloodSplatterView : UserControl {
    #region Constructors

    public BloodSplatterView () {
        InitializeComponent();

        DataContext = App.Services
            .GetRequiredService<IEnumerable<UAssetViewModel>>()
            .First(x => x is BloodSplatterViewModel);
    }

    #endregion Constructors
}
