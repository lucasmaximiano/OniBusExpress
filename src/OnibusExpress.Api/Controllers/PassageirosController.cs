using Microsoft.AspNetCore.Mvc;
using OnibusExpress.Application.DTOs.Passageiros;
using OnibusExpress.Application.Interfaces;

namespace OnibusExpress.Api.Controllers
{
    [ApiController]
    [Route("api/passageiros")]
    [Produces("application/json")]
    public class PassageirosController(IPassageirosApplication application) : ControllerBase
    {
        private readonly IPassageirosApplication _application = application;

        [HttpPost]
        [ProducesResponseType(typeof(PassageiroResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarAsync(
            [FromBody] CreatePassageiroRequest request,
            CancellationToken cancellationToken)
        {
            await _application.CriarAsync(
                    request,
                    cancellationToken);

            return Created();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PassageiroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            PassageiroResponse? response = await _application.ObterPorIdAsync(
                id,
                cancellationToken);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(PassageiroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarAsync(
          Guid id,
          [FromBody] UpdatePassageiroRequest request,
          CancellationToken cancellationToken)
        {
            PassageiroResponse? response = await _application.AtualizarAsync(
                id,
                request,
                cancellationToken);

            if (response is null)
                return NotFound();

            return Ok(response);
        }
    }
}
