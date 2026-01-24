// Copyright (c) Ethan Coley and Anthony J. Raymond, MIT License
using System;
using Avalonia;

namespace CAFESharp;

public sealed class Program {
    #region Main

    [STAThread]
    public static void Main (string[] args) => AppBuilder
        .Configure<App>()
        .UsePlatformDetect()
        .WithInterFont()
        .LogToTrace()
        .StartWithClassicDesktopLifetime(args);

    #endregion Main
}
