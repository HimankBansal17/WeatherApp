using ClientLibrary.ExternalClients.MessageModels.ResponseMessage;
using ExternalClientLibrary.ExternalClients.MessageModels.ResponseMessage;
using Newtonsoft.Json;

namespace ClientLibrary.ExternalClients
{
    public interface IOpenWeatherAPIClient
    {
        public Task<OpenWeatherWeatherResponse?> GetCurrentWeather(decimal lat, decimal lon);

        public Task<OpenWeatherForcastResponse?> GetWeatherForcast(decimal lat, decimal lon);
    }
    public class OpenWeatherAPIClient : IOpenWeatherAPIClient
    {
        private HttpClient _httpClient;
        private string _apiKey;
        public OpenWeatherAPIClient(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<OpenWeatherWeatherResponse?> GetCurrentWeather(decimal lat, decimal lon)
        {

            var requestUri = _httpClient.BaseAddress + $"weather?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var responseJsonString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OpenWeatherWeatherResponse>(responseJsonString);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<OpenWeatherForcastResponse?> GetWeatherForcast(decimal lat, decimal lon)
        {
            var requestUri = _httpClient.BaseAddress + $"forecast?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var responseJsonString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OpenWeatherForcastResponse>(responseJsonString);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
