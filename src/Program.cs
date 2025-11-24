using System;
using Avalonia;
using AvaloniaChessApp;
using DotNetEnv;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Env.TraversePath().Load();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}
