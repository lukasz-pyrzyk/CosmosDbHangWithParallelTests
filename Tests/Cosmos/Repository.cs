using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;

namespace Tests.Cosmos
{
    public class Repository
    {
        private readonly Settings _settings;
        private readonly DocumentClient _client;

        public Repository(Settings settings)
        {
            _settings = settings;

            _client = new DocumentClient(new Uri(_settings.Endpoint), _settings.Key);
        }

        public async Task ReadDatabase()
        {
            await _client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_settings.DatabaseId));
        }
    }
}
