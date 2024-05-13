FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy as base

RUN apt-get update
RUN apt-get install -y software-properties-common --fix-missing
RUN add-apt-repository ppa:ubuntu-toolchain-r/test
RUN apt-get update
RUN apt-get install -y --only-upgrade libstdc++6

FROM base as build

COPY ../src /src

WORKDIR /src/api

RUN dotnet publish -c Release -o /app

FROM base as runtime

COPY --from=build /app /app

