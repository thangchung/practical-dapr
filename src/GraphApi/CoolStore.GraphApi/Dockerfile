#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.1-alpine3.10 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101-alpine3.10 AS build
ENV GLIBC_REPO=https://github.com/sgerrand/alpine-pkg-glibc
ENV GLIBC_VERSION=2.30-r0
RUN set -ex && \
    apk --update add libstdc++ curl ca-certificates && \
    for pkg in glibc-${GLIBC_VERSION} glibc-bin-${GLIBC_VERSION}; \
    do curl -sSL ${GLIBC_REPO}/releases/download/${GLIBC_VERSION}/${pkg}.apk -o /tmp/${pkg}.apk; done && \
    apk add --allow-untrusted /tmp/*.apk && \
    rm -v /tmp/*.apk && \
    /usr/glibc-compat/sbin/ldconfig /lib /usr/glibc-compat/lib
WORKDIR /src
COPY ["nuget.config", "./"]
COPY ["services.json", "./"]
COPY ["src/GraphApi/CoolStore.GraphApi/CoolStore.GraphApi.csproj", "src/GraphApi/CoolStore.GraphApi/"]
COPY ["src/BuildingBlocks/N8T.Infrastructure/N8T.Infrastructure.csproj", "src/BuildingBlocks/N8T.Infrastructure/"]
COPY ["src/BuildingBlocks/N8T.Domain/N8T.Domain.csproj", "src/BuildingBlocks/N8T.Domain/"]
RUN dotnet restore "src/GraphApi/CoolStore.GraphApi/CoolStore.GraphApi.csproj"  --configfile nuget.config
COPY . .
WORKDIR "/src/src/GraphApi/CoolStore.GraphApi"
RUN dotnet build "CoolStore.GraphApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoolStore.GraphApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoolStore.GraphApi.dll"]