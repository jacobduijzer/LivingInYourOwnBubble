FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.17 as build
WORKDIR /app
COPY . .
RUN dotnet restore "CattleInformationSystem.sln"
RUN dotnet publish "src/CIS.Web/CIS.Web.csproj" -c Release -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.17 as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/CIS.Web.dll" ]