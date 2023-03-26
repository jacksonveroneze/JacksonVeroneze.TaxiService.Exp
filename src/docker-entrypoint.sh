#!/bin/sh

APP=JacksonVeroneze.TemplateWebApi.Api.dll
OS=$(cat /etc/*-release | egrep "PRETTY_NAME" | cut -d = -f 2 | tr -d '"')

echo "- System Operation: $OS"
echo "- Run application: $APP"
echo "- EnvironmentName: $ASPNETCORE_ENVIRONMENT"
echo ""
dotnet $APP