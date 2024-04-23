pm2 stop inventoryservice.api;
cd WebAPI; 
dotnet publish -c Release
pm2 restart inventoryservice.api; 
pm2 reset inventoryservice.api;
