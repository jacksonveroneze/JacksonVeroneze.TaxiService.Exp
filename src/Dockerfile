# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env

ADD . /build

RUN dotnet restore /build/main/Web/DocSignService.Web.csproj \
    --runtime linux-x64 \
    /p:PublishReadyToRun=true

RUN dotnet publish /build/main/Api/TaxiService.Api.csproj \
    --configuration Release \
    --output /build/out \
    --no-restore \
    --self-contained \
    --runtime linux-x64 \
    /p:PublishReadyToRun=true \
    /p:PublishTrimmed=true \
    /p:TieredCompilation=true \
    /p:DebugType=None \
    /p:DebugSymbols=false \
    /p:PublishSingleFile=false \
    /p:EnableCompressionInSingleFile=false \
    /p:IncludeAllLocalesForSelfContained=false \
    /p:Optimize=true \
    /p:PublishCrossgen2=true

FROM jacksonveroneze/aspnet:9.0-debian-latest

COPY --from=build-env /build/out /home/app
#
#USER root
#
## Install the agent
#RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
#&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
#&& wget https://download.newrelic.com/548C16BF.gpg \
#&& apt-key add 548C16BF.gpg \
#&& apt-get update \
#&& apt-get install -y 'newrelic-dotnet-agent' \
#&& rm -rf /var/lib/apt/lists/*
#
## Enable the agent
#ENV CORECLR_ENABLE_PROFILING=1 \
#CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
#CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
#CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
#NEW_RELIC_LICENSE_KEY=255ee02dabd7178f8c020548fc4dc930FFFFNRAL \
#NEW_RELIC_APP_NAME="TaxiService"