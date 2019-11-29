using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Tests.Cosmos
{
    public class Repository
    {
        private readonly Settings _settings;
        private readonly Lazy<DocumentClient> _client;

        public Repository(Settings settings)
        {
            _settings = settings;

            _client = new Lazy<DocumentClient>(() =>
            {
                var client = new DocumentClient(new Uri(_settings.Endpoint), _settings.Key);
                PrepareDatabase(client).ConfigureAwait(false).GetAwaiter().GetResult();
                return client;
            });
        }

        private async Task<string> PrepareDatabase(DocumentClient client)
        {
            try
            {
                var response = await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_settings.DatabaseId)).ConfigureAwait(false);
                return _settings.DatabaseId;
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    Trace.WriteLine($"Database {_settings.DatabaseId} doesn't exist!");
                }

                throw;
            }
        }

        public async Task ReadDatabase()
        {
            await _client.Value.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_settings.DatabaseId));
        }
    }
}
