FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

COPY ../src /src

WORKDIR /src/api

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

COPY --from=build /app /app
