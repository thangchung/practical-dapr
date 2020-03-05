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
$ dotnet run -p \src\GraphApi\CoolStore.GraphApi\CoolStore.GraphApi.csproj
$ dotnet run -p \src\ProductCatalog\CoolStore.ProductCatalogApi\CoolStore.ProductCatalogApi.csproj
```

## Step 3
Go to http://localhost:5000

```js
query{
    products(currentPage: 1, highPrice: 1000) {
    id
    name
    imageUrl
  }
}
```

```js
mutation createProductMutation($createProductInput: CreateProductInput!) {
  createProduct(createProductInput: $createProductInput) {
    product {
      id
      name
    }
  }
}
{
  "createProductInput": {
    "name": "product 1",
    "description": "this is a description",
    "imageUrl": "https://picsum.photos/1200/900?image=100",
    "price": 100,
    "categoryId": "77666AA8-682C-4047-B075-04839281630A",
    "inventoryId": "88ef3cab-5f7e-4111-b151-3fe0d9c20733"
  }
}
```

# Best reference articles
- https://andrewlock.net/sharing-appsettings-json-configuration-files-between-projects-in-asp-net-core/
