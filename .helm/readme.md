- Install Sql Server
```bash
$ helm install sqlserver stable/mssql-linux --debug --namespace dev --values sqlserver/values.dev.yaml
```

```bash
$ kubectl run mssqlcli --image=microsoft/mssql-tools -ti --restart=Never --rm=true -- /bin/bash
$ sqlcmd -S sqlserver -U sa
```

```sql
CREATE DATABASE CoolStoreDb
GO

SELECT name FROM sys.databases
GO
```

- Test your charts
```bash
$ helm upgrade --debug --namespace dev --install --wait product-catalog-api product-catalog-api --dry-run
$ helm upgrade --debug --namespace dev --install --wait inventory-api inventory-api --dry-run
```
