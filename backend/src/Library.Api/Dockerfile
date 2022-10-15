FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Library.Api/Library.Api.csproj", "src/Library.Api/"]
COPY ["src/Library.IoC/Library.IoC.csproj", "src/Library.IoC/"]
COPY ["src/Library.Application/Library.Application.csproj", "src/Library.Application/"]
COPY ["src/Library.Domain/Library.Domain.csproj", "src/Library.Domain/"]
COPY ["src/Library.Infra.ResponseNotifier/Library.Infra.ResponseNotifier.csproj", "src/Library.Infra.ResponseNotifier/"]
COPY ["src/Library.Repository/Library.Repository.csproj", "src/Library.Repository/"]
RUN dotnet restore "src/Library.Api/Library.Api.csproj"
COPY . .
WORKDIR "/src/src/Library.Api"
RUN dotnet build "Library.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Library.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Unit Test
FROM build AS unittest
WORKDIR "../../tests/Library.UnitTests"

# Integration Test
FROM build AS integrationtest
WORKDIR "../../tests/Library.UnitTests"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library.Api.dll"]