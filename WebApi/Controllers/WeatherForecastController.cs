using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Context;
using WebApi.Domain;
using WebApi.Logging;
using WebApi.Validators;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IValidator<WeatherForecast> _validator;
        private readonly LoggingBehaviour<WeatherForecast, int> logging;
        private readonly IMapper mapper;
        private readonly LogDbContext dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IValidator<WeatherForecast> validator, LoggingBehaviour<WeatherForecast, int> logging, IMapper mapper, LogDbContext dbContext)
        {
            _logger = logger;
            _validator = validator;
            this.logging = logging;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }


        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> GetAsync(string id)
        {
            _logger.LogInformation(id);

            WeatherForecast[] weathers = Enumerable.Range(1, 3).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return weathers;
        }


        [HttpPost(Name = "PostGWeatherForecast")]
        public async Task<WeatherForecast> CreateAsync([FromBody] WeatherForecastDto weather)
        {

            var sw = new Stopwatch();

            sw.Start();

            WeatherForecast w = mapper.Map<WeatherForecast>(weather);

            await ModelValidation<WeatherForecast>.ValidateAsync(w, _validator);

            var response = new WeatherForecast
            {
                Date = DateTime.Now.AddDays(7),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

            sw.Stop();

            return response;
        }


        [HttpPut(Name = "GetOperationLogList")]
        public async Task<List<OperationLog>> GetOperationLogList()
        {
            var res = dbContext.OperationLogs.ToList();

            //res.ForEach(x => x.Answer = "");

            return res;
        }

    }
}