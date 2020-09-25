using Challenge.Application.Dto;
using Challenge.Application.Operations.Interfaces;
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


        [HttpPost]
        public async Task Create(OperationDto operationDto)
        {
            await _operationCreate.Handle(operationDto);
        }
    }
}
