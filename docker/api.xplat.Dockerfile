FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy-amd64 as build

RUN apt-get update
RUN apt-get install -y software-properties-common clang zlib1g-dev
RUN add-apt-repository ppa:ubuntu-toolchain-r/test
RUN apt-get update
RUN apt-get install -y --only-upgrade libstdc++6

COPY ../src /src

WORKDIR /src/api

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-amd64 as runtime

RUN apt-get update
RUN apt-get install -y software-properties-common clang zlib1g-dev
RUN add-apt-repository ppa:ubuntu-toolchain-r/test
RUN apt-get update
RUN apt-get install -y --only-upgrade libstdc++6

COPY --from=build /app /app
