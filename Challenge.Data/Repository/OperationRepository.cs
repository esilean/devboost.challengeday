using Challenge.Data.DbContext;
using Challenge.Domain;
using Challenge.Domain.Dto;
using Challenge.Domain.Interfaces;
using Challenge.Shared.Enum;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Data.Repository
{
    public class OperationRepository : IOperationRepository
    {

        private readonly MongoDbContext _context;
        public OperationRepository(MongoDbContext context)
        {
            _context = context;
        }
        public async Task Add(Operation operation)
        {
            await _context.Operation.InsertOneAsync(operation);
        }

        public async Task<ContaCorrenteDto> List()
        {
            var allOperations = await _context.Operation.FindAsync(Builders<Operation>.Filter.Empty);

            var totalDepositos = allOperations.ToList().Where(o => o.OperationType == OperationType.Deposito).Sum(d => d.Value);
            var totalSaque = allOperations.ToList().Where(o => o.OperationType == OperationType.Saque).Sum(d => d.Value);

            return new ContaCorrenteDto { Saldo = (decimal)totalDepositos - (decimal)totalSaque };

        }
    }
}
