FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

COPY ../src /src

WORKDIR /src/api

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime

COPY --from=build /app /app

