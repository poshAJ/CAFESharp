// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using CAFESharp.ViewModels;
using CAFESharp.ViewModels.Armor;
using Microsoft.Extensions.DependencyInjection;

namespace CAFESharp.Views.Armor;

public sealed partial class BlueprintView : UserControl {
    #region Constructors

    public BlueprintView () {
        InitializeComponent();

        DataContext = App.Services
            .GetRequiredService<IEnumerable<UAssetViewModel>>()
            .First(x => x is BlueprintViewModel);
    }

    #endregion Constructors
}
