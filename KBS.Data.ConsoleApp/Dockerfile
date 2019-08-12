FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY KBS.Data.ConsoleApp.csproj .
RUN dotnet restore

COPY . .

RUN ls -la

RUN dotnet publish -c release -o out


FROM mcr.microsoft.com/dotnet/core/runtime:2.2

WORKDIR /app
COPY --from=build /src/out .
RUN ls -la
ENTRYPOINT ["dotnet", "KBS.Data.ConsoleApp.dll"]

# run  (double undersorce)
# docker run -e "ConnectionStrings__Main"="Host=172.17.0.1;Database=kbs;Username=postgres;Password=postgres"  kbs-schema -c update