# practical-dapr
Practical Dapr on Coolstore business model

# High Level Architecture

![](assets/high_level_architecture.png)

# Get starting

## Step 1

```bash
$ docker-compose -f docker-compose.yml -f docker-compose.override.yml run sqlserver
```

## Step 2

```bash
$ cd src\ProductCatalog\CoolStore.ProductCatalogApi
$ dotnet ef database update
```

## Step 3

```bash
$ dotnet run -p \src\GraphApi\CoolStore.GraphApi\CoolStore.GraphApi.csproj
$ dotnet run -p \src\ProductCatalog\CoolStore.ProductCatalogApi\CoolStore.ProductCatalogApi.csproj
```

## Step 4
Go to http://localhost:5000

```js
products(currentPage: 1, highPrice: 1000) {
  id
  name
  imageUrl
}
```

# Best reference articles
- https://andrewlock.net/sharing-appsettings-json-configuration-files-between-projects-in-asp-net-core/
