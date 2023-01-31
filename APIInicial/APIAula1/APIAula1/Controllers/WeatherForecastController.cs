using Microsoft.AspNetCore.Mvc;

namespace APIAula1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Tempos")]
        public IEnumerable<WeatherForecast> ConsultaTempos()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public WeatherForecast ConsultaTempo()
        {
            return new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpPost]
        public void InserirTempo(WeatherForecast tempoParametro)
        {
            Console.WriteLine("Inserir");
        }

        [HttpPut]
        public void AtualizarTempo(WeatherForecast tempoParametro)
        {
            Console.WriteLine("Atualizar");
        }

        [HttpDelete]
        public void DeletarTempo(WeatherForecast tempoParametro)
        {
            Console.WriteLine("Deletar");
        }

    }
}