# Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /build

ADD . /build

RUN dotnet restore main/JacksonVeroneze.TemplateWebApi.Api/JacksonVeroneze.TemplateWebApi.Api.csproj
RUN dotnet publish main/JacksonVeroneze.TemplateWebApi.Api/JacksonVeroneze.TemplateWebApi.Api.csproj \
      -c Release -r linux-x64 -p:PublishReadyToRun=true --self-contained -o out

# Run
FROM jacksonveroneze/aspnet:7.0-ubuntu

COPY --from=build-env /build/out $APP_DIR

ADD ./docker-entrypoint.sh $APP_DIR/entrypoint.sh

ENTRYPOINT ["/bin/sh", "entrypoint.sh"]