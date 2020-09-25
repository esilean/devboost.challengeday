using Challenge.Shared;
using System.Threading.Tasks;

namespace Challenge.Producer.Api.Services
{
    public interface IOperationService
    {
        Task SendOperation(OperationCreatedEvent @event);
    }
}
