using Microsoft.AspNetCore.Mvc;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.Interfaces;

namespace OnibusExpress.Api.Controllers
{
    [ApiController]
    [Route("api/rotas")]
    [Produces("application/json")]
    public sealed class RotasController(IRotasApplication application) : ControllerBase
    {
        private readonly IRotasApplication _application = application;

        [HttpPost]
        [ProducesResponseType(typeof(RotaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarAsync(
            [FromBody] CreateRotaRequest request,
            CancellationToken cancellationToken)
        {
            await _application.CriarAsync(
                request,
                cancellationToken);

            return Created();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RotaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorIdAsync(
         Guid id,
         CancellationToken cancellationToken)
        {
            RotaResponse? response = await _application.ObterPorIdAsync(
                id,
                cancellationToken);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RotaResponse>),StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodasAsync(
            CancellationToken cancellationToken)
        {
            IEnumerable<RotaResponse> response = await _application.ObterTodasAsync(
                cancellationToken);

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(RotaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarAsync(
            Guid id,
            [FromBody] UpdateRotaRequest request,
            CancellationToken cancellationToken)
        {
            RotaResponse response = await _application.AtualizarAsync(
                id,
                request,
                cancellationToken);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _application.ExcluirAsync(
                 id,
                 cancellationToken);

            return NoContent();
        }
    }
}