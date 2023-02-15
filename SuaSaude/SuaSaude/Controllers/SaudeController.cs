using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuaSaude.Service.Dto;
using SuaSaude.Service.Interface;

namespace SuaSaude.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaudeController : ControllerBase
    {
        private IControleDePesoService _controleDePesoService;
        public SaudeController(IControleDePesoService controleDePesoService)
        {
            _controleDePesoService = controleDePesoService;
        }

        [HttpGet("IMC")]
        [Authorize(Roles = "admin")]
        public ActionResult<double> GetIMC(double peso, double altura)
        {
            var teste = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "teste");
            double imc = _controleDePesoService.CalcularIMC(peso, altura);
            return Ok(imc);
        }

        [HttpGet("Classificacao")]
        [Authorize]
        public ActionResult<string> DefineIMC(double imc)
        {
            string classificacao = _controleDePesoService.ClassificarIMC(imc);
            return Ok(classificacao);
        }

        [HttpGet("Categorizacao")]
        [Authorize]
        public ActionResult<InformacoesIMCDto> CategorizaIMC(double peso, double altura)
        {
            InformacoesIMCDto informacoes = _controleDePesoService.CategorizarIMC(peso, altura);
            return Ok(informacoes);
        }
    }
}