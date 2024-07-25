using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Windows;
using System.Windows.Controls;
using WeatherWorks.Helpers;
using WeatherWorks.Services.ViewModels;
using System.Collections.Generic;
using System.Linq;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Globalization;
using WeatherWorks.Extensions;

namespace WeatherWorks
{
    /// <summary>
    /// Interaction logic for WeatherDayListItem.xaml
    /// </summary>
    public partial class WeatherDayListItem : UserControl
    {
        public DateTime ItemDateKey { get; set; }
        List<DailyWeatherForcastModel> WeatherForecastForItem { get; set; }
        DailyWeatherForcastModel? FullDayForcast { get; set; }
        public ISeries[] Series { get; set; } = new ISeries[0];

        public WeatherDayListItem()
        {
            InitializeComponent();
        }

        public WeatherDayListItem(List<DailyWeatherForcastModel> dailyWeatherForcastModels)
        {
            InitializeComponent();
            WeatherForecastForItem = dailyWeatherForcastModels;
            FullDayForcast = dailyWeatherForcastModels.FirstOrDefault();
            UpdateLabels();

            if (FullDayForcast != null &&  FullDayForcast.ForcastDate!.Value.Date == DateTime.Today.Date)
            {
                UpdateChart();
            }
        }


        private void UpdateLabels()
        {
            if (FullDayForcast != null)
            {
                DayLabel.Content = FullDayForcast.ForcastDate!.Value.DayOfWeek.ToString();
                MaxTempLabel.Content = FullDayForcast.TempMax + "°C";
                MinTempLabel.Content = FullDayForcast.TempMin + "°C";
                HumidityLabel.Content = FullDayForcast.Humidity + "%";
                ShortDescriptionLabel.Content = FullDayForcast.ShortDescription;
                WeatherIcon.Source = ImageExtension.GetIconForWeather(FullDayForcast.ShortDescription);
            }
        }


        private void CreateChartSeries()
        {
            var tempratureLine = WeatherForecastForItem.Select(x => x.Temp);
            Series = new ISeries[]
            {
                new LineSeries<decimal>
                {
                    Values = tempratureLine,
                }
            };
        }

        private void Tile_Select(object sender, RoutedEventArgs e)
        {
            UpdateChart();
        }


        private void UpdateChart()
        {
            CreateChartSeries();
            if (WeatherForecastForItem.Any())
            {
                var result = VisualTreeExtension.FindChild<WeatherDayList>(Application.Current.MainWindow, "DailyList");
                var timeOfDay = WeatherForecastForItem.Select(x => x.ForcastDate!.Value.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture));
                result!.Chart.Series = Series;

                var updatedXAxis = new Axis[]
                {
                    new Axis
                    {
                        Name = "Time of Day",
                        NamePaint = new SolidColorPaint(SKColors.WhiteSmoke),
                        Labels = timeOfDay.ToList(),
                        LabelsPaint = new SolidColorPaint(SKColors.AntiqueWhite),
                        TextSize = 10,
                        SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 2 }
                    }
                };
                result.Chart.XAxes = updatedXAxis;

            }

        }
    }
}
