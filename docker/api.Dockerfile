FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine as build

RUN apk add clang build-base zlib-dev --no-cache

COPY ../src /src

WORKDIR /src/api

RUN dotnet publish -r linux-musl-x64 -c Release -o /app

FROM alpine:3.14 as runtime

COPY --from=build /app /app
