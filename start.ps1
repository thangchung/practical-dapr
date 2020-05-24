# Inventory Api
start-process powershell.exe -argument `
  '-nologo
    -noprofile
    -executionpolicy bypass
    -command dapr run `
      --app-id inventoryapi `
      --app-port 5005 `
      --port 5015 `
      --log-level debug `
      dotnet run dotnet `
        -- -p src\Inventory\CoolStore.InventoryApi\CoolStore.InventoryApi.csproj'
Start-Sleep -Seconds 2

# Product Catalog Api
start-process powershell.exe -argument `
  '-nologo
    -noprofile
    -executionpolicy bypass
    -command dapr run `
      --app-id productcatalogapi `
      --app-port 5003 `
      --port 5013 `
      --log-level debug `
      dotnet run dotnet `
        -- -p src\ProductCatalog\CoolStore.ProductCatalogApi\CoolStore.ProductCatalogApi.csproj'
Start-Sleep -Seconds 2

# Identity Server
start-process powershell.exe -argument `
  '-nologo
    -noprofile
    -executionpolicy bypass
    -NoExit
    -command dapr run `
      --app-id identityserver `
      --app-port 5001 `
      --port 5011 `
      --log-level debug `
      dotnet run dotnet `
        -- -p src\Identity\CoolStore.IdentityServer\CoolStore.IdentityServer.csproj'
Start-Sleep -Seconds 2

# Graph Api
start-process powershell.exe -argument `
  '-nologo
    -noprofile
    -executionpolicy bypass
    -command dapr run `
      --app-id graphapi `
      --app-port 5002 `
      --port 5012 `
      --log-level debug `
      dotnet run dotnet `
        -- -p src\GraphApi\CoolStore.GraphApi\CoolStore.GraphApi.csproj'
Start-Sleep -Seconds 2

# Web Host
start-process powershell.exe -argument `
  '-nologo
    -noprofile
    -executionpolicy bypass
    -command dapr run `
      --app-id webuihost `
      --app-port 5100 `
      --port 6868 `
      --log-level debug `
      dotnet run dotnet `
        -- -p src\WebUI\CoolStore.WebUI.Host\CoolStore.WebUI.Host.csproj'
