pm2 stop assemblyservice.api;
cd WebAPI; 
dotnet publish -c Release
pm2 restart assemblyservice.api; 
pm2 reset assemblyservice.api;
