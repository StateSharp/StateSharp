#!/bin/bash

set -euo pipefail

dotnet test --collect:"XPlat Code Coverage" --settings:coverlet.runsettings
wget https://github.com/codecov/codecov-bash/releases/download/1.0.5/codecov

find ./tests/UnitTests/TestResults/

bash codecov -f $(find ./tests/UnitTests/TestResults/)

exit 0
