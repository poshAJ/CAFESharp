// Copyright (c) Ethan "CosmicBoogaloo" and Anthony J. Raymond, MIT License
using System;
using Avalonia;

namespace CAFESharp;

sealed class Program {
    [STAThread]
    public static void Main (string[] args) =>
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .StartWithClassicDesktopLifetime(args);
}
