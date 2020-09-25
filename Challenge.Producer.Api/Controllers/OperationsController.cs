using Challenge.Producer.Api.Dto;
using Challenge.Producer.Api.Services;
using Challenge.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Challenge.Producer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationsController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        /// <summary>
        /// Criar uma operação
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/operation
        ///     {
        ///        "value": 1000,
        ///        "operationType": 1
        ///     }
        ///
        /// </remarks>        
        /// <param name="operationDto"></param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] OperationDto operationDto)
        {
            var @event = new OperationCreatedEvent(operationDto.Value, operationDto.OperationType);
            await _operationService.SendOperation(@event);

            return Ok();
        }
    }
}
