using ClientLibrary.ExternalClients;
using WeatherWorks.Client.Messages.WeatherApiResponses;
using WeatherWorksService.Extensions;

namespace WeatherWorksService.Services
{
    public interface IWeatherService
    {
        public Task<WeatherResponse?> GetCurrentWeather(decimal lat, decimal lon);
        public Task<ForcastWeatherResponse?> GetWeatherForcast(decimal lat, decimal lon);
    }
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherAPIClient _openWeatherApiClient;

        public WeatherService(IOpenWeatherAPIClient openWeatherAPIClient)
        {
            _openWeatherApiClient = openWeatherAPIClient;
        }
        public async Task<WeatherResponse?> GetCurrentWeather(decimal lat, decimal lon)
        {
            var result = await _openWeatherApiClient.GetCurrentWeather(lat, lon);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result.ToServiceModel();
            }

        }

        public async Task<ForcastWeatherResponse?> GetWeatherForcast(decimal lat, decimal lon)
        {
            var result = await _openWeatherApiClient.GetWeatherForcast(lat, lon);
            if (result == null)
            {
                return null;
            }
            else
            {
                return new ForcastWeatherResponse
                {
                    WeatherResponse = result.WeatherForcast.Select(x => x.ToServiceModel()).ToList()
                };
            }
        }
    }
}
