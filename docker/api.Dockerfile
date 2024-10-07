FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

RUN apk update && apk upgrade
RUN apk add clang build-base zlib-dev --no-cache

COPY ../src /src

WORKDIR /src/api

RUN dotnet publish -r linux-musl-x64 -c Release -o /app

FROM alpine:latest AS runtime

RUN apk update && apk upgrade
RUN apk add icu-libs fontconfig ttf-dejavu --no-cache

COPY --from=build /app /app
