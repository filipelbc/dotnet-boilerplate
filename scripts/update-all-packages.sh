#!/bin/bash

for p in `find . -name '*.csproj'`
do
    grep -o '".*" Version' $p \
        | sed 's/"\|" Version//g' \
        | xargs -n1 dotnet add $p package
done
