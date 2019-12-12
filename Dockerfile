FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY Tests ./Tests
COPY *.sln ./

RUN dotnet build

ENTRYPOINT ["dotnet", "test"]