#!/bin/sh
set -e

WORKDIR=/tmp/run
mkdir -p $WORKDIR
cd $WORKDIR

cat > Program.cs

dotnet new console -n App --no-restore >/dev/null 2>&1
mv Program.cs App/Program.cs

cd App

if ! dotnet build -c Release -nologo -clp:ErrorsOnly -clp:NoSummary; then
  echo "__COMPILE_ERROR__"
  exit 1
fi

dotnet run -c Release --no-build