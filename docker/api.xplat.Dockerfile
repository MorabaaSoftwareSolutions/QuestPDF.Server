FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy-amd64 as build

RUN apt-get update
RUN apt-get install -y software-properties-common clang zlib1g-dev
RUN add-apt-repository ppa:ubuntu-toolchain-r/test
RUN apt-get update
RUN apt-get install -y --only-upgrade libstdc++6

COPY ../src /src

WORKDIR /src/api

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine as runtime

RUN apk add gcompat

ENV GLIBC_VERSION 2.32-r0
ENV GLIBC_REPO=https://github.com/sgerrand/alpine-pkg-glibc

RUN set -ex && \
    apk --update add libstdc++ curl ca-certificates && \
    for pkg in glibc-${GLIBC_VERSION} glibc-bin-${GLIBC_VERSION}; \
        do curl -sSL ${GLIBC_REPO}/releases/download/${GLIBC_VERSION}/${pkg}.apk -o /tmp/${pkg}.apk; done && \
    apk add --allow-untrusted /tmp/*.apk && \
    rm -v /tmp/*.apk && \
    /usr/glibc-compat/sbin/ldconfig /lib /usr/glibc-compat/lib

COPY --from=build /app /app
