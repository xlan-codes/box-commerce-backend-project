# Starter API

This is a startup project for building a backend API for both a Client facing and an Agent facing application.

It includes:

-   Demo CRUD controller that writes to a MongoDB instance
-   Dockerfile configuration
-   Error handling and logging

## Running locally

Run a local or a MongoDB container instance.

Check the `appSettings.Development.json` file for the MongoDB configuration.

```
cd WebAPI
dotnet run
```

Choosing appsettings.json `dotnet run --environment <something>`

```
$env:ASPNETCORE_ENVIRONMENT="OIDC"
```

Visit http://localhost:3000 -> Login -> Copy the obtained token -> Paste in the Swagger UI

## Staging (first time deploy)

```sh
cd /app/inventoryservice.api
git pull
cd WebAPI
dotnet publish
cd /bin/...
# check to see if the configured port is free
ASPNETCORE_ENVIRONMENT=Staging dotnet WebAPI.dll
pm2 start --name inventoryservice.api "ASPNETCORE_ENVIRONMENT=Staging dotnet WebAPI.dll"
```

## Staging (recurrent deploy)

```sh
cd /app/inventoryservice.api
git pull
cd WebAPI
dotnet publish
pm2 restart inventoryservice.api
```

## Running with Docker

Remove the URL property from appsettings.Production.json

```
cd WebAPI
docker build ./ --tag bff:1.0.0
docker run --rm -it -p 3000:80 --name bff bff:1.0.0
```
