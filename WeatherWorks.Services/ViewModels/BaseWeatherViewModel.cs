namespace WeatherWorks.Services.ViewModels
{
    public class BaseWeatherViewModel
    {
        public decimal Temp { get; set; }
        public decimal Visibility { get; set; }
        public decimal WindSpeed { get; set; }
        public decimal Clouds { get; set; }
        public DateTimeOffset Sunrise { get; set; }
        public DateTimeOffset SunSet { get; set; }
        public decimal FeelsLike { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal TempMin { get; set; }

        public decimal TempMax { get; set; }

        public decimal Pressure { get; set; }

        public decimal Humidity { get; set; }

        public decimal SeaLevel { get; set; }
        public decimal GroundLevel { get; set; }
    }

}
