#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.1-alpine3.10 AS base
WORKDIR /app
EXPOSE 80

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
COPY ["src/ProductCatalog/CoolStore.ProductCatalogApi/CoolStore.ProductCatalogApi.csproj", "src/ProductCatalog/CoolStore.ProductCatalogApi/"]
COPY ["src/BuildingBlocks/N8T.Domain/N8T.Domain.csproj", "src/BuildingBlocks/N8T.Domain/"]
COPY ["src/BuildingBlocks/N8T.Infrastructure/N8T.Infrastructure.csproj", "src/BuildingBlocks/N8T.Infrastructure/"]
RUN dotnet restore "src/ProductCatalog/CoolStore.ProductCatalogApi/CoolStore.ProductCatalogApi.csproj"
COPY . .
WORKDIR "/src/src/ProductCatalog/CoolStore.ProductCatalogApi"
RUN dotnet build "CoolStore.ProductCatalogApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoolStore.ProductCatalogApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoolStore.ProductCatalogApi.dll"]