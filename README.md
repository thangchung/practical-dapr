# practical-dapr
Practical Dapr on Coolstore business model

[![Price](https://img.shields.io/badge/price-FREE-0098f7.svg)](https://github.com/thangchung/practical-dapr/blob/master/LICENSE)
![graphql-api](https://github.com/thangchung/practical-dapr/workflows/graphql-ci/badge.svg?branch=master)
![identity-api](https://github.com/thangchung/practical-dapr/workflows/identity-ci/badge.svg?branch=master)
![product-catalog-api](https://github.com/thangchung/practical-dapr/workflows/product-catalog-ci/badge.svg?branch=master)
![inventory-api](https://github.com/thangchung/practical-dapr/workflows/inventory-ci/badge.svg?branch=master)

## Give a Star! :star:

If you liked `practical-dapr` project or if it helped you, please give a star :star: for this repository. That will not only help strengthen our .NET community but also improve cloud-native apps development skills for .NET developers in around the world. Thank you very much :+1:

# High Level Architecture

![](assets/high_level_architecture.png)

# Get starting

## Start required components

```bash
$ docker-compose -f docker-compose.yml -f docker-compose.override.yml run sqlserver
```

## Manual run with Visual Studio or Visual Code

```bash
$ dotnet run -p \src\GraphApi\CoolStore.GraphApi\CoolStore.GraphApi.csproj
$ dotnet run -p \src\ProductCatalog\CoolStore.ProductCatalogApi\CoolStore.ProductCatalogApi.csproj
$ dotnet run -p \src\Inventory\CoolStore.InventoryApi\CoolStore.InventoryApi.csproj
```

## Run with Dapr

- Follow those steps at https://github.com/dapr/cli to install Dapr CLI

```bash
$ cd src\ProductCatalog\CoolStore.ProductCatalogApi
$ dapr run --app-id product-catalog-api dotnet run
```

```bash
$ cd src\Inventory\CoolStore.InventoryApi
$ dapr run --app-id inventory-api dotnet run
```

```bash
$ cd src\GraphApi\CoolStore.GraphApi
$ dapr run --app-id graphql-api dotnet run
```

## Test it

- Go to http://localhost:5000

### Query

```js
query {
    products(
    page: 1
    pageSize: 5
    where: { price_lte: 10000 }
    order_by: { price: DESC }
  ) {
    edges {
      id
      name
      imageUrl
      price
      categoryId
      categoryName
      inventoryId
      inventoryLocation
    }
    totalCount
  }
}
```

![](assets/graphql_playground_query_products.png)

### Mutation

```js
mutation createProductMutation($createProductInput: CreateProductInput!) {
  createProduct(createProductInput: $createProductInput) {
    product {
      id
      name
    }
  }
}
```

```js
{
  "createProductInput": {
    "name": "product 1",
    "description": "this is a description",
    "imageUrl": "https://picsum.photos/1200/900?image=100",
    "price": 100,
    "categoryId": "77666AA8-682C-4047-B075-04839281630A",
    "inventoryId": "90C9479E-A11C-4D6D-AAAA-0405B6C0EFCD"
  }
}
```

![](assets/graphql_playground_mutation.png)

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :p