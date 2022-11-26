FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Demo.TransactionalOutbox.MessageRelay/Demo.TransactionalOutbox.MessageRelay.csproj", "Demo.TransactionalOutbox.MessageRelay/"]
RUN dotnet restore "Demo.TransactionalOutbox.MessageRelay/Demo.TransactionalOutbox.MessageRelay.csproj"
COPY . .
WORKDIR "/src/Demo.TransactionalOutbox.MessageRelay"
RUN dotnet build "Demo.TransactionalOutbox.MessageRelay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.TransactionalOutbox.MessageRelay.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.TransactionalOutbox.MessageRelay.dll"]
