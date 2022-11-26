FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator/Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator.csproj", "Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator/"]
RUN dotnet restore "Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator/Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator.csproj"
COPY . .
WORKDIR "/src/Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator"
RUN dotnet build "Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator.dll"]
