using Challenge.Application.Dto;
using Challenge.Application.Operations.Interfaces;
using Challenge.Domain;
using Challenge.Domain.Interfaces;
using System.Threading.Tasks;

namespace Challenge.Application.Operations
{
    public class Create : IOperationCreate
    {
        private readonly IOperationRepository _operationRepository;

        public Create(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task Handle(OperationDto operationDto)
        {
            var operation = new Operation(operationDto.Id, operationDto.Value, operationDto.OperandType, operationDto.Created);
            await _operationRepository.Add(operation);
        }
    }
}
