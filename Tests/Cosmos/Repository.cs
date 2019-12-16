using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Tests.Cosmos
{
    public class Repository
    {
        private readonly Settings _settings;
        private readonly Lazy<Database> _clientFactory;
        private readonly CosmosClient _cosmosClient;

        public Repository(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            var options = new CosmosClientOptions
            {
                ConnectionMode = ConnectionMode.Gateway // it's default in SDK v2
            };
            _cosmosClient = new CosmosClient(settings.Endpoint, settings.Key, options);
            _clientFactory = new Lazy<Database>(() =>
            {
                var database = CheckIfDatabaseExists(settings).ConfigureAwait(false).GetAwaiter().GetResult();

                return database;
            });
        }

        private async Task<Database> CheckIfDatabaseExists(Settings settings)
        {
            var database = _cosmosClient.GetDatabase(settings.DatabaseId);
            var read = await database.ReadStreamAsync().ConfigureAwait(false);
            if (read.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception($"CosmosDB database {settings.DatabaseId} does not exist!");
            }

            return database;
        }

        public async Task ReadDatabase()
        {
            await _clientFactory.Value.ReadAsync();
        }
    }
}
