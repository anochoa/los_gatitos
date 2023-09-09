using cat.infra;
using cat.infra.modelo;
using Microsoft.AspNetCore.Mvc;

namespace cat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SaveTheCatController : ControllerBase
    {
        private readonly RepositorioDados _repositorioDados;

        public SaveTheCatController(RepositorioDados repositorioDados)
        {
            _repositorioDados = repositorioDados;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repositorioDados.GetCats(new FiltroGatinho());

            return Ok(result);                  
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveTheCat([FromBody] Gatinho gatinho)
        {
            var result = await _repositorioDados.SaveCat(gatinho);
            return Ok(result);
        }
        [HttpPost("SaveLosGatos")]        
        public async Task<IActionResult> SaveLosGatos([FromBody] List<Gatinho> gatinhos)
        {
            foreach (var gato in gatinhos)
            {
               await _repositorioDados.SaveCat(gato);
            }
            return Ok(true);
        }

    }
}
