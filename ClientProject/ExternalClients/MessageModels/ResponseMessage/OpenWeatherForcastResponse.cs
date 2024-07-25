using ClientLibrary.ExternalClients.MessageModels.ResponseMessage;
using Newtonsoft.Json;

namespace ExternalClientLibrary.ExternalClients.MessageModels.ResponseMessage
{
    public class OpenWeatherForcastResponse
    {
        [JsonProperty("list")]
        public List<OpenWeatherWeatherResponse> WeatherForcast { get; set; } = new List<OpenWeatherWeatherResponse>();
    }
}
