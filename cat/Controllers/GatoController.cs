using cat.infra.Interfaces;
using cat.infra.modelo;
using Microsoft.AspNetCore.Mvc;

namespace cat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GatoController : ControllerBase
    {
        private readonly IRepositorioDados _repositorioDados;

        public GatoController(IRepositorioDados repositorioDados)
        {
            _repositorioDados = repositorioDados;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Gatinho> resultado = await _repositorioDados.BuscarGato(new FiltroGatinho());

            return Ok(resultado);                  
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveTheCat([FromBody] Gatinho gatinho)
        {
            await _repositorioDados.SalvarGato(gatinho);
            return Ok();
        }

        [HttpPost(Name = "SaveLosGatos")]        
        public async Task<IActionResult> SaveLosGatos([FromBody] List<Gatinho> gatinhos)
        {
            await _repositorioDados.SalvarGato(gatinhos);
            return Ok();
        }

    }
}
