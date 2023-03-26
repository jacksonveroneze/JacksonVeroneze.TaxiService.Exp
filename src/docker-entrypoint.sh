#!/bin/sh

APP=JacksonVeroneze.Ged.Api.dll
OS=$(cat /etc/*-release | egrep "PRETTY_NAME" | cut -d = -f 2 | tr -d '"')

echo "- System Operation: $OS"
echo "- Run xray-daemon /usr/bin/xray"
/usr/bin/xray &

echo - Run application $APP
echo ""
dotnet $APP