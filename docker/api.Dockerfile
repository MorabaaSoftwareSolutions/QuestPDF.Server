FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy as build

RUN apt update
RUN apt install clang zlib1g-dev -y

COPY ../src /src

WORKDIR /src/api

RUN dotnet add package SkiaSharp.NativeAssets.Linux
RUN dotnet add package SkiaSharp.NativeAssets.Linux.NoDependencies
RUN dotnet publish -r linux-musl-x64 -c Release -o /app

FROM alpine:3.14 as runtime

COPY --from=build /app /app
