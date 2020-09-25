using Challenge.Application.Dto;
using System.Threading.Tasks;

namespace Challenge.Application.Operations.Interfaces
{
    public interface IOperationCreate
    {
        Task Handle(OperationDto operationDto);
    }
}
