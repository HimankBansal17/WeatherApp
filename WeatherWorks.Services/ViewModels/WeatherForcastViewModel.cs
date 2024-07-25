
namespace WeatherWorks.Services.ViewModels
{
    public class WeatherForcastViewModel 
    {
        public List<DailyWeatherForcastModel> DailyWeatherForcasts { get; set;}
    }


    public class DailyWeatherForcastModel : BaseWeatherViewModel
    {
        public decimal? ProbbilityOfPrecipitation { get; set; }
        public DateTimeOffset? ForcastDate { get; set; }
    }
}
