using LiveChartsCore.SkiaSharpView;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WeatherWorks.Infrastructure;
using WeatherWorks.Services.ViewModels;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.Painting.Effects;

namespace WeatherWorks
{
    /// <summary>
    /// List of Daily weather forcast
    /// </summary>
    public partial class WeatherDayList : UserControl
    {
        #region Dependency Properties
        //NOTE:Dependency property added for smooth scrolling with storyboard (Can not alter original offset directly)
        private static readonly FrameworkPropertyMetadata ScrollMetaData = new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnScrollOffsetChanged));

        public static readonly DependencyProperty ScrollOffsetProperty = DependencyProperty.Register("ScrollOffset",
            typeof(double), typeof(WeatherDayList), ScrollMetaData);
        #endregion

        private BackgroundWorker worker = new BackgroundWorker();
        private WeatherForcastViewModel? model { get; set; } = new WeatherForcastViewModel();
        public Axis[] XAxes { get; set; } = new Axis[]
        {
            new Axis
            {
                Name = "Time of Day",
                NamePaint = new SolidColorPaint(SKColors.WhiteSmoke),

                LabelsPaint = new SolidColorPaint(SKColors.AntiqueWhite),
                TextSize = 10,
                SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 2 }
            }
        };
        public Axis[] YAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "Temp °C",
                    NamePaint = new SolidColorPaint(SKColors.White),

                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 20,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                    {
                        StrokeThickness = 3,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    }
                }
            };

        public double ScrollOffset
        {
            get { return (double)GetValue(ScrollOffsetProperty); }
            set { SetValue(ScrollOffsetProperty, value); }
        }

        public WeatherDayList()
        {
            InitializeComponent();

            worker.DoWork += GetWeatherForcast;
            worker.RunWorkerCompleted += UpdateElements;
            OnLoad();
        }

        public void OnLoad()
        {
            Chart.XAxes = XAxes;
            Chart.YAxes = YAxes;
            worker.RunWorkerAsync();
        }

        private void UpdateElements(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (model != null)
            {
                var forcastGroupedByDate = model.DailyWeatherForcasts.OrderBy(x => x.ForcastDate!.Value.Date)
                                                                 .GroupBy(x => x.ForcastDate!.Value.Date);

                foreach (var dailyforcastGrouped in forcastGroupedByDate)
                {
                    var dailyforcastItem = new WeatherDayListItem(dailyforcastGrouped.ToList());
                    DailyListStackPanel.Children.Add(dailyforcastItem);
                }
            }
        }



        private void GetWeatherForcast(object? sender, DoWorkEventArgs e)
        {
            try
            {
                model = Task.Run(async () => await ServiceFactory.Instance!._weatherService.GetWeatherForcast()).Result;
            }
            catch
            (Exception ex)
            {
                MessageBox.Show(ex.Message, "Weather Day List Future Forcast", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }

        }


        /// <summary>
        /// This gets triggered every time the ScrollOffset changes and makes the acutal scroll move
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        private static void OnScrollOffsetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var objectType = obj as WeatherDayList;
            var sv = objectType!.ScrollViewerInstance;

            //TODO: Detect the max and min range for horizontal start and end
            //TIP: Can use the ScrollToEnd function and then store that value for left and right on element load

            if (objectType != null)
                sv.ScrollToHorizontalOffset(objectType.ScrollOffset);
        }
    }
}
