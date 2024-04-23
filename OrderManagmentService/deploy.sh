pm2 stop OrderService.api;
cd WebAPI; 
dotnet publish -c Release
pm2 restart OrderService.api; 
pm2 reset OrderService.api;
