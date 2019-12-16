# A repro for issue https://github.com/Azure/azure-cosmos-dotnet-v3/issues/1067 . It was based on the CosmosDB SDK v3 3.4.1

## Setup

### 1. Create new environment variables. For testing purposed, I have used `West Europe` as a region

- - "CosmosdbHang-endpoint"
- - "CosmosdbHang-key"
- - "CosmosdbHang-databaseId"

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
10. Checkout branch which has the same code logic, but SDK V2. It's called `sdkV2`.
11. Run all tests in parallel for SDK V2. They should all be successful.

## 3. Repro with Docker
1. Create empty file called `.env` in the root of the repository
2. Add credentials to the database to the new file. It should looks like:

```txt
CosmosdbHang-endpoint=XXX
CosmosdbHang-key=YYY
CosmosdbHang-databaseId=ZZZ
```

3. Call `./build.sh`. It will build a docker image
4. Call `./run.sh`, which will start docker container. It has a `docker test` set as an entry point, so the test runner should start intermediately.
5. Test execution should hang. Wait max 60 second. You shoudn't see any progress
6. Disable parallel execution by opening `xunit.runner.json` and changing `parallelizeTestCollections` to `false`
7. Repeat step 3 and 4
8. Tests should finish successfully in approximately 25 seconds.