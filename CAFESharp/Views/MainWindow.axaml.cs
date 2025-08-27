using Avalonia.Controls.Notifications;
using CAFESharp.ViewModels;
using Ursa.Controls;
using WindowNotificationManager = Ursa.Controls.WindowNotificationManager;

namespace CAFESharp.Views;

public partial class MainWindow : UrsaWindow {
    #region Properties

    public WindowNotificationManager NotificationManager { get; set; }

    #endregion Properties

    #region Constructors

    public MainWindow (MainViewModel mainViewModel) {
        InitializeComponent();

        DataContext = mainViewModel;

        NotificationManager = new(this) {
            Position = NotificationPosition.TopCenter
        };
    }

    #endregion Constructors
}
