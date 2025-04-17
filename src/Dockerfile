# =====================================================
# Etapa 1: Build
# =====================================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore ./main/Api/TaxiService.Api.csproj \
    --runtime linux-x64 \
    /p:PublishReadyToRun=true

# =====================================================
# Etapa 2: Publicação
# =====================================================
FROM build AS publish

RUN dotnet publish ./main/Api/TaxiService.Api.csproj \
    --configuration Release \
    --output /publish \
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
    /p:PublishCrossgen2=true \
    /p:PublishContainer

# =====================================================
# Etapa Final: Imagem de Runtime
# =====================================================
FROM jacksonveroneze/aspnet:9.0-debian-1.4.1 AS runtime

ENV CORECLR_ENABLE_PROFILING=0
ENV ASSEMBLY_NAME=TaxiService.Api.dll

HEALTHCHECK --interval=10s --timeout=3s --start-period=10s --retries=3 \
  CMD curl --silent --fail http://localhost:8080/health?source=docker-inner || exit 1

COPY --from=publish /publish .