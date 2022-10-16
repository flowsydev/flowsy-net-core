#!/usr/bin/env bash

DEF_BUILD_CONFIG=Release
DEF_PACKAGE_SOURCE="https://api.nuget.org/v3/index.json"

read -r -p "Configuration ($DEF_BUILD_CONFIG): " BUILD_CONFIG
read -r -p "Package Source ($DEF_PACKAGE_SOURCE): " PACKAGE_SOURCE
read -r -s -p "API KEY: " API_KEY

echo
echo

[[ -z "$API_KEY" ]] && echo "API Key is mandatory" && exit 1

BUILD_CONFIG=${BUILD_CONFIG:-"$DEF_BUILD_CONFIG"}
PACKAGE_SOURCE=${PACKAGE_SOURCE:-"$DEF_PACKAGE_SOURCE"}

PROJECT_FILE=$(find . -type f -iname "*.csproj" | grep -v ".Test.")
PROJECT_DIR=$(dirname "$PROJECT_FILE")

PACKAGE_DIR="$PROJECT_DIR/bin/$BUILD_CONFIG"
PACKAGE_VERSION=$(grep -oE '<version>(.+)</version>' "$PROJECT_DIR/.nuspec" | sed -nr 's/<version>(.+)<\/version>/\1/p')

{ dotnet clean --configuration "$BUILD_CONFIG" && \
  dotnet pack "$PROJECT_FILE" --configuration "$BUILD_CONFIG" --include-symbols; } \
  || { echo "Could not create packages" && exit 1; }

echo

nuget push "$PACKAGE_DIR/*.$PACKAGE_VERSION.nupkg" -ApiKey "$API_KEY" -Source "$PACKAGE_SOURCE"
