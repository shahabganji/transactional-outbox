FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Demo.TransactionalOutbox.Application/Demo.TransactionalOutbox.Application.csproj", "Demo.TransactionalOutbox.Application/"]
RUN dotnet restore "Demo.TransactionalOutbox.Application/Demo.TransactionalOutbox.Application.csproj"
COPY . .
WORKDIR "/src/Demo.TransactionalOutbox.Application"
RUN dotnet build "Demo.TransactionalOutbox.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.TransactionalOutbox.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.TransactionalOutbox.Application.dll"]
