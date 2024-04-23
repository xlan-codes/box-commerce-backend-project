pm2 stop notificationservice.api;
cd WebAPI; 
dotnet publish -c Release
pm2 restart notificationservice.api; 
pm2 reset notificationservice.api;
