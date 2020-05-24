# Debugging

```bash
$ tye run --debug src/ProductCatalog/CoolStore.ProductCatalogApi
```

Follow steps at [Debugging Dapr application using Tye tool](https://dev.to/thangchung/debugging-dapr-application-using-tye-tool-1djb)

# Distributed logs and tracing

```bash
$ tye run --dtrace zipkin=http://localhost:9411 --logs seq=http://localhost:5340
```

Now, you can access Seq at http://localhost:5340, and Zipkin at http://localhost:9411.

Run serveral queries on `graph-api`, then come back to `Seq` and `Zipkin` UIs, you should see logs and tracing.