using System.Configuration;
using WeatherWorks.Client;
using WeatherWorks.Services;

namespace WeatherWorks.Infrastructure
{
    public  class  ServiceFactory
    {
        public static ServiceFactory? Instance { get; private set; }
        public readonly IWeatherService _weatherService;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ServiceFactory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public ServiceFactory(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public void BuildService()
        {
            var apiKey = ConfigurationManager.AppSettings["WeatherWorks:Api:Key"];
            var baseUrl = ConfigurationManager.AppSettings["WeatherWorks:Api:BaseUrl"];
            
            if(apiKey == null || baseUrl == null)
            {
                throw new System.Exception("Error application not configured properly");
            }

            var client =  new WeatherWorksClient(baseUrl, apiKey);
            Instance = new ServiceFactory(new WeatherService(client));
             
        }
    }
}
