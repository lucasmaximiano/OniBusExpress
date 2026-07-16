using Microsoft.AspNetCore.Mvc;

namespace OnibusExpress.Api.Controllers
{
    [ApiController]
    [Route("api/passageiros")]
    [Produces("application/json")]
    public class PassageirosController : ControllerBase
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
        public IActionResult ObterTodos()
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
