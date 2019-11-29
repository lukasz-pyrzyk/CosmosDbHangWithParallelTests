using System;
using Tests.Cosmos;

namespace Tests
{
    public abstract class BaseIntegrationTests
    {
        protected readonly Repository Repository;

        protected BaseIntegrationTests()
        {
            var settings = new Settings();
            settings.Endpoint = Environment.GetEnvironmentVariable("CosmosdbHang-endpoint");
            settings.Key = Environment.GetEnvironmentVariable("CosmosdbHang-key");
            settings.DatabaseId = Environment.GetEnvironmentVariable("CosmosdbHang-databaseId");

            Repository = new Repository(settings);
        }
    }
}
