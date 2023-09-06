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
            return Ok(await _repositorioDados.PegarGatos(new CatFilter()));

        }

        [HttpPost]
        public async Task<IActionResult> SalvarGato([FromBody] Gatinho gatista)
        {
            return Ok(await _repositorioDados.SalvarGato(gatista));
        }

        [HttpPost(Name = "SaveLosGatos")]
        public async Task<IActionResult> SaveLosGatos([FromBody] List<Gatinho> animal)
        {
            foreach (var item in animal)
                await _repositorioDados.SalvarGato(item);

            return Ok(true);
        }

    }
}
