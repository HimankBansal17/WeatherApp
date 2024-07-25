
namespace WeatherWorks.Client.Messages.WeatherApiResponses
{
    public class WeatherResponse
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal WindSpeed { get; set; }
        public decimal WindDeg { get; set; }
        public decimal Temperature { get; set; }
        public decimal TempMin { get; set; }
        public decimal TempMax { get; set; }
        public decimal FeelsLike { get; set; }
        public decimal Pressure { get; set; }
        public decimal Humidity { get; set; }
        public decimal Visibility { get; set; }
        public DateTimeOffset Sunrise { get; set; }
        public DateTimeOffset Sunset {get;set;}
        public string WeatherDescription { get; set; }
        public string MainWeather { get; set; }
        public decimal Cloud { get; set; }
        public decimal? ProbbilityOfPrecipitation { get; set; }
        public string? DateString { get; set; }

        public DateTimeOffset? ReportDate { get; set; }
    }
}
