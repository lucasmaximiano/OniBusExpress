using Microsoft.AspNetCore.Mvc;
using OnibusExpress.Application.DTOs.Reserva;
using OnibusExpress.Application.Interfaces;

namespace OnibusExpress.Api.Controllers
{
    [ApiController]
    [Route("api/reservas")]
    [Produces("application/json")]
    public class ReservasController(IReservasApplication application) : ControllerBase
    {
        private readonly IReservasApplication _application = application;

        [HttpPost]
        [ProducesResponseType(typeof(ReservaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarAsync(
            [FromBody] CreateReservaRequest request,
            CancellationToken cancellationToken)
        {
            await _application.CriarAsync(
                request,
                cancellationToken);

            return Created();
        }

        [HttpGet("{codigoReserva}")]
        [ProducesResponseType(typeof(ReservaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorCodigoAsync(
            string codigoReserva,
            CancellationToken cancellationToken)
        {
            ReservaResponse? response = await _application.ObterPorCodigoAsync(
                codigoReserva,
                cancellationToken);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReservaResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodas(
            CancellationToken cancellationToken)
        {
            IEnumerable<ReservaResponse> response = await _application.ObterTodasAsync(
                cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{codigoReserva}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelarAsync(
           string codigoReserva,
           CancellationToken cancellationToken)
        {
            await _application.CancelarAsync(
                codigoReserva,
                cancellationToken);

            return NoContent();
        }
    }
}
