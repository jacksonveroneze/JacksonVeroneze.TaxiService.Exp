# Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

ADD . /build

RUN dotnet restore /build/main/JacksonVeroneze.TemplateWebApi.Api/JacksonVeroneze.TemplateWebApi.Api.csproj \
    --runtime linux-x64 \
    /p:PublishReadyToRun=true

RUN dotnet publish /build/main/JacksonVeroneze.TemplateWebApi.Api/JacksonVeroneze.TemplateWebApi.Api.csproj \
    --configuration Release \
    --output /build/out \
    --no-restore \
    --runtime linux-x64 \
    --self-contained \
    /p:PublishTrimmed=true \
    /p:PublishReadyToRun=true \
    /p:PublishSingleFile=false

FROM jacksonveroneze/aspnet:7.0-debian-1.0.8

COPY --from=build-env /build/out /home/app

# Run
ENV APP_NAME JacksonVeroneze.TemplateWebApi.Api.dll
