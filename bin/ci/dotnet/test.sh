#!/bin/bash

set -euo pipefail

dotnet test --no-build --verbosity normal

exit 0
