using Challenge.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Challenge.Data.DbContext
{
    public class MongoDbContext
    {

        private readonly IMongoDatabase _operationDatabase;

        public MongoDbContext(IOptions<DbConfig> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _operationDatabase = mongoClient.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Operation> Operation => _operationDatabase.GetCollection<Operation>("Operation");

    }
}
