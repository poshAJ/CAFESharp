using CAFESharp.ViewModels;
using Ursa.Controls;

namespace CAFESharp.Views;

public partial class MainWindow : UrsaWindow {
    #region Properties

    public WindowToastManager ToastManager { get; set; }

    #endregion Properties

    #region Constructors

    public MainWindow (MainViewModel mainViewModel) {
        InitializeComponent();

        DataContext = mainViewModel;

        ToastManager = new(this);
    }

    #endregion Constructors
}
