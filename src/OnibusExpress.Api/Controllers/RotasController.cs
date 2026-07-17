using Microsoft.AspNetCore.Mvc;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.Interfaces;

namespace OnibusExpress.Api.Controllers
{
    [ApiController]
    [Route("api/rotas")]
    [Produces("application/json")]
    public class RotasController(IRotasApplication application) : ControllerBase
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
    }
}