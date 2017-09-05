#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/Collectively.Tools.RedisSync
dotnet run --no-restore