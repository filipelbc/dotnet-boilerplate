#!/bin/bash

# Use this to add the initial migrations for the Identity Server, while not
# going to production.

set -ex

cd src/Boilerplate.IdentityServer

dotnet ef migrations add InitialMigration --context PersistedGrantDbContext --output-dir Migrations/PersistentGrantStore

dotnet ef migrations add InitialMigration --context ConfigurationDbContext --output-dir Migrations/ConfigurationStore

dotnet ef migrations add InitialMigration --context ApplicationDbContext --output-dir Migrations/IdentityStore
