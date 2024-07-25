using ClientLibrary.ExternalClients.MessageModels.ResponseMessage;
using WeatherWorks.Client.Messages.WeatherApiResponses;

namespace WeatherWorksService.Extensions
{
    public static class MappingExtensions
    {
        public static WeatherResponse ToServiceModel(this OpenWeatherWeatherResponse responseMessage)
        {
            
            var serviceModel = new WeatherResponse
            {
                FeelsLike = responseMessage.WeatherDetails.FeelsLike,
                Humidity = responseMessage.WeatherDetails.Humidity,
                MainWeather = responseMessage.Weather.First().MainWeather,
                Pressure = responseMessage.WeatherDetails.Pressure,
                TempMax = responseMessage.WeatherDetails.TempMax,
                TempMin = responseMessage.WeatherDetails.TempMin,
                Visibility = responseMessage.Visibility,
                WindDeg = responseMessage.Wind.Deg,
                WindSpeed = responseMessage.Wind.Speed,
                Temperature = responseMessage.WeatherDetails.Temp,
                WeatherDescription = responseMessage.Weather.First().Description,
                Cloud = responseMessage.Clouds.All,
                DateString = responseMessage.DateString,
                ProbbilityOfPrecipitation = responseMessage.ProbbilityOfPrecipitation 

            };

            if (responseMessage.SunTimes == null)
            {
                if (responseMessage.Sunrise.HasValue)
                {
                    serviceModel.Sunrise = new DateTimeOffset(responseMessage.Sunrise.Value, new TimeSpan());
                }

                if (responseMessage.Sunset.HasValue)
                {
                    serviceModel.Sunset = new DateTimeOffset(responseMessage.Sunset.Value, new TimeSpan());
                }
            }
            else
            {
                if (responseMessage.SunTimes != null)
                {
                    serviceModel.Sunrise = new DateTimeOffset(responseMessage.SunTimes.Sunrise, new TimeSpan());
                    serviceModel.Sunset = new DateTimeOffset(responseMessage.SunTimes.Sunset, new TimeSpan());
                }

            }

            if(!string.IsNullOrEmpty(responseMessage.DateString)) {

                serviceModel.ReportDate = DateTimeOffset.Parse(responseMessage.DateString);
                    
            }

            return serviceModel;
        }
    }
}
