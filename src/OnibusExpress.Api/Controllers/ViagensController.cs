using Microsoft.AspNetCore.Mvc;

namespace OnibusExpress.Api.Controllers
{
    [ApiController]
    [Route("api/viagens")]
    [Produces("application/json")]
    public class ViagensController : ControllerBase
    {
        [HttpPost]
        public IActionResult Criar()
        {
            return Ok();
        }

        [HttpPut("{id:guid}")]
        public IActionResult Atualizar(Guid id)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult ObterTodas()
        {
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public IActionResult ObterPorId(Guid id)
        {
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            return NoContent();
        }
    }
}
