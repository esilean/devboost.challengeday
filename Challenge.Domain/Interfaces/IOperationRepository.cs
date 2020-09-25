using Challenge.Domain.Dto;
using System.Threading.Tasks;

namespace Challenge.Domain.Interfaces
{
    public interface IOperationRepository
    {
        Task<Operation> Add(Operation operation);
        Task<ContaCorrenteDto> List();
    }
}
