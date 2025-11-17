using Avalonia;
using Avalonia.Markup.Xaml;
using AvaloniaChessApp.Views;

namespace AvaloniaChessApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Create and show the main window
        var mainWindow = new MainWindow();
        if (mainWindow != null)
        {
            mainWindow.Show();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
