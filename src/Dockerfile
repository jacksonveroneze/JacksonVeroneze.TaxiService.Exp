# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

ADD . /build

RUN dotnet restore /build/main/JacksonVeroneze.TemplateWebApi.Api/JacksonVeroneze.TemplateWebApi.Api.csproj

RUN dotnet publish /build/main/JacksonVeroneze.TemplateWebApi.Api/JacksonVeroneze.TemplateWebApi.Api.csproj \
    --configuration Release \
    --output /build/out \
    --no-restore

FROM jacksonveroneze/aspnet:8.0-debian-1.1.0

COPY --from=build-env /build/out /home/app

COPY --from=pyroscope/pyroscope-dotnet:0.8.14-glibc /Pyroscope.Profiler.Native.so /Pyroscope.Profiler.Native.so
COPY --from=pyroscope/pyroscope-dotnet:0.8.14-glibc /Pyroscope.Linux.ApiWrapper.x64.so /Pyroscope.Linux.ApiWrapper.x64.so

ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={BD1A650D-AC5D-4896-B64F-D6FA25D6B26A}
ENV CORECLR_PROFILER_PATH=/Pyroscope.Profiler.Native.so
ENV LD_PRELOAD=/Pyroscope.Linux.ApiWrapper.x64.so

ENV PYROSCOPE_APPLICATION_NAME=JacksonVeroneze.TemplateWebApi.Api
ENV PYROSCOPE_SERVER_ADDRESS=http://pyroscope:4040
ENV PYROSCOPE_PROFILING_LOG_DIR=/home/app
ENV PYROSCOPE_LOG_LEVEL=info
ENV PYROSCOPE_PROFILING_ENABLED=1
ENV PYROSCOPE_PROFILING_ALLOCATION_ENABLED=true
ENV PYROSCOPE_PROFILING_CONTENTION_ENABLED=true
ENV PYROSCOPE_PROFILING_EXCEPTION_ENABLED=true
ENV PYROSCOPE_PROFILING_LOCK_ENABLED=true
ENV PYROSCOPE_PROFILING_CPU_ENABLED=true
