#!/bin/bash

# Farmland Drops Soil Build Script
# Usage: ./build.sh [version]
# Example: ./build.sh 1.22.0

VERSION=${1:-1.22.0}
GAME_VERSION="1.22.0"
OUTPUT_NAME="FarmlandDropsSoil-${VERSION}"

echo "Building Farmland Drops Soil v${VERSION} for VS ${GAME_VERSION}..."

# Clean previous builds
rm -rf bin/ obj/
rm -f ${OUTPUT_NAME}.zip

# Build the project
echo "Compiling..."
dotnet build -c Release

if [ $? -ne 0 ]; then
    echo "Build failed!"
    exit 1
fi

# Create the zip
echo "Packaging..."
zip -j ${OUTPUT_NAME}.zip \
  modinfo.json \
  bin/Release/net10.0/FarmlandDropsSoil.dll \
  bin/Release/net10.0/FarmlandDropsSoil.pdb

zip -r ${OUTPUT_NAME}.zip assets/

echo "Done! Created ${OUTPUT_NAME}.zip"
echo "To install, copy to ~/VintagestoryData/Mods/"