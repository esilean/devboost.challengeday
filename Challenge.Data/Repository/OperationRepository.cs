using Challenge.Data.DbContext;
using Challenge.Domain;
using Challenge.Domain.Dto;
using Challenge.Domain.Interfaces;
using Challenge.Shared.Enum;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Data.Repository
{
    public class OperationRepository : IOperationRepository
    {

        private readonly MongoDbContext _dbContext;
        public OperationRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Operation> Add(Operation operation)
        {
            try
            {

                await _dbContext.Operation.InsertOneAsync(operation);
                return operation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ContaCorrenteDto> List()
        {
            var allOperations = await _dbContext.Operation.FindAsync(Builders<Operation>.Filter.Empty);

            var totalDepositos = allOperations.ToList().Where(o => o.OperationType == OperationType.Deposito).Sum(d => d.Value);
            var totalSaque = allOperations.ToList().Where(o => o.OperationType == OperationType.Saque).Sum(d => d.Value);

            return new ContaCorrenteDto { Saldo = (decimal)totalDepositos - (decimal)totalSaque };

        }
    }
}
