#!/usr/bin/env fish
dotnet build $argv ConfigurationManager.Patcher/ConfigurationManager.Patcher.csproj
dotnet build $argv ConfigurationManager/ConfigurationManager.csproj
