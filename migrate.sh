#!/usr/bin/env zsh

dotnet ef migrations add Initialize \
  -s Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator/Demo.TransactionalOutbox.Infrastructure.Postgres.Migrator.csproj \
  -p Demo.TransactionalOutbox.Infrastructure.Postgres/Demo.TransactionalOutbox.Infrastructure.Postgres.csproj -o Migrations  
