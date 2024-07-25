using Newtonsoft.Json;
using WeatherWorks.Client.Messages;
using WeatherWorks.Client.Messages.WeatherApiResponses;

namespace WeatherWorks.Client
{
    public interface IWeatherWorksClient
    {
        public Task<ApiResponse<WeatherResponse>> GetCurrentWeatherByLatLon(decimal lat, decimal lon);
        public Task<ApiResponse<ForcastWeatherResponse>> GetFutureForcast(decimal lat, decimal lon);
    }
    public class WeatherWorksClient : IWeatherWorksClient
    {
        private readonly HttpClient _httpClient;
        public WeatherWorksClient(string baseAddress, string apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }
        public async Task<ApiResponse<WeatherResponse>> GetCurrentWeatherByLatLon(decimal lat, decimal lon)
        {
            var requestUri = _httpClient.BaseAddress + $"WeatherForecast/GetCurrentWeather?lat={lat}&lon={lon}";
            var request = new HttpRequestMessage(HttpMethod.Get,requestUri);
            var result = await _httpClient.SendAsync(request);
            if(result.IsSuccessStatusCode)
            {
                var responseJsonString = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ApiResponse<WeatherResponse>>(responseJsonString);
                return response;
            }
            else
            {
                return new ApiResponse<WeatherResponse>
                {
                    IsSuccess = false,
                    StatusCode = result.StatusCode,
                    ErrorMessage = result.ReasonPhrase
                };
            }
            
        }

        public async Task<ApiResponse<ForcastWeatherResponse>> GetFutureForcast(decimal lat, decimal lon)
        {
            var requestUri = _httpClient.BaseAddress + $"WeatherForecast/GetFutureForcast?lat={lat}&lon={lon}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var result = await _httpClient.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                var responseJsonString = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ApiResponse<ForcastWeatherResponse>>(responseJsonString);
                return response;
            }
            else
            {
                return new ApiResponse<ForcastWeatherResponse>
                {
                    IsSuccess = false,
                    StatusCode = result.StatusCode,
                    ErrorMessage = result.ReasonPhrase
                };
            }

        }
    }
}