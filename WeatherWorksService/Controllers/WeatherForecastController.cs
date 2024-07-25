using ClientLibrary.ExternalClients;
using Microsoft.AspNetCore.Mvc;
using WeatherWorks.Client.Messages;
using WeatherWorks.Client.Messages.WeatherApiResponses;
using WeatherWorksService.Services;

namespace WeatherWorksService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet("GetCurrentWeather")]
        public async Task<ApiResponse<WeatherResponse>> GetCurrentWeather(decimal? lat, decimal? lon)
        {
            if(lat == null || lon == null)
            {
                return new ApiResponse<WeatherResponse>
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessage = "Latitude and Longitude can not be null",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            var result = await _weatherService.GetCurrentWeather(lat.Value, lon.Value);

            return new ApiResponse<WeatherResponse>
            {
                Data = result,
                ErrorMessage =  result == null ?  "InternalServerError: Unable to Retrive Required Information" : string.Empty,
                IsSuccess = result != null,
                StatusCode = result == null ? System.Net.HttpStatusCode.InternalServerError : System.Net.HttpStatusCode.OK

            };
            
        }

        [HttpGet("GetFutureForcast")]
        public async Task<ApiResponse<ForcastWeatherResponse>> GetFutureForcast(decimal? lat, decimal? lon)
        {

            if (lat == null || lon == null)
            {
                return new ApiResponse<ForcastWeatherResponse>
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessage = "Latitude and Longitude can not be null",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }

            var result = await _weatherService.GetWeatherForcast(lat.Value, lon.Value);
            return new ApiResponse<ForcastWeatherResponse>
            {
                Data = result,
                ErrorMessage = result == null ? "InternalServerError: Unable to Retrive Required Information" : string.Empty,
                IsSuccess = result != null,
                StatusCode = result == null ? System.Net.HttpStatusCode.InternalServerError : System.Net.HttpStatusCode.OK
            };
        }

    }
}