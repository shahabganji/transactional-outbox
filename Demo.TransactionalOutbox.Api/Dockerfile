FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Demo.TransactionalOutbox.Api/Demo.TransactionalOutbox.Api.csproj", "Demo.TransactionalOutbox.Api/"]
RUN dotnet restore "Demo.TransactionalOutbox.Api/Demo.TransactionalOutbox.Api.csproj"
COPY . .
WORKDIR "/src/Demo.TransactionalOutbox.Api"
RUN dotnet build "Demo.TransactionalOutbox.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.TransactionalOutbox.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.TransactionalOutbox.Api.dll"]
