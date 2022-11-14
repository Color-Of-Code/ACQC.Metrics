using Avalonia;
using ACQC.Metrics.UI.ViewModels;
using ACQC.Metrics.UI.Views;
using Avalonia.ReactiveUI;

namespace ACQC.Metrics.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel());
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                        .UsePlatformDetect()
                        .UseReactiveUI()
                        .LogToTrace();
        }
    }
}
