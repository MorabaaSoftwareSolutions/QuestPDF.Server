FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

COPY ../src /src

WORKDIR /src/api

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

RUN apt update && apt upgrade
RUN apt install -y libicu-dev icu-devtools fontconfig fonts-dejavu

COPY --from=build /app /app
