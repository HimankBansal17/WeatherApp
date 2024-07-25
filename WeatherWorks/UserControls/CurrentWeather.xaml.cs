using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WeatherWorks.Extensions;
using WeatherWorks.Infrastructure;
using WeatherWorks.Services.ViewModels;

namespace WeatherWorks.UserControls
{
    public partial class CurrentWeather : UserControl
    {
        private BackgroundWorker worker  = new BackgroundWorker();
        
        private CurrentWeatherViewModel? viewModel { get; set; } = new CurrentWeatherViewModel();
        public CurrentWeather()
        {
            InitializeComponent();
            OnLoad();
            worker.DoWork += GetCurrentWeather;
            worker.RunWorkerCompleted += UpdateElements;
        }

        private void UpdateElements(object? sender, RunWorkerCompletedEventArgs e)
        {
            if(viewModel != null)
            {
                CurrentTemperatureLabel.Content = viewModel.Temp + "°C";
                FeelsLikeTempLabel.Content = viewModel.FeelsLike + "°C";
                VisibilityLabel.Content = viewModel.Visibility / 1000 + " Km";
                WeatherDescLabel.Content = viewModel.Description.ToUpper();
                WindLabel.Content = viewModel.WindSpeed + "m/sec";
                PressureLabel.Content = viewModel.Pressure + " hPa";
                HumidityLabel.Content = viewModel.Humidity + "%";
                CloudsLabel.Content = viewModel.Clouds + "%";
                WeatherShortDescLabel.Content = viewModel.ShortDescription.ToUpper();
                WeatherIconImage.Source = ImageExtension.GetIconForWeather(viewModel.ShortDescription);
            }
            
        }
        
        private void GetCurrentWeather(object? sender, DoWorkEventArgs e)
        {
            try
            {
                viewModel = Task.Run(async () => await ServiceFactory.Instance._weatherService.GetCurrentWeather()).Result;
            }
            catch
            (Exception ex)
            {
                MessageBox.Show(ex.Message, "Current Weather", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            
        }

        public void OnLoad()
        {
            worker.RunWorkerAsync();
        }
    }
}
