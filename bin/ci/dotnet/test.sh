#!/bin/bash

set -euo pipefail

wget -O ./opencover.zip https://github.com/OpenCover/opencover/releases/download/4.7.1221/opencover.4.7.1221.zip
unzip opencover.zip -d ./bin/opencover
find ./bin/opencover

# dotnet test --no-build --verbosity normal

exit 0
