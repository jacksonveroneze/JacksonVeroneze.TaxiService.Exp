# Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /build

ADD . /build

RUN dotnet restore main/JacksonVeroneze.TemplateWebApi.Api/JacksonVeroneze.TemplateWebApi.Api.csproj
RUN dotnet publish main/JacksonVeroneze.TemplateWebApi.Api/JacksonVeroneze.TemplateWebApi.Api.csproj \
    -c Release -r linux-x64 -p:PublishReadyToRun=true --self-contained -o out

# Run
FROM jacksonveroneze/aspnet:7.0-debian-1.0.8

COPY --from=build-env /build/out $APP_DIR

ENV APP_NAME JacksonVeroneze.TemplateWebApi.Api.dll

# Publish your application.
#COPY INSERT_NAME_OF_APP_TO_BE_PUBLISHED /app

USER root

# Install the agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y newrelic-dotnet-agent \
&& rm -rf /var/lib/apt/lists/*

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=96dba141b89d7d9c4f035b1f9058f231d5c1NRAL \
NEW_RELIC_APP_NAME=JacksonVeroneze.TemplateWebApi.Api

USER app