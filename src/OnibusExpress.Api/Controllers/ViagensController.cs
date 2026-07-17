using Microsoft.AspNetCore.Mvc;
using OnibusExpress.Application.DTOs.Viagens;
using OnibusExpress.Application.Interfaces;

namespace OnibusExpress.Api.Controllers
{
    [ApiController]
    [Route("api/viagens")]
    [Produces("application/json")]
    public class ViagensController(IViagensApplication application) : ControllerBase
    {
        private readonly IViagensApplication _application = application;

        [HttpPost]
        [ProducesResponseType(typeof(ViagemResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarAsync(
            [FromBody] CreateViagemRequest request,
            CancellationToken cancellationToken)
        {
            await _application.CriarAsync(
                request,
                cancellationToken);

            return Created();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ViagemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(
            Guid id,
            CancellationToken cancellationToken)
        {
            ViagemResponse? response = await _application.ObterPorIdAsync(
                id,
                cancellationToken);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("filtro")]
        [ProducesResponseType(typeof(IEnumerable<ViagemResponse>),StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPorFiltro(
            [FromQuery] string origem,
            [FromQuery] string destino,
            CancellationToken cancellationToken)
        {
            ViagemResponse? response = await _application.ObterPorFiltro(
                origem,
                destino,
                cancellationToken);

            return Ok(response);
        }
    }
}
