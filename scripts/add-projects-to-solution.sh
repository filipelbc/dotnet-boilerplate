#!/bin/bash

find . -name '*.csproj' | xargs dotnet sln add
