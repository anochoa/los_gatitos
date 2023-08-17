using cat.infra;
using cat.infra.modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SaveTheCatController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var gatos = new List<Gatinho>();

            return Ok(new repositorio_de_dados().GetCATS(new CatFilter()));         
            
        }
        
        [HttpPost]
        public IActionResult SaveTheCat([FromBody] Gatinho gatista)
        {
            return Ok(new repositorio_de_dados().SaveTheCat(gatista));
        }
        [HttpPost(Name = "SaveLosGatos")]        
        public IActionResult SaveLosGatos([FromBody] Gatinho[] animal)
        {
            var repo = new repositorio_de_dados();
            foreach (var item in animal)
            {
                repo.SaveTheCat(item);
                repo.conn = "";
            }
            return Ok(true);
        }

    }
}
