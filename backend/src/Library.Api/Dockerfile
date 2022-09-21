FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Library.Api/Library.Api.csproj", "src/Library.Api/"]
COPY ["src/Library.Repository/Library.Repository.csproj", "src/Library.Repository/"]
COPY ["src/Library.Core/Library.Core.csproj", "src/Library.Core/"]
COPY ["src/Library.IoC/Library.IoC.csproj", "src/Library.IoC/"]
RUN dotnet restore "src/Library.Api/Library.Api.csproj"
COPY . .
WORKDIR "/src/src/Library.Api"
RUN dotnet build "Library.Api.csproj" -c Release -o /app/build

# Unit Test
FROM build AS unittest
WORKDIR /backend/tests/Library.UnitTests
CMD ["dotnet", "test"]

# Integration Test
FROM build AS integrationtest
WORKDIR /backend/tests/Library.IntegrationTests
CMD ["dotnet", "test"]

FROM build AS publish
RUN dotnet publish "Library.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library.Api.dll"]