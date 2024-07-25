
using System.Windows;
using System.Windows.Media.Imaging;

namespace WeatherWorks.Extensions
{
    public static class ImageExtension
    {
        public static BitmapImage GetIconForWeather(string weather)
        {

            switch (weather)
            {
                case "Clear":
                    return (BitmapImage)Application.Current.Resources["SunnyIcon"];
                case "Clouds":
                    return (BitmapImage)Application.Current.Resources["CloudsIcon"];
                case "Rain":
                    return (BitmapImage)Application.Current.Resources["RainIcon"];
                case "Drizzle":
                    return (BitmapImage)Application.Current.Resources["RainIcon"];
                case "Thunderstorm":
                    return (BitmapImage)Application.Current.Resources["ThunderstormIcon"];
                default:
                    throw new System.Exception($"{weather} Icon Image not found");

            }

        }
    }
}
