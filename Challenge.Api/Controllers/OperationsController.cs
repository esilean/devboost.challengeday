using Challenge.Application.Dto;
using Challenge.Application.Operations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationCreate _operationCreate;

        public OperationsController(IOperationCreate operationCreate)
        {
            _operationCreate = operationCreate;
        }


        /// <summary>
        /// Criar uma operação
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/operations
        ///     {
        ///        "id": "53734bc7-c7c2-4583-83eb-82e4a8e194ec",
        ///        "value": 1000,
        ///        "operationType": 1,
        ///        "created": "2020-21-31"
        ///     }
        ///
        /// </remarks>        
        /// <param name="operationDto"></param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task Create(OperationDto operationDto)
        {
            await _operationCreate.Handle(operationDto);
        }
    }
}
