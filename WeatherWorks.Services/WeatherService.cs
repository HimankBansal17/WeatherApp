using WeatherWorks.Client;
using WeatherWorks.Services.ViewModels;

namespace WeatherWorks.Services
{
    public interface IWeatherService
    {
        public Task<CurrentWeatherViewModel?> GetCurrentWeather();
        public Task<WeatherForcastViewModel?> GetWeatherForcast();
    }

    public class WeatherService : IWeatherService
    {
        private readonly IWeatherWorksClient _weatherServiceClient;

        public WeatherService(IWeatherWorksClient weatherServiceClient)
        {
            _weatherServiceClient = weatherServiceClient;
        }

        public async Task<CurrentWeatherViewModel?> GetCurrentWeather()
        {
            try
            {
                var response = await _weatherServiceClient.GetCurrentWeatherByLatLon(null, null);
                
                if (response == null || !response.IsSuccess || response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }
                else
                {
                    var weatherDetails = response.Data;
                    return new CurrentWeatherViewModel
                    {
                        Description = weatherDetails.WeatherDescription,
                        FeelsLike = weatherDetails.FeelsLike,
                        Pressure = weatherDetails.Pressure,
                        Humidity = weatherDetails.Humidity,
                        TempMax = weatherDetails.TempMax,
                        TempMin = weatherDetails.TempMin,
                        Temp = weatherDetails.Temperature,
                        Visibility = weatherDetails.Visibility,
                        WindSpeed = weatherDetails.WindSpeed,
                        Clouds = weatherDetails.Cloud,
                        Sunrise = weatherDetails.Sunrise.ToLocalTime(),
                        SunSet = weatherDetails.Sunset.ToLocalTime(),
                        ShortDescription = weatherDetails.MainWeather
                    };
                }
            }
            catch (TimeoutException)
            {
                throw new Exception("There was a timeout while trying to access Api");
            }
            catch (Exception)
            {
                throw new Exception("There was an issue retreiving data from external service");
            }
        }

        public async Task<WeatherForcastViewModel?> GetWeatherForcast()
        {
            try
            {
                var response = await _weatherServiceClient.GetFutureForcast(-27.40m, 153.02m);

                if (response == null || !response.IsSuccess || response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }
                else
                {
                    var forcasts = response.Data;

                    return new WeatherForcastViewModel
                    {
                        DailyWeatherForcasts = forcasts.WeatherResponse.Select(weatherDetails => new DailyWeatherForcastModel
                        {
                            Description = weatherDetails.WeatherDescription,
                            FeelsLike = weatherDetails.FeelsLike,
                            Pressure = weatherDetails.Pressure,
                            Humidity = weatherDetails.Humidity,
                            TempMax = weatherDetails.TempMax,
                            TempMin = weatherDetails.TempMin,
                            Temp = weatherDetails.Temperature,
                            Visibility = weatherDetails.Visibility,
                            WindSpeed = weatherDetails.WindSpeed,
                            Clouds = weatherDetails.Cloud,
                            Sunrise = weatherDetails.Sunrise.ToLocalTime(),
                            SunSet = weatherDetails.Sunset.ToLocalTime(),
                            ShortDescription = weatherDetails.MainWeather,
                            ForcastDate = weatherDetails.ReportDate,
                            ProbbilityOfPrecipitation = weatherDetails.ProbbilityOfPrecipitation,

                        }).ToList()
                    };
                }
            }
            catch(TimeoutException)
            {
                throw new Exception("There was a timeout while trying to access Api");
            }
            catch (Exception)
            {
                throw new Exception("There was an issue retreiving data from external service");
            }
            
        }
    }
}