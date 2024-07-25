
using System.Windows;
using System.Windows.Threading;
using WeatherWorks.Infrastructure;

namespace WeatherWorks
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
      public App() {
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            var serviceFactory = new ServiceFactory();
            serviceFactory.BuildService();
        }
        void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Prevent default unhandled exception processing
            MessageBox.Show(
                $"There was an unexpected Error \n {e.Exception.Message}",
                "Alert",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            
            Application.Current.Shutdown();
        }
    }
}
