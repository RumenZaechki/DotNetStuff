using Microsoft.AspNetCore.Mvc;

namespace MeditatR.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get(string town, CancellationToken token)
        {
            await _mediator.SendAsync<string>(town, token);
            return await _mediator.SendAsync<string, IEnumerable<WeatherForecast>>(town, token);
        }
    }
}
