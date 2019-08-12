echo "Run KBS.Data.ConsoleApp - Development"

export ConnectionStrings__Main="Host=127.0.0.1;Database=kbs;Username=postgres;Password=postgres"

(cd ./KBS.Data.ConsoleApp && dotnet restore && dotnet publish -o obj/Debug/publish -c Release)
(cd ./KBS.Data.ConsoleApp/obj/Debug/publish && dotnet KBS.Data.ConsoleApp.dll -c drop)
(cd ./KBS.Data.ConsoleApp/obj/Debug/publish && dotnet KBS.Data.ConsoleApp.dll -c update)
(cd ./KBS.Data.ConsoleApp/obj/Debug/publish && dotnet KBS.Data.ConsoleApp.dll -c seed)