using Challenge.Domain.Dto;
using System.Threading.Tasks;

namespace Challenge.Domain.Interfaces
{
    public interface IOperationRepository
    {
        Task Add(Operation operation);
        Task<ContaCorrenteDto> List();
    }
}
