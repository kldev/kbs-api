echo "Run KBS.Web - Development"

export ASPNETCORE_ENVIRONMENT="Development"
export CorsConfig__Urls="http://localhost:3000,http://localhost:3001,http://localhost:3002,http://localhost:8000"
export ASPNETCORE_URLS="http://*:50101"

(dotnet run -p KBS.Web)
