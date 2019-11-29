# Reproducing a thread hang when CosmosDB is used by several tests in parallel

## Setup

### 1. Create new environment variables. For testing purposed, I have used `West Europe` as a region

- - "CosmosdbHang-endpoint"
- - "CosmosdbHang-key"
- - "CosmosdbHang-databaseId"
- - "CosmosdbHang-containerId"

## 2. Repro

1. Start single test, for example `A._1`.
2. It should finish successfully
3. Run all tests
4. Tests should start running in parallel. 
5. Each of them should finish in few seconds.
6. Unfortunately, they hang during the startup and none of the tests finishes.
7. Stop the tests execution, by force if needed
8. Disable parallel execution by opening `xunit.runner.json` and changing `parallelizeTestCollections` to `false`
9. Run all tests again. They should all be successful. 

