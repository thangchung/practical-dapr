- Install Sql Server
```bash
$ helm install sqlserver stable/mssql-linux --debug --namespace dev --values sqlserver/values.dev.yaml
```

- Test your charts
```bash
$ helm upgrade --debug --namespace dev --install --wait product-catalog-api product-catalog-api --dry-run
$ helm upgrade --debug --namespace dev --install --wait inventory-api inventory-api --dry-run
```
