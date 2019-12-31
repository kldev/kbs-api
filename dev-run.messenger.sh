echo "Run KBS.Messenger - Development"

export ASPNETCORE_ENVIRONMENT="Development"
# appsettings.json settings override
export Kbs__Api="http://127.0.0.1:50101"


(cd ./KBS.Messenger && dotnet restore && dotnet publish -o obj/Debug/publish -c Release)
(cd ./KBS.Messenger/obj/Debug/publish && ASPNETCORE_URLS="http://*:14333" dotnet KBS.Messenger.dll)
