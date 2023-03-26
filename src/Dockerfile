# Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /build

ADD . /build

RUN dotnet restore main/JacksonVeroneze.Ged.Api/JacksonVeroneze.Ged.Api.csproj
RUN dotnet publish main/JacksonVeroneze.Ged.Api/JacksonVeroneze.Ged.Api.csproj \
      -c Release -r linux-x64 -p:PublishReadyToRun=true -p:PublishTrimmed=true --self-contained -o out

# Run
FROM jacksonveroneze/aspnet:7.0-ubuntu-xray

COPY --from=build-env /build/out $APP_DIR

ADD ./docker-entrypoint.sh $APP_DIR/entrypoint.sh
#ADD ./sampling-rules.json $APP_DIR/sampling-rules.json

ENTRYPOINT ["/bin/sh", "entrypoint.sh"]