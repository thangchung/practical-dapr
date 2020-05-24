# Developer Guide

## Install pre-requisites

- .NET 5 Preview 3
- Docker for Desktop installed with Kubernetes is enabled.
  - 2.2.0.5 (43884)
- Follow those steps at https://github.com/dapr/cli to install Dapr CLI.
  - dapr: 0.0.7
  - dapr cli: 0.0.7
- Follow these steps at https://github.com/dotnet/tye/blob/master/docs/getting_started.md to install `tye` CLI.
  - 0.3.0-alpha.20265.1+ef441e87047930e04d24646e0d89183b07552747
- EF Core CLI
  - 3.1.3

## Grab source code

Clone the source code at https://github.com/thangchung/practical-dapr

```bash
$ git@github.com:thangchung/practical-dapr.git
```

## Up & running

### **Option 1**: Run locally with Tye (no Dapr)

> Currently, `tye` v0.2 and v0.3 with `dapr` extension wasn't working correctly. So please comment `dapr` extension temporary. 

> Find and replace all `noTye` configs in appsetttings.json from `true` to `false`.

```bash
$ tye run
```

Then you can see `tye dashboard` as below

![](assets/tye-dashboard.png)

#### Testing it

- Go to `webui`, and on `Bindings` column click to `http` link (http://localhost:58275 in the picture) to access to `Blazor Web UI`
- Go to `identity-api`, and on `Bindings` column click to `http` link (http://localhost:58269 in the picture) to access to `Identity Server 4`
- Go to `graph-api`, and on `Bindings` column click to `http` link (http://localhost:58267 in the picture) to access to `GraphQL Api Server`

### **Option 2**: Run mixed Tye and Dapr (practical way)

- Run `redis` and `sqlserver` from `tye`

```bash
$ tye run tye-min.yaml
```

- Run dapr locally

```bash
$ powershell -f start.ps1
```

Waiting seconds, then you happy to go `http://localhost:5012`

## Contribution and development

- Step 1: Open up `src\Identity\CoolStore.IdentityServer\appsettings.json`, and turn `IsDev` to `true`
- Step 2: Open up `tye.yaml`, and comment out `name: webui`
- Step 3: At root of project, open up terminal and type `tye run`, and we get `IdentityUrl` and `GraphQLUrl` from the `tye dashboard`
- Step 4: Open up `src\WebUI\CoolStore.WebUI.Host\appsettings.json`, and turn `IsDev` to `true`, and replace `IdentityUrl` with the value at Step 3, and also replace `GraphQLUrl` with the value at Step 3. Final step, change `src\WebUI\CoolStore.WebUI.Host\GraphQL\berry.json` with the `GraphQL` endpoint at the Step 3
- Step 5: Run `CoolStore.WebUI.Host` at the debug mode, then we are ready to develop the `practical-dapr` project.

Happy hacking!