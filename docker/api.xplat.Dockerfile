FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine as build

RUN apk update && apk upgrade
RUN apk add clang build-base zlib-dev --no-cache

COPY ../src /src

WORKDIR /src/api

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine as runtime

RUN apk update && apk upgrade
RUN apk add icu-libs fontconfig ttf-dejavu --no-cache

COPY --from=build /app /app
