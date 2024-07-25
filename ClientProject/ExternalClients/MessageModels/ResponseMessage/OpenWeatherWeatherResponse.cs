
using Newtonsoft.Json;

namespace ClientLibrary.ExternalClients.MessageModels.ResponseMessage
{
    public class OpenWeatherWeatherResponse
    {
        [JsonProperty("coord")]
        public Coordinates Coord { get; set; }
        [JsonProperty("Weather")]
        public List<Weather> Weather { get; set; } = new List<Weather>();
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("timezone")]
        public long Timezone { get; set; }
        [JsonProperty("main")]
        public WeatherDetails WeatherDetails { get; set; }
        [JsonProperty("visibility")]
        public decimal Visibility { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sys")]
        public SunTimes? SunTimes { get; set; }
        [JsonProperty("clouds")]
        public Cloud Clouds { get; set; }

        [JsonProperty("pop")]
        public decimal? ProbbilityOfPrecipitation { get; set; }

        [JsonProperty("dt_txt")]
        public string? DateString { get; set; }

        [JsonProperty("sunrise")]
        public long? Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long? Sunset { get; set; }


    }

    public class Coordinates {

        [JsonProperty("lat")]
        public decimal Latitude { get; set; }
        [JsonProperty("lon")]
        public decimal Longitude { get; set; }
    }
    public class Cloud
    {
        [JsonProperty("all")]
        public decimal All { get; set; }
    }

    public class SunTimes
    {
        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }
    public class Wind
    {
        [JsonProperty("speed")]
        public decimal Speed { get; set; }
        [JsonProperty("deg")]
        public decimal Deg { get; set; }

        [JsonProperty("gust")]
        public decimal Gust { get; set; }
    }

    public class WeatherDetails
    {
        [JsonProperty("temp")]
        public long Temp { get; set; }
        [JsonProperty("feels_like")]
        public decimal FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public decimal TempMin { get; set; }

        [JsonProperty("temp_max")]
        public decimal TempMax { get; set; }

        [JsonProperty("pressure")]
        public decimal Pressure { get; set; }

        [JsonProperty("humidity")]
        public decimal Humidity { get; set; }

        [JsonProperty("sea_level")]
        public decimal SeaLevel { get; set; }

        [JsonProperty("grnd_level")]
        public decimal GroundLevel { get; set; }

    }


    public class Weather
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("main")]
        public string MainWeather { get; set; }
        [JsonProperty("description")]
        public string Description { get; set;}
    }
}
