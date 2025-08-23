using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CAFESharp.ViewModels;

namespace CAFESharp;

public class ViewLocator : IDataTemplate {
    #region IDataTemplate Members

    public Control? Build (object? param) {
        if (param is null)
            return null;

        string name = param
            .GetType()
            .FullName!
            .Replace(
                oldValue: "ViewModel",
                newValue: "View",
                comparisonType: StringComparison.Ordinal
            );
        Type? type = Type.GetType(name);

        if (type is not null) {
            return (Control) Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match (object? data) {
        return data is BaseViewModel;
    }

    #endregion IDataTemplate Members
}
