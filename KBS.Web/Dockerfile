FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY KBS.Web.csproj .
RUN dotnet restore

COPY . .

RUN ls -la

RUN dotnet publish -c release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=build /src/out .
RUN ls -la
ENV ASPNETCORE_ENVIRONMENT Docker 
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
ENTRYPOINT ["dotnet", "KBS.Web.dll"]

# run  (double undersorce)
# docker run -e "ConnectionStrings__Main"="Host=172.17.0.1;Database=kbs;Username=postgres;Password=postgres"  kbs-api