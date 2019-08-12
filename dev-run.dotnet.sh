echo "Run KBS.Web - Development"

export ASPNETCORE_ENVIRONMENT="Development"
export CorsConfig__Urls="http://localhost:3000,http://localhost:3001,http://localhost:3002"


(cd ./KBS.Web && dotnet restore && dotnet publish -o obj/Debug/publish -c Release)
(cd ./KBS.Web/obj/Debug/publish && ASPNETCORE_URLS="http://*:50101" dotnet KBS.Web.dll)
