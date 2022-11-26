FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Demo.TransactionalOutbox.FancyConsumer/Demo.TransactionalOutbox.FancyConsumer.csproj", "Demo.TransactionalOutbox.FancyConsumer/"]
RUN dotnet restore "Demo.TransactionalOutbox.FancyConsumer/Demo.TransactionalOutbox.FancyConsumer.csproj"
COPY . .
WORKDIR "/src/Demo.TransactionalOutbox.FancyConsumer"
RUN dotnet build "Demo.TransactionalOutbox.FancyConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.TransactionalOutbox.FancyConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.TransactionalOutbox.FancyConsumer.dll"]
