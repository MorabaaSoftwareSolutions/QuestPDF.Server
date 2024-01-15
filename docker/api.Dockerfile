FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine as build

RUN apk add clang build-base zlib-dev --no-cache

COPY ../src /src

WORKDIR /src/api

RUN dotnet add package SkiaSharp.NativeAssets.Linux --version 2.88.7
RUN dotnet add package SkiaSharp.NativeAssets.Linux.NoDependencies --version 2.88.7
RUN dotnet add package HarfBuzzSharp.NativeAssets.Linux --version 2.88.7
RUN dotnet publish -r linux-musl-x64 -c Release -o /app

FROM alpine:3.14 as runtime

COPY --from=build /app /app
