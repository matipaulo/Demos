namespace MinimalAPIs.Endpoints
{
    using Services;

    public class WeatherForecastEndpoints : IEndpointRouting
    {
        private readonly WeatherForecastService _forecastService;

        public WeatherForecastEndpoints(WeatherForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        private IResult GetWeatherForecasts()
        {
            return Results.Ok(_forecastService.GetWeatherForecasts());
        }

        /// <inheritdoc />
        public void Register(WebApplication webApplication)
        {
            webApplication.MapGet("/weatherforecast", GetWeatherForecasts).WithTags("WeatherForecast");
            webApplication.MapGet("/weatherforecast/{id}", GetWeatherForecastsById).WithTags("WeatherForecast");
            webApplication.MapMethods("/weatherforecast", new[] { "PATCH" }, PatchWeatherForecast).WithTags("WeatherForecast");
        }

        private static Task PatchWeatherForecast(WeatherForecast weatherForecast)
        {
            throw new NotImplementedException();
        }

        private IResult GetWeatherForecastsById(int id)
        {
            return Results.Ok(_forecastService.GetWeatherForecastsById(id));
        }
    }
}