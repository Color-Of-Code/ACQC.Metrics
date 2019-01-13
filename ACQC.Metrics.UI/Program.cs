using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using ACQC.Metrics.UI.ViewModels;
using ACQC.Metrics.UI.Views;

namespace ACQC.Metrics.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel());
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
    }
}
